using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A3AVA.Models
{
    public class ArtistBaseViewModel
    {
        [Key]
        public int ArtistId { get; set; }

        [StringLength(120)]
        [Display(Name = "Artist Name")]
        public string Name { get; set; }
    }
}