using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Podler.Models.ImagePages;

namespace Podler.Models.Chapters
{
    public class ChapterUpload : ChapterBase
    {
        public List<ImagePageUpload> Pages { get; set; }
        
        public int? MangaId { get; set; }

        public ChapterUpload()
        {
            Pages = new List<ImagePageUpload>();
        }
    }
}
