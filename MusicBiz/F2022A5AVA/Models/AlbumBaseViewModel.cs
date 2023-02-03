using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class AlbumBaseViewModel : AlbumAddViewModel
    {
        [Required]
        [StringLength(120)]
        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }
        public int Id { get; set; }
    }
}