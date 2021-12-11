using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPartsInvoiceSystem.Main
{
    class clsMainSQL
    {
        /// <summary>
        /// SQL statement that returns all the items in the database
        /// </summary>
        /// <returns></returns>
        public string SelectAllItemData()
        {
            try
            {
                string sSQL = "SELECT * FROM ItemDesc";

                return sSQL;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// SQL statment that returns the invoice information for a specific invoice number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SelectInvoiceData(string invoiceNum)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SQL statment that returns a list of items for a specific invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string SelectInvoiceItems(string invoiceNum)
        {
            try
            {
                string sSQL = "SELECT ItemDesc.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost" +
                    " FROM ItemDesc INNER JOIN LineItems ON ItemDesc.ItemCode = LineItems.ItemCode" +
                    " WHERE LineItems.InvoiceNum = " + invoiceNum;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes a line item in an invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string DeleteLineItems(string invoiceNum)
        {
            try
            {
                string sSQL = "DELETE From LineItems WHERE InvoiceNum = " + invoiceNum;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes an invoice. To be used in conjunction with DeleteLineItems()
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string DeleteInvoices(string invoiceNum)
        {
            try
            {
                string sSQL = "DELETE From Invoices WHERE InvoiceNum = " + invoiceNum;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserts a line item
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string InsertLineItem(string invoiceNum, string lineItemNum, string itemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values(" + invoiceNum + ", " + lineItemNum + ", \'" + itemCode + "\')";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserts an invoice.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public string InsertInvoice(string invoiceDate, string totalCost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#" + invoiceDate + "#, " + totalCost + ")";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updatesthe total cost of an invoice. To be used when line items are added.
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string UpdateTotalCost(string totalCost, string invoiceNum)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET TotalCost = " + totalCost + " WHERE InvoiceNum = " + invoiceNum;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the last inserted invoice number.
        /// </summary>
        /// <returns></returns>
        public string GetLastInvoiceNumber()
        {
            try
            {
                string sSQL = "SELECT MAX(InvoiceNum) FROM Invoices";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the invoice date in the db.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public string UpdateInvoiceDate(string invoiceDate, string invoiceNum)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET InvoiceDate = #" + invoiceDate + "# WHERE InvoiceNum = " + invoiceNum;

                return sSQL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
