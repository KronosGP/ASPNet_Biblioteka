using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Seriale
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Tytul { get; set; }

        [Display(Name = "Data Obejrzenia")]
        [DataType(DataType.Date)]
        public DateTime? DataObejrzenia { get; set; }
    }
}