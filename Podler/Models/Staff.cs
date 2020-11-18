using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Podler.Models.Mangas;

namespace Podler.Models
{
    public class Staff : ModelBase
    {
        [Required(ErrorMessage = "O campo nome e obriatorio.")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Title { get; set; }

        [Required(ErrorMessage = "O campo role e obriatorio.")]
        public Role? Role { get; set; }

        public List<Manga> Mangas { get; set; }

        public Staff()
        {
            Mangas = new List<Manga>();
        }

        internal void Update(Staff staff)
        {
            Title = staff.Title;
            Role = staff.Role;
        }
    }

    public enum Role
    {
        Author = 0,
        Writer = 1
    }
}
