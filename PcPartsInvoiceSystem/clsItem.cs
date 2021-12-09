using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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

        /// <summary>
        /// Empty Constructor w/ no setters
        /// </summary>
        public clsItem()
        {

        }

        /// <summary>
        /// Constructor that sets the item properties
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDescription"></param>
        /// <param name="sItemCost"></param>
        public clsItem(string sItemCode, string sItemDescription, string sItemCost)
        {
            this.sItemCode = sItemCode;
            this.sItemDescription = sItemDescription;
            this.sItemCost = sItemCost;
        }

        /// <summary>
        /// Overrides ToString() to display an item's description
        /// "First Last"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            try
            {
                return sItemDescription;
            }
            catch (Exception ex)
            {
                //Calling Method: Throw error
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
