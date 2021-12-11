using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPartsInvoiceSystem.Search
{
    /// <summary>
    /// Returns SQL results
    /// </summary>
    class clsSearchSQL
    {
        /// <summary>
        /// SQL string for getting results
        /// </summary>
        private String sql;
        /// <summary>
        /// Constructor that recieves an sql statement as a string
        /// </summary>
        /// <param name="sql"></param>
        public clsSearchSQL(String sql) 
        {
            this.sql = sql;
        }
        /// <summary>
        /// Runs sql statement and returns List list of strings which is just a list of the rows from the db
        /// </summary>
        /// <returns></returns>
        public List<List<String>> GetData() 
        {
            List<List<String>> dataList = new List<List<String>>();
            List<String> tempList;
            String temp;
            String[] tempArr;
            String conString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + @"..\..\..\Invoice.mdb";
            using (OleDbConnection con = new OleDbConnection(conString))
            {
                OleDbCommand cmd = new OleDbCommand(sql, con);
                con.Open();
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tempArr = new string[2];
                        tempList = new List<string>();
                        tempList.Add(reader["InvoiceNum"].ToString());
                        temp = reader["InvoiceDate"].ToString();
                        tempArr = temp.Split(' ');
                        tempList.Add(tempArr[0]);
                        tempList.Add(reader["TotalCost"].ToString());
                        dataList.Add(tempList);
                    }
                }
            }
            return dataList;
        }
    }
}
