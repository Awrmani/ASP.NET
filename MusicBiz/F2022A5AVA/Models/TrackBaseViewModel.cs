using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class TrackBaseViewModel : TrackAddViewModel
    {
        [Required]
        [StringLength(120)]
        [Display(Name = "Track genre")]
        public string Genre { get; set; }

        public int Id { get; set; }
    }
}