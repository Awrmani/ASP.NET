using F2022A2AVA.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A2AVA.Models
{
    public class TrackBaseViewModel
    {
        #region Constructor

        public TrackBaseViewModel()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
            Playlists = new HashSet<Playlist>();
        }

        #endregion

        #region Columns

        [Key]
        public int TrackId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Track Name")]
        public string Name { get; set; }

        [StringLength(220)]
        [Display(Name = "Composer Name(s)")]
        public string Composer { get; set; }

        [Display(Name = "Length (ms)")]
        public int Milliseconds { get; set; }

        [Display(Name = "Size (bytes)")]
        public int? Bytes { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Price")]
        public decimal UnitPrice { get; set; }




        #endregion

        #region Entity Collections

        public ICollection<InvoiceLine> InvoiceLines { get; set; }

        public ICollection<Playlist> Playlists { get; set; }

        #endregion
    }
}