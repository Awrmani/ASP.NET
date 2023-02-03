using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A5AVA.Models
{
    public class AlbumAddFormViewModel
    {
        public AlbumAddFormViewModel()
        {
            ReleaseDate = DateTime.Now.AddYears(-22);
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "URL to album image (cover art)")]
        public string UrlAlbum { get; set; }



        [Display(Name = "Album's primary genre")]
        public SelectList GenreList { get; set; }

        [Display(Name = "All artists")]
        public MultiSelectList ArtistList { get; set; }

        [Display(Name = "All tracks")]
        public MultiSelectList TrackList { get; set; }

        public string ArtistName { get; set; }
    }
}