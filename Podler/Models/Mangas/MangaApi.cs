using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Podler.Models.Chapters;

namespace Podler.Models.Mangas
{
    public class MangaApi : BaseManga
    {
        [Required]
        public string CoverPath { get; set; }

        public List<Chapter> Chapters { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Theme> Themes { get; set; }
        public List<Staff> Staffs { get; set; }

        public MangaApi()
        {
            Chapters = new List<Chapter>();
            Genres = new List<Genre>();
            Themes = new List<Theme>();
            Staffs = new List<Staff>();
        }
    }
}
