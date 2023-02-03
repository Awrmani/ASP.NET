﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class ArtistWithDetailViewModel : ArtistBaseViewModel
    {
        public ArtistWithDetailViewModel()
        {
            Albums = new List<AlbumBaseViewModel>();
        }

        [Display(Name = "Number of albums")]
        public int AlbumsCount { get; set; }

        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }
    }
}