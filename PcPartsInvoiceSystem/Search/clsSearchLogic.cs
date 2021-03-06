using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PcPartsInvoiceSystem.Search
{
    /// <summary>
    /// Logic for the Search Window
    /// </summary>
    class clsSearchLogic
    {
        /// <summary>
        /// String of what the invoice date that is selected
        /// </summary>
        private String invoiceDate;
        /// <summary>
        /// int of invoice cost that is selected, if none selected value is -1
        /// </summary>
        private double invoiceCost;
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
            try
            {
                bool mult = false;
                String sql = "SELECT * FROM Invoices";
                if (!invoiceDate.Equals(String.Empty))
                {
                    sql += " WHERE (InvoiceDate = #" + invoiceDate + "#)";
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
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// sets invoiceNum
        /// </summary>
        /// <param name="invoiceNum"></param>
        public void UpdateInvoiceNum(int invoiceNum) 
        {
            try
            {
                this.invoiceNum = invoiceNum;
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// sets invoiceDate
        /// </summary>
        /// <param name="invoiceDate"></param>
        public void UpdateInvoiceDate(String invoiceDate) 
        {
            try
            {
                this.invoiceDate = invoiceDate;
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// sets invoice cost
        /// </summary>
        /// <param name="invoiceCost"></param>
        public void UpdateInvoiceCost(double invoiceCost) 
        {
            try
            {
                this.invoiceCost = invoiceCost;
            }
            catch (Exception ex)
            {
                //Low Level Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
