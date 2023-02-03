using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class ArtistAddViewModel
    {
        public ArtistAddViewModel()
        {
            BirthOrStartDate = DateTime.Now.AddYears(-22);
        }

        [StringLength(120)]
        [Display(Name = "If applicable, artist birth's name")]
        public string BirthName { get; set; }

        [Required]
        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthOrStartDate { get; set; }

        [StringLength(120)]
        [Display(Name = "Executive who looks after this artist")]
        public string Executive { get; set; }

        public int GenreId { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }
    }
}