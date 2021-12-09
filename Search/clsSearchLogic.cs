using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPartsInvoiceSystem.Search
{
    class clsSearchLogic
    {
        /// <summary>
        /// String of what the invoice date that is selected
        /// </summary>
        private String invoiceDate;
        /// <summary>
        /// int of invoice cost that is selected, if none selected value is -1
        /// </summary>
        private int invoiceCost;
        /// <summary>
        /// int of invoice number that is selected, if none selected value is -1
        /// </summary>
        private int invoiceNum;
        /// <summary>
        /// Constructor that sets initial values of invoiceDate, invoiceCost, and invoiceNum
        /// </summary>
        public clsSearchLogic() 
        {
            invoiceDate = String.Empty;
            invoiceCost = -1;
            invoiceNum = -1;

        }
        /// <summary>
        /// Creates the sql statements based on if there are selected values or not
        /// </summary>
        /// <returns></returns>
        public List<List<String>> SQLGen() 
        {
            bool mult= false;
            String sql = "SELECT * FROM Invoices";
            if (!invoiceDate.Equals(String.Empty)) 
            {
                sql += " WHERE (InvoiceDate = #"+invoiceDate+"#)";
                mult = true;
            }
            if (invoiceCost >= 0)
            {
                if (mult)
                {
                    sql += " AND";
                }
                else
                {
                    sql += " WHERE";
                    mult = true;
                }
                sql += " (TotalCost = " + invoiceCost + ")";
            }
            if (invoiceNum >= 0) 
            {
                if (mult)
                {
                    sql += " AND";
                }
                else 
                {
                    sql += " WHERE";
                    mult = true;
                }
                sql += " (invoiceNum = " + invoiceNum + ")";
            }
            clsSearchSQL data = new clsSearchSQL(sql);
            return data.GetData();
        }
        /// <summary>
        /// sets invoiceNum
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void UpdateInvoiceNum(int invoiceNum) 
        {
            this.invoiceNum = invoiceNum;
        }
        /// <summary>
        /// sets invoiceDate
        /// </summary>
        /// <param name="invoiceDate"></param>
        public void UpdateInvoiceDate(String invoiceDate) 
        {
            this.invoiceDate = invoiceDate;
        }
        /// <summary>
        /// sets invoice cost
        /// </summary>
        /// <param name="invoiceCost"></param>
        public void UpdateInvoiceCost(int invoiceCost) 
        {
            this.invoiceCost = invoiceCost;
        }
    }
}
