using PcPartsInvoiceSystem.Items;
using PcPartsInvoiceSystem.Search;
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

namespace PcPartsInvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        wndSearch searchWindow;
        wndItems itemsWindow;
        clsMainLogic mainLogic = new clsMainLogic();

        public wndMain()
        {
            InitializeComponent();

            cmbItems.ItemsSource = mainLogic.GenerateItemList();

            cmbItems.DisplayMemberPath = "sItemDescription";
            cmbItems.SelectedValuePath = "sItemDescription";
        }

        private void menuSearch_Click(object sender, RoutedEventArgs e)
        {
            searchWindow = new wndSearch();
            searchWindow.ShowDialog();
        }

        private void menuUpdate_Click(object sender, RoutedEventArgs e)
        {
            itemsWindow = new wndItems();
            itemsWindow.ShowDialog();
        }

        private void cmbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clsItem selectedItem = (clsItem)cmbItems.SelectedItem;
            txtItemCost.Text = selectedItem.sItemCost;
        }
    }
}
