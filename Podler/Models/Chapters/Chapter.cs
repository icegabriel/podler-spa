using System.Collections.Generic;
using Podler.Models.Mangas;

namespace Podler.Models.Chapters
{
    public class Chapter : ChapterBase
    {
        public List<ImagePage> Pages { get; set; }

        public Manga Manga { get; set; }
        
        public Chapter()
        {
            Pages = new List<ImagePage>();
        }
    }
}
