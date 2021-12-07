using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using PcPartsInvoiceSystem.Main;

namespace PcPartsInvoiceSystem.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region attributes

        /// <summary>
        /// class to access database. Should this be on main??
        /// </summary>
        clsDataAccess db; //On main? and pass into this window?

        /// <summary>
        /// class to handle Item Logic
        /// </summary>
        clsItemsLogic items;

        /// <summary>
        /// Tells if the user is deleting a row.
        /// </summary>
        bool bIsDeleting = false;

        #endregion

        #region top-level methods
        public wndItems()
        {
            try
            {
                InitializeComponent();

                //Initialize Database
                db = new clsDataAccess(); //On main?

                //Initialize item logic
                items = new clsItemsLogic();

                //Bind item list to DataGrid
                dgItems.ItemsSource = items.PopulateItemList(db);

                //Select first item
                dgItems.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }

        /// <summary>
        /// Add an item to the table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Make sure the item code is not empty
                if (inAddCode.Text.Equals(""))
                {
                    lblMessage.Text = "Item must have a code.";
                    return;
                }

                //Make sure the item code is unique
                if (!items.IsUnique(db, inAddCode.Text))
                {
                    lblMessage.Text = "Item code must be unique";
                    return;
                }

                //Make sure the description input has a value
                if (inAddDescription.Text.Equals(""))
                {
                    lblMessage.Text = "Item must have a description.";
                    return;
                }

                //Make sure the cost input can be parsed as a double
                if (!Double.TryParse(inAddCost.Text, out double cost))
                {
                    lblMessage.Text = "Cost must be a dollar amount.";
                    return;
                }

                //Call items.AddItem() to add
                clsItem newItem = new clsItem(inAddCode.Text, inAddDescription.Text, inAddCost.Text);
                items.AddItem(db, newItem);

                //reflect the change in the DataGrid
                dgItems.ItemsSource = items.PopulateItemList(db);

                //Leave message for user
                lblMessage.Text = inAddDescription.Text + " added.";
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Edits the currently selected item with the user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Make sure there is a valid selection
                if (dgItems.SelectedItem != null && dgItems.SelectedIndex < dgItems.Items.Count - 1)
                {
                    //Get selected item
                    clsItem itemToEdit = (clsItem)dgItems.SelectedItem;

                    //Make sure the description input has a value
                    if (inEditDescription.Text.Equals(""))
                    {
                        lblMessage.Text = "Item must have a description.";
                        return;
                    }

                    //Make sure the cost input can be parsed as a double
                    if (!Double.TryParse(inEditCost.Text, out double cost))
                    {
                        lblMessage.Text = "Cost must be a dollar amount.";
                        return;
                    }

                    //Update local item with new values
                    if (itemToEdit != null)
                    {
                        itemToEdit.sItemDescription = inEditDescription.Text;
                        itemToEdit.sItemCost = inEditCost.Text;
                    }
                    else
                    {
                        lblMessage.Text = "Select an item to edit.";
                    }
                    

                    //Call items.EditItem() to edit                
                    items.EditItem(db, itemToEdit);

                    //reflect the change in the DataGrid
                    dgItems.ItemsSource = items.PopulateItemList(db);

                    //Inform the user of the change
                    lblMessage.Text = inEditDescription.Text + " edited.";
                }

                else
                {
                    lblMessage.Text = "Select an item to edit.";
                    return;
                }
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Deletes the currently selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //test
                lblMessage.Text = inEditDescription.Text + " deleted.";
                
                //If there is an item selected
                //if (dgItems.SelectedIndex < dgItems.Items.Count - 1)
                //{
                //    //Create item object from current row
                //    clsItem itemDelete = (clsItem)dgItems.SelectedItem;

                //    //Check that this item is not on any existing invoices
                //    List<string> invoiceNumbers = new List<string>();
                //    invoiceNumbers = items.ItemInvoices(db, itemDelete);

                //    //if this item is on an invoice, leave a message and do not delete
                //    if (invoiceNumbers.Count > 0)
                //    {
                //        lblMessage.Text = "Cannot Delete. Item currently on invoice(s): ";
                //        foreach (string number in invoiceNumbers)
                //        {
                //            lblMessage.Text += number + " ";
                //        }
                //        lblMessage.Text += ".";
                //    }
                //    //Otherwise, delete this item
                //    else
                //    {
                //Mark that this item is deleting

                //Call items.DeleteItem()
                //        items.DeleteItem(db, itemDelete);
                //    }
                //}
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Update the edit boxes with item information reflecting the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Make sure current row is non-null
                if (dgItems.SelectedItem != null)
                {
                    //Make sure user is not deleting
                    if (!bIsDeleting)
                    {
                        //Make sure selected index is valid
                        if (dgItems.SelectedIndex < dgItems.Items.Count - 1)
                        {
                            //Create object from current row
                            clsItem tempItem = (clsItem)dgItems.SelectedItem;

                            //Set description
                            inEditDescription.Text = tempItem.sItemDescription;
                            //Set cost
                            inEditCost.Text = tempItem.sItemCost;
                        }
                        else
                        {
                            inEditDescription.Text = "";
                            inEditCost.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void lblGoBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion

        #region low-level methods

        /// <summary>
        /// Return a list of the currently available items
        /// </summary>
        /// <returns>A list of item class objects</returns>
        public List<clsItem> GetItemList()
        {
            try
            {
                return items.itemList;
            }            
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        #region exception handling

        /// <summary>
        /// Handles any exceptions which cause an error in the program. Shows a message box to pinpoint the method which caused the error.
        /// </summary>
        /// <param name="sClass">The class of the error</param>
        /// <param name="sMethod">The method of the error</param>
        /// <param name="sMessage">The trail leading to the exception</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }

        #endregion


    }
}
