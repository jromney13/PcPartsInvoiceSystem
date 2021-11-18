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
    /// Class to handle all logic for the Edit Items window. Handles all SQL functions,
    /// and stores a list of all currently available items.
    /// </summary>
    class clsItemsLogic
    {
        #region attributes

        /// <summary>
        /// Initialize the SQL class object
        /// </summary>
        private clsItemsSQL SQL = new clsItemsSQL();

        /// <summary>
        /// The list of items
        /// </summary>
        public List<clsItem> itemList { get; set; }

        #endregion

        #region low level methods

        /// <summary>
        /// Populates Item List with items from the current database.
        /// </summary>
        /// <param name="db">the database to be pulled from</param>
        /// <returns>Returns a list of items for sale</returns>
        public List<clsItem> PopulateItemList(clsDataAccess db)
        {
            try
            {
                itemList = new List<clsItem>();

                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values
                clsItem item; //Used to load the return values into the combo box

                //Extract the items and put them into the DataSet
                ds = db.ExecuteSQLStatement(SQL.SelectAllItems(), ref iRet);

                //Loop through the data and create item objects to add to itemList
                for (int i = 0; i < iRet; i++)
                {
                    item = new clsItem();
                    item.sItemCode = ds.Tables[0].Rows[i]["ItemCode"].ToString();
                    item.sItemDescription = ds.Tables[0].Rows[i]["ItemDesc"].ToString();
                    item.sItemCost = "$" + String.Format("{0:0.00}", Convert.ToDouble(ds.Tables[0].Rows[i]["Cost"]));

                    itemList.Add(item);
                }
                return itemList;
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds a user inputted item to the list of items.
        /// </summary>
        /// <param name="db">The database to work from</param>
        /// <param name="itemNew">The new item to be added to the database</param>
        public void AddItem(clsDataAccess db, clsItem itemNew)
        {
            try
            {
                int iRet = 0;   //Number of return values

                //Execute SQL statement
                iRet = db.ExecuteNonQuery(SQL.AddItem(itemNew));
            }
            catch (Exception ex)
            {
                //Low-Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the selected item with the requested description and cost
        /// </summary>
        /// <param name="db">The database to work from</param>
        /// <param name="itemUpdate">The temp item to copy values from</param>
        public void EditItem(clsDataAccess db, clsItem itemToEdit)
        {
            try
            {
                int iRet = 0;   //Number of return values

                //Execute SQL statement
                iRet = db.ExecuteNonQuery(SQL.EditItem(itemToEdit));
            }
            catch (Exception ex)
            {
                //Low-Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes the requested item from the database.
        /// </summary>
        /// <param name="db">The database to work from</param>
        /// <param name="itemDelete">The item to be deleted</param>
        public void DeleteItem(clsDataAccess db, clsItem itemToDelete)
        {
            try
            {
                int iRet = 0;   //Number of return values

                //Execute SQL statement
                iRet = db.ExecuteNonQuery(SQL.DeleteItem(itemToDelete));
            }
            catch (Exception ex)
            {
                //Low-Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Checks the LineItems table to see if the item to delete is currently 
        /// part of an invoice.
        /// </summary>
        /// <param name="db">The database to work from</param>
        /// <param name="itemDelete">The item pending a delete</param>
        /// <returns>Returns a list of invoice numbers for which the item currently
        /// resides</returns>
        public List<string> ItemInvoices(clsDataAccess db, clsItem itemToDelete)
        {
            try
            {
                //List of invoice numbers attached to the item
                List<string> itemInvoiceList = new List<string>();

                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values
                string invoiceNum; //Used to load the return values into the combo box

                //Extract the items and put them into the DataSet
                ds = db.ExecuteSQLStatement(SQL.ConnectedInvoices(itemToDelete), ref iRet);

                //Loop through the data and create item objects to add to itemList
                for (int i = 0; i < iRet; i++)
                {
                    //add the invoice numbers to the list
                    invoiceNum = ds.Tables[0].Rows[i]["InvoiceNum"].ToString();
                    itemInvoiceList.Add(invoiceNum);
                }
                return itemInvoiceList;
            }
            catch (Exception ex)
            {
                //Low-Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }
}
