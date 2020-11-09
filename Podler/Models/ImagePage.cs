using System.ComponentModel.DataAnnotations;
using Podler.Models.Chapters;

namespace Podler.Models
{
    public class ImagePage : ModelBase
    {
        [Required]
        public string Path { get; set; }

        [Required]
        public int Number { get; set; }

        public Chapter Chapter { get; set; }
    }
}
