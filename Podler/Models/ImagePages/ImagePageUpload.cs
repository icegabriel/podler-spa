using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Podler.Models.ImagePages
{
    public class ImagePageUpload : ModelBase
    {
        [Required(ErrorMessage = "A imagem e obrigatoria")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "O numero da pagina e obrigatorio")]
        public int? Number { get; set; }
    }
}
