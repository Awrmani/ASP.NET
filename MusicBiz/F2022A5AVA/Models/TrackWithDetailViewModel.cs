using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        public TrackWithDetailViewModel()
        {
            AlbumNames = new List<string>();
        }

        [Display(Name = "Number of albums with this track")]
        public string AlbumsCount { get; set; }

        [Display(Name = "Albums with this track")]
        public IEnumerable<string> AlbumNames { get; set; }
    }
}