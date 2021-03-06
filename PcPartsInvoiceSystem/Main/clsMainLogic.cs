using PcPartsInvoiceSystem.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PcPartsInvoiceSystem.Main
{
    /// <summary>
    /// Logic for the Main Window
    /// </summary>
    class clsMainLogic
    {
        /// <summary>
        /// Data Access Class to process SQL queries
        /// </summary>
        clsDataAccess db = new clsDataAccess();
        /// <summary>
        /// SQL class that returns SQL strings.
        /// </summary>
        clsMainSQL sql = new clsMainSQL();

        /// <summary>
        /// List of Items
        /// </summary>
        List<clsItem> itemList;

        /// <summary>
        /// Generates list of all items in the database
        /// </summary>
        /// <returns></returns>
        public List<clsItem> GenerateItemList()
        {
            try
            {
                itemList = new List<clsItem>();

                DataSet ds;

                int iRet = 0;

                ds = db.ExecuteSQLStatement(sql.SelectAllItemData(), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsItem item = new clsItem(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                    itemList.Add(item);
                }

                return itemList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserts an invoice into the database and returns the invoiceNumber of the created invoice.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public string AddInvoice(string invoiceDate, string totalCost, List<clsItem> itemList)
        {
            try
            {
                int iRet = 0;

                string statement = sql.InsertInvoice(invoiceDate, totalCost);

                iRet = db.ExecuteNonQuery(sql.InsertInvoice(invoiceDate, totalCost));

                string max = db.ExecuteScalarSQL(sql.GetLastInvoiceNumber());

                int index = 1;
                foreach (clsItem item in itemList)
                {
                    db.ExecuteNonQuery(sql.InsertLineItem(max, index.ToString(), item.sItemCode));
                    index++;
                }

                return max;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a list of items from a specified invoiceNum.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public List<clsItem> GetInvoice(string invoiceNum)
        {
            try
            {
                List<clsItem> itemList = new List<clsItem>();

                DataSet ds;

                int iRet = 0;

                string statement = sql.SelectInvoiceItems(invoiceNum);

                ds = db.ExecuteSQLStatement(sql.SelectInvoiceItems(invoiceNum), ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsItem item = new clsItem(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                    itemList.Add(item);
                }

                return itemList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes and invoice from the database.
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void DeleteInvoice(string invoiceNum)
        {
            try
            {
                int iRet = 0;

                iRet = db.ExecuteNonQuery(sql.DeleteLineItems(invoiceNum));

                iRet = db.ExecuteNonQuery(sql.DeleteInvoices(invoiceNum));
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Deletes the line items from an invoice in the database.
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void DeleteLines(string invoiceNum)
        {
            try
            {
                int iRet = 0;

                iRet = db.ExecuteNonQuery(sql.DeleteLineItems(invoiceNum));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adds invoice lines to the database.
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="itemList"></param>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        public void AddLines(string totalCost, List<clsItem> itemList, string invoiceNum, string invoiceDate)
        {
            try
            {
                int iRet = 0;

                //Update total cost
                iRet = db.ExecuteNonQuery(sql.UpdateTotalCost(totalCost, invoiceNum));

                // update lines
                int index = 1;
                foreach (clsItem item in itemList)
                {
                    db.ExecuteNonQuery(sql.InsertLineItem(invoiceNum, index.ToString(), item.sItemCode));
                    index++;
                }

                //update date
                iRet = db.ExecuteNonQuery(sql.UpdateInvoiceDate(invoiceDate, invoiceNum));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
