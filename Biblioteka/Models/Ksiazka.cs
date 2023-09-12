using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Ksiazka
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Tytul { get; set; }
        
        [Required]
        [Display(Name = "Autor")]
        public string Autor { get; set; }
        
        [Display(Name ="Data Przeczytania")]
        [DataType(DataType.Date)]
        public DateTime? DataPrzeczytania { get; set; }
    }
}