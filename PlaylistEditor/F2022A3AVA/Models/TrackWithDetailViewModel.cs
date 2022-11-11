using F2022A3AVA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A3AVA.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        [Display(Name = "Media Type")]
        public MediaType MediaType { get; set; }

        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }

        [Display(Name = "Artist Name")]
        public string AlbumArtistName { get; set; }
    }
}