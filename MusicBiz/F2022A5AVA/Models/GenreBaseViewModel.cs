using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2022A5AVA.Models
{
    public class GenreBaseViewModel
    {
        public int Id { get; set; }

        [StringLength(120)]
        public string Name { get; set; }
    }
}