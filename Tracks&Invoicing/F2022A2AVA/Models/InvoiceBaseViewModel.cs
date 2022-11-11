using F2022A2AVA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A2AVA.Models
{
    public class InvoiceBaseViewModel
    {
        #region Columns

        [Key]
        public int InvoiceId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MMM d, yy}")]
        public DateTime InvoiceDate { get; set; }

        [StringLength(70)]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [StringLength(40)]
        [Display(Name = "City")]
        public string BillingCity { get; set; }

        [StringLength(40)]
        [Display(Name = "State")]
        public string BillingState { get; set; }

        [StringLength(40)]
        [Display(Name = "Country")]
        public string BillingCountry { get; set; }

        [StringLength(10)]
        [Display(Name = "Postal/Zip")]
        public string BillingPostalCode { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }

        #endregion
    }
}