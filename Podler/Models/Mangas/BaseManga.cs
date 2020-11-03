using System;
using System.ComponentModel.DataAnnotations;

namespace Podler.Models.Mangas
{
    public class BaseManga : ModelBase
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(40, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 1)]
        public string Title { get; set; }

        [Required(ErrorMessage = "O resumo é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 50)]
        public string Summary { get; set; }

        [Required(ErrorMessage = "A data de lançamento é obrigatória")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "O status é obrigatório")]
        public Status Status { get; set; }

        public BaseManga()
        {

        }
    }

    public enum Status
    {
        OnGoing = 1,
        Finished = 2,
        Canceled = 3,
        Paused = 4
    }
}