using PcPartsInvoiceSystem.Items;
using PcPartsInvoiceSystem.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace PcPartsInvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        /// <summary>
        /// Search Window for Invoice searching
        /// </summary>
        wndSearch searchWindow;
        /// <summary>
        /// Items window to edit and add items to database
        /// </summary>
        wndItems itemsWindow;
        /// <summary>
        /// Main Logic class for Main Window
        /// </summary>
        clsMainLogic mainLogic = new clsMainLogic();

        /// <summary>
        /// Constructor for Main Window
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();

                cmbItems.ItemsSource = mainLogic.GenerateItemList();

                cmbItems.DisplayMemberPath = "sItemDescription";
                cmbItems.SelectedValuePath = "sItemDescription";
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }

        /// <summary>
        /// Opens the seach window to search for invoices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                searchWindow = new wndSearch();
                searchWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }


        }

        /// <summary>
        /// Opens the items window to update, add, or delete items in the db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                itemsWindow = new wndItems();
                itemsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Displays the item cost when an item is selected from the drop down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsItem selectedItem = (clsItem)cmbItems.SelectedItem;
                txtItemCost.Text = selectedItem.sItemCost;
            }
            catch (Exception ex)
            {
                //Top Level: Handle Exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Adds the item from the invoice. Udpates the total cost of the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Deletes an item from the invoice displayed. Updates the total cost of the invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Saves the invoice after editing or after a new creation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Enables editing of the invoice. Enables the add and delete item buttons after invoice is passed from search window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Deletes the invoice that was passed to the main window from the search window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Called when the items window is closed. Runs SQL query to refill combo box to ensure all items are present.
        /// </summary>
        public void checkItemUpdates()
        {
            // Call logic class method that runs SQL statement

            // Update item combo box

        }

        /// <summary>
        /// Called when the search window is closed. SQL statement is run to gather items in invoice and display them in datagrid.
        /// </summary>
        public void selectInvoice(string invoiceId)
        {
            // Calls logic clss method that runs SQL statement

            // Updates datagrid

            // Updates invoice number label and total cost.
        }

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
    }
}
