using System;
using System.Collections.Generic;
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

namespace PcPartsInvoiceSystem.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region attributes

        //class to access database
        clsDataAccess db; //On main? and pass into this window?

        //class to manage item list
        clsItemsSQL items;

        //class to handle item logic
        clsItemsLogic itemLogic;

        #endregion

        public wndItems()
        {
            InitializeComponent();

            //Initialize Database
            db = new clsDataAccess(); //On main?

            //Initialize a list of items
            items = new clsItemsSQL();

            //Initialize item logic
            itemLogic = new clsItemsLogic();

            //Bind item list to DataGrid
            dgItems.ItemsSource = items.PopulateItemList(db);
        }

        /// <summary>
        /// Add and item to the table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
