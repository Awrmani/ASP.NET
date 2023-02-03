using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A5AVA.Models
{
    public class TrackAddFormViewModel
    {
        [StringLength(120)]
        [Display(Name = "Composer names (comma-seperated)")]
        public string Composers { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Range(1, Int32.MaxValue)]
        public int AlbumId { get; set; }



        public string AlbumName { get; set; }

        [Display(Name = "Track Genre")]
        public SelectList GenreList { get; set; }
    }
}