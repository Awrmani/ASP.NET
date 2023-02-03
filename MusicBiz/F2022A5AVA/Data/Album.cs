using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Data
{
    public class Album
    {
        public Album()
        {
            Artists = new HashSet<Artist>();
            Tracks = new HashSet<Track>();

            ReleaseDate = DateTime.Now;
        }

        [Required]
        [StringLength(120)]
        public string Coordinator { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        [StringLength(500)]
        public string UrlAlbum { get; set; }


        public ICollection<Artist> Artists { get; set; }

        public ICollection<Track> Tracks { get; set; }
    }
}