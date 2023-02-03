using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class AlbumWithDetailViewModel : AlbumBaseViewModel
    {
        public AlbumWithDetailViewModel()
        {
            ArtistNames = new List<string>();
        }

        [Display(Name = "Number of tracks on this album")]
        public int TracksCount { get; set; }

        [Display(Name = "Number of artists on this album")]
        public int ArtistsCount { get; set; }

        [Display(Name = "Artists with this album")]
        public IEnumerable<string> ArtistNames { get; set; }
    }
}