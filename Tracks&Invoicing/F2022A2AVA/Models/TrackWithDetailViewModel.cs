using F2022A2AVA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A2AVA.Models
{
    public class TrackWithDetailViewModel : TrackBaseViewModel
    {
        [Required]
        public Album Album { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public MediaType MediaType { get; set; }

        
    }
}