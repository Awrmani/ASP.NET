using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class TrackAddViewModel
    {
        [StringLength(120)]
        [Display(Name = "Clerk who helps with album tasks")]
        public string Clerk { get; set; }

        [StringLength(120)]
        [Display(Name = "Composer name(s)")]
        public string Composers { get; set; }

        [Display(Name = "Track genre")]
        public int GenreId { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Range(1, Int32.MaxValue)]
        public int AlbumId { get; set; }
    }
}