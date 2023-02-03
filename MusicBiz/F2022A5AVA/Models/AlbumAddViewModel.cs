using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class AlbumAddViewModel
    {
        public AlbumAddViewModel()
        {
            ReleaseDate = DateTime.Now.AddYears(-22);
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }

        [StringLength(120)]
        [Display(Name = "Coordinator who looks after the album")]
        public string Coordinator { get; set; }

        [Display(Name = "Album's primary genre")]
        public int GenreId { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Album name")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Album image (cover art)")]
        public string UrlAlbum { get; set; }


        public IEnumerable<int> ArtistIds { get; set; }

        public IEnumerable<int> TrackIds { get; set; }
    }
}