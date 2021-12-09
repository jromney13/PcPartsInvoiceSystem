using PcPartsInvoiceSystem.Main;
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

namespace PcPartsInvoiceSystem.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// object of the logic class for the search window
        /// </summary>
        private clsSearchLogic datalogic;
        /// <summary>
        /// Constructor for window
        /// runs the loadDataGrid method on start using the starting sql which should be all records of invoices
        /// </summary>
        /// 
        private bool submitted = false;

        public wndSearch()
        {
            InitializeComponent();
            datalogic = new clsSearchLogic();
            loadDataGrid(datalogic.SQLGen());
            
        }
        /// <summary>
        /// Method that takes the list recieved from the sql querry and puts it into a datagrid
        /// </summary>
        /// <param name="dataList"></param>
        private void loadDataGrid(List<List<String>> dataList) 
        {
            clearGrid();
            for(int i = 0; i<dataList.Count; i++)
            {
                invoiceDataGrid.Items.Add(dataList[i]);
                
            }
            for (int i = 0; i < dataList.Count;i++) 
            {
                Console.WriteLine("List: "+dataList[i][0]);
            }
            LoadBoxes(dataList);

        }
        /// <summary>
        /// clears all cells in data grid
        /// </summary>
        private void clearGrid() 
        {
            invoiceDataGrid.Items.Clear();
        }
        /// <summary>
        /// puts current availiable values into combo boxes
        /// at the start all options should be availible but as things are selected it narrows down
        /// </summary>
        /// <param name="str"></param>
        private void LoadBoxes(List<List<String>> str) 
        {
            List<String> numList = new List<String>();
            List<String> dateList = new List<String>();
            List<String> costList = new List<String>();
            for (int i = 0; i < str.Count; i++) 
            {
                numList.Add(str[i][0]);
                dateList.Add(str[i][1]);
                costList.Add(str[i][2]);
            }
            numList = RemoveDups(numList);
            dateList = RemoveDups(dateList);
            costList = RemoveDups(costList);
            costList = SortList(costList);
            invoiceNumBox.ItemsSource = numList;
            invoiceDateBox.ItemsSource = dateList;
            invoiceCostBox.ItemsSource = costList;
        }
        /// <summary>
        /// Method that removes duplicate dates and costs from the list that will be used for combo boxes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private List<String> RemoveDups(List<String> str) 
        {
            List<String> temp = new List<String>();
            temp = str;
            for (int i = 0; i < temp.Count; i++) 
            {
                for (int j = 0; j < temp.Count; j++) 
                {
                    if (temp[i].Equals(temp[j])&&i!=j) 
                    {
                        temp.RemoveAt(j);
                    }
                }
            }
            return temp;
        }
        /// <summary>
        /// Sorts list of cost for combo box
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private List<String> SortList(List<String> str) 
        {
            int prev;
            int curr;
            String temp;
            for (int j = 0; j < str.Count; j++)
            {
                for (int i = 1; i < str.Count; i++)
                {
                    Int32.TryParse(str[i - 1], out prev);
                    Int32.TryParse(str[i], out curr);
                    if (curr < prev)
                    {
                        temp = str[i - 1];
                        str[i - 1] = str[i];
                        str[i] = temp;
                        i = 1;
                    }
                }
            }

            return str;
        }
        /// <summary>
        /// Method that selects record that is highlighted in the data grid
        /// This is what would be sending data to the next window
        /// It might be usefull to make an object for a invoice so it is easier to send that data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //List<String> row = new List<String>();
                //if (invoiceDataGrid.SelectedItems.Count > 0)
                //{
                //    for (int i = 0; i < invoiceDataGrid.SelectedItems.Count; i++)
                //    {
                //        List<String> selectedFile = (List<String>)invoiceDataGrid.SelectedItems[i];
                //        row = selectedFile;
                //    }
                //}
                if (invoiceDataGrid.SelectedItem != null) 
                {
                    List<string> invoiceString = (List<string>)invoiceDataGrid.SelectedItem;
                    wndMain wndMain = new wndMain(invoiceString[0]);
                    wndMain.Show();
                    this.Close();

                }
                
                submitted = true;
                //string invoiceNum = invoiceNumBox.SelectedItem.ToString();

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }

        }
        /// <summary>
        /// This button closes the search window and returns the user to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }

        }
        /// <summary>
        /// updates for when the drop down box closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceNumBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (invoiceNumBox.SelectedItem != null)
                {
                    int i;
                    Int32.TryParse(invoiceNumBox.SelectedItem.ToString(), out i);
                    datalogic.UpdateInvoiceNum(i);
                    loadDataGrid(datalogic.SQLGen());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        /// <summary>
        /// updates for when the drop down box closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceCostBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (invoiceCostBox.SelectedItem != null)
                {
                    int i;
                    Int32.TryParse(invoiceCostBox.SelectedItem.ToString(), out i);
                    datalogic.UpdateInvoiceCost(i);
                    loadDataGrid(datalogic.SQLGen());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// updates for when the drop down box closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invoiceDateBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (invoiceDateBox.SelectedItem != null)
                {
                    datalogic.UpdateInvoiceDate(invoiceDateBox.SelectedItem.ToString());
                    loadDataGrid(datalogic.SQLGen());
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// clears selected combo box values and resets the data grid to the start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                invoiceCostBox.SelectedItem = null;
                invoiceNumBox.SelectedItem = null;
                invoiceDateBox.SelectedItem = null;
                datalogic.UpdateInvoiceCost(-1);
                datalogic.UpdateInvoiceDate("");
                datalogic.UpdateInvoiceNum(-1);
                List<List<String>> lt = datalogic.SQLGen();
                loadDataGrid(lt);
                LoadBoxes(lt);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!submitted)
            {
                wndMain wndMain = new wndMain();
                wndMain.Show();
            }

        }
    }
}
