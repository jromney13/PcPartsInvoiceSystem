using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace PcPartsInvoiceSystem.Items
{
    /// <summary>
    /// class to return different SQL statements for the Edit Items window, depending on the required action. 
    /// Select all, add, delete, edit, and find on outstanding invoices.
    /// </summary>
    class clsItemsSQL
    {
        #region low level methods

        /// <summary>
        /// SQL statement to select all items
        /// </summary>
        /// <returns>returns a SQL statement to select all items</returns>
        public string SelectAllItems()
        {
            try
            {
                return "SELECT * FROM ItemDesc";
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to add an item
        /// </summary>
        /// <param name="itemToAdd">The item object to be added</param>
        /// <returns>Returns an SQL statement to select all items</returns>
        public string AddItem(clsItem itemToAdd)
        {
            try
            {
                return "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES (\"" + itemToAdd.sItemCode + "\", \"" + itemToAdd.sItemDescription + "\", \"" + itemToAdd.sItemCost + "\")";
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to edit an item
        /// </summary>
        /// <param name="itemToEdit">The item object to be edited</param>
        /// <returns>Returns an SQL statement to edit the selected item</returns>
        public string EditItem(clsItem itemToEdit)
        {
            try
            {
                return "UPDATE ItemDesc SET ItemDesc = \"" + itemToEdit.sItemDescription + "\", Cost = \"" + itemToEdit.sItemCost + "\" WHERE ItemCode = \"" + itemToEdit.sItemCode + "\"";
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to delete an item
        /// </summary>
        /// <param name="itemToDelete">The item object to be deleted</param>
        /// <returns>Returns an SQL statement to delete the selected item</returns>
        public string DeleteItem(clsItem itemToDelete)
        {
            try
            {
                return "DELETE FROM ItemDesc WHERE ItemCode = + " + itemToDelete.sItemCode;
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL statement to find invoices which the current item is currently part of
        /// </summary>
        /// <param name="itemToDelete">The item which is attempting to be deleted</param>
        /// <returns>Returns an SQL statement to find the selected item on any outstanding invoices.</returns>
        public string ConnectedInvoices(clsItem itemToDelete)
        {
            try
            {
                return "SELECT DISTINCT(InvoiceNum) FROM LineItems WHERE ItemCode = " + itemToDelete.sItemCode;
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns an SQL statement to check for an item in the database, by the given item code.
        /// </summary>
        /// <param name="sItemCode">The user inputted item code</param>
        /// <returns>An SQL statement to check for an item in the database, by the given item code.</returns>
        public string FindItem(string sItemCode)
        {
            try
            {
                return "SELECT * FROM ItemDesc " +
                        "WHERE ItemCode = \"" + sItemCode + "\"";
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }
}
