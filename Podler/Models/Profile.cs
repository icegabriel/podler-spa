using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Podler.Models.Mangas;

namespace Podler.Models
{
    public class Profile : ModelBase
    {
        [Required]
        public string UserId { get; private set; }

        public List<Manga> Favorites { get; set; }

        public Profile()
        {
            Favorites = new List<Manga>();
        }
    }
}
