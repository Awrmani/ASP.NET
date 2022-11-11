using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A1AVA.Models
{
    public class VenueAddViewModel
    {
        public VenueAddViewModel()
        {
            Company = "Cyan Creations Co.";
            Address = "1218 Maple Street";
            City = "North York";
            State = "ON";
            Country = "Canada";
            PostalCode = "M4T 2W3";
            Phone = "+1 (437) 904-8043";
            Fax = "+1 (437) 203-6985";
            Email = "armanvalaee@gmail.com";
            Website = "https://www.cyancrane.ca";
            OpenDate = DateTime.Now.AddYears(-22);
        }

        [Required]
        [StringLength(80)]
        public string Company { get; set; }

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

        [Display(Name = "Date Opened")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpenDate { get; set; }
    }
}