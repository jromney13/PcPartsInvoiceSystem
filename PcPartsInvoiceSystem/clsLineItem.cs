using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPartsInvoiceSystem
{
    public class clsLineItem
    {
        /// <summary>
        /// The Invoice Number
        /// </summary>
        public string sInvoiceNum { get; set; }
        
        /// <summary>
        /// The Line Item Number
        /// </summary>
        public string sLineItemNum { get; set; }
        
        /// <summary>
        /// The Item Code
        /// </summary>
        public string sItemCode { get; set; }
    }
}
