using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A3AVA.Models
{
    public class PlaylistEditTracksFormViewModel
    {
        PlaylistEditTracksFormViewModel()
        {
            Tracks = new List<TrackBaseViewModel>();
        }


        [Key]
        public int PlaylistId { get; set; }

        [StringLength(120)]
        [Display(Name = "Playlist Name")]
        public string Name { get; set; }

        [Display(Name = "All tracks")]
        public MultiSelectList TrackList { get; set; }

        public int TracksCount { get; set; }

        [Display(Name = "Now on playlist")]
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}