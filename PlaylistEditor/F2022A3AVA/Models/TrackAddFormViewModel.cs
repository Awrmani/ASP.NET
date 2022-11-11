using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3AVA.Models
{
    public class TrackAddFormViewModel
    {
        [Required]
        [StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [StringLength(220)]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal UnitPrice { get; set; }





        [Display(Name = "Album")]
        public SelectList AlbumList { get; set; }

        [Display(Name = "Media Type")]
        public SelectList MediaTypeList { get; set; }

        [Display(Name = "Album")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Media Type")]
        public string MediaTypeName { get; set; }
    }
}