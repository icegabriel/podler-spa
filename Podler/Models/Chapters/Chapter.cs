using System.Collections.Generic;
using Podler.Models.ImagePages;
using Podler.Models.Mangas;

namespace Podler.Models.Chapters
{
    public class Chapter : ChapterBase
    {
        public List<ImagePage> Pages { get; set; }

        public int MangaId { get; set; }
        public Manga Manga { get; set; }
        
        public Chapter()
        {
            Pages = new List<ImagePage>();
        }
    }
}
