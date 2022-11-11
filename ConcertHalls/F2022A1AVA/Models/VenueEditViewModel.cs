using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A1AVA.Models
{
    public class VenueEditViewModel
    {
        [Key]
        public int VenueId { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [Display(Name = "Province")]
        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(60)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        [StringLength(60)]
        public string Website { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Date Opened")]
        public DateTime? OpenDate { get; set; }

        [Display(Name = "Sign-up Password")]
        [StringLength(60)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Promo Code")]
        [RegularExpression(@"[0-9]{3}[A-Z]{2}", ErrorMessage = "Promo Code should be in the NNNLL format where N represents a number (0-9) and L represents a capital Letter.")]
        [StringLength(5, MinimumLength = 5)]
        public string PromoCode { get; set; }

        [Range(typeof(int), "1", "40000", ErrorMessage = "Venue Capacity should be between 1 to 40,000.")]
        public int Capacity { get; set; }
    }
}