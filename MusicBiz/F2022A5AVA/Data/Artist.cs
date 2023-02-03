using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Data
{
    public class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
            BirthOrStartDate = DateTime.Now;
        }

        [StringLength(120)]
        public string BirthName { get; set; }

        [Required]
        public DateTime BirthOrStartDate { get; set; }

        [Required]
        [StringLength(120)]
        public string Executive { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Genre { get; set; }

        [StringLength(500)]
        public string UrlArtist { get; set; }


        public ICollection<Album> Albums { get; set; }
    }
}