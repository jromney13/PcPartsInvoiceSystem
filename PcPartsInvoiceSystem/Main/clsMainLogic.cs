﻿using PcPartsInvoiceSystem.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        /// Generates list of all items in the database
        /// </summary>
        /// <returns></returns>
        public List<clsItem> GenerateItemList()
        {
            try
            {
                List<clsItem> itemList = new List<clsItem>();

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

    }
}
