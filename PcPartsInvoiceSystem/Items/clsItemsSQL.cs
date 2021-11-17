using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace PcPartsInvoiceSystem.Items
{
    class clsItemsSQL
    {
        /// <summary>
        /// Populates Item List with items from the current database.
        /// </summary>
        /// <param name="db">the database to be pulled from</param>
        /// <returns>Returns a list of items for sale</returns>
        public List<clsItem> PopulateItemList(clsDataAccess db)
        {
            List<clsItem> itemList = new List<clsItem>();

            string sSQL;    //Holds an SQL statement
            int iRet = 0;   //Number of return values
            DataSet ds = new DataSet(); //Holds the return values
            clsItem item; //Used to load the return values into the combo box

            //Create the SQL statement to extract the items
            sSQL = "SELECT * FROM ItemDesc";

            //Extract the items and put them into the DataSet
            ds = db.ExecuteSQLStatement(sSQL, ref iRet);

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

        public void AddItem()
        {

        }
    }
}
