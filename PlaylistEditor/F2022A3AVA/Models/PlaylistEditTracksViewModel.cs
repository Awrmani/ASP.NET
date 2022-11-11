using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A3AVA.Models
{
    public class PlaylistEditTracksViewModel
    {
        PlaylistEditTracksViewModel()
        {
            TrackIds = new List<int>();
        }

        [Key]
        public int PlaylistId { get; set; }

        public IEnumerable<int> TrackIds { get; set; }
    }
}