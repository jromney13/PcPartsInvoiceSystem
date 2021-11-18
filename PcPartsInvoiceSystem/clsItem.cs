using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPartsInvoiceSystem.Items
{
    /// <summary>
    /// A class to store an item object
    /// </summary>
    public class clsItem
    {
        #region attributes

        /// <summary>
        /// The Item Code
        /// </summary>
        public string sItemCode { get; set; }
        
        /// <summary>
        /// The Item Description
        /// </summary>
        public string sItemDescription { get; set; }
        
        /// <summary>
        /// The Item Cost
        /// </summary>
        public string sItemCost { get; set; }

        #endregion
    }
}
