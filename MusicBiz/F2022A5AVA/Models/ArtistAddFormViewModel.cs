using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A5AVA.Models
{
    public class ArtistAddFormViewModel
    {
        public ArtistAddFormViewModel()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-22);
        }

        [StringLength(120)]
        [Display(Name = "If applicable, artist birth's name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthOrStartDate { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "URL to artist photo")]
        public string UrlArtist { get; set; }



        [Display(Name = "Artist's primary genre")]
        public SelectList GenreList { get; set; }
    }
}