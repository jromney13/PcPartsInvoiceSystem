using PcPartsInvoiceSystem.Items;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPartsInvoiceSystem.Main
{
    class clsMainLogic
    {
        clsDataAccess db = new clsDataAccess();
        clsMainSQL sql = new clsMainSQL();

        public List<clsItem> GenerateItemList()
        {
            try
            {
                List<clsItem> itemList = new List<clsItem>();

                DataSet ds;

                int iRet = 0;

                ds = db.ExecuteSQLStatement(sql.SelectItemData(), ref iRet);

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
