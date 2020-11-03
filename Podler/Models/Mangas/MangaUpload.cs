using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Podler.Models.Mangas
{
    public class MangaUpload : BaseManga
    {
        public IFormFile Cover { get; set; }
        public List<int> Genres { get; set; }
        public List<int> Themes { get; set; }
        public List<int> Staffs { get; set; }

        public MangaUpload()
        {
            Genres = new List<int>();
            Themes = new List<int>();
            Staffs = new List<int>();
        }
    }
}
