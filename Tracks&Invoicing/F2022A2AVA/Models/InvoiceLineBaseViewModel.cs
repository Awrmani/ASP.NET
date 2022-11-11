using F2022A2AVA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A2AVA.Models
{
    public class InvoiceLineBaseViewModel
    {
        #region Columns

        [Key]
        [Display(Name = "Line ID")]
        public int InvoiceLineId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal LinePrice
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }

        #endregion

        #region Navigation Properties

        public Invoice Invoice { get; set; }

        #endregion
    }
}