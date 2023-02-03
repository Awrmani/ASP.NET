using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Data
{
    public class Track
    {
        public Track()
        {
            Albums = new HashSet<Album>();
        }

        [Required]
        [StringLength(120)]
        public string Clerk { get; set; }

        [StringLength(120)]
        public string Composers { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public int Id { get; set; }

        public string Genre { get; set; }


        public ICollection<Album> Albums { get; set; }
    }
}