using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A3AVA.Models
{
    public class MediaTypeBaseViewModel
    {
        [Key]
        public int MediaTypeId { get; set; }

        [StringLength(120)]
        [Display(Name = "Media Type")]
        public string Name { get; set; }
    }
}