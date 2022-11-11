using F2022A3AVA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A3AVA.Models
{
    public class PlaylistWithDetailViewModel : PlaylistBaseViewModel
    {
        PlaylistWithDetailViewModel()
        {
            Tracks = new List<TrackBaseViewModel>();
        }
        [Display(Name = "Tracks on the Playlist")]
        public IEnumerable<TrackBaseViewModel> Tracks { get; set; }
    }
}