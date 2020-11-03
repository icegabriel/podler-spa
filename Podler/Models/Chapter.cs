using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Podler.Models.Mangas;

namespace Podler.Models
{
    public class Chapter : ModelBase
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Title { get; set; }

        [Required(ErrorMessage = "O numero do captulo é obrigatório")]
        public decimal Number { get; set; }
        
        [Required(ErrorMessage = "A data de lançamento é obrigatória")]
        public DateTime ReleaseDate { get; set; }

        public List<ImagePage> Pages { get; set; }
        
        public Manga Manga { get; set; }

        public Chapter()
        {
            Pages = new List<ImagePage>();
        }
    }
}
