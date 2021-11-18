using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPartsInvoiceSystem
{
    public class clsInvoice
    {
        /// <summary>
        /// The Invoice Number
        /// </summary>
        public string sInvoiceNum { get; set; }
        
        /// <summary>
        /// The Invoice Date
        /// </summary>
        public string sInvoiceDate { get; set; }
        
        /// <summary>
        /// The Total Cost
        /// </summary>
        public string sTotalCost { get; set; }
    }
}
