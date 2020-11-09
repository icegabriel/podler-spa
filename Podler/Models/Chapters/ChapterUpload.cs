using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Podler.Models.Chapters
{
    public class ChapterUpload : ChapterBase
    {
        public List<IFormFile> Pages { get; set; }

        public ChapterUpload()
        {
            Pages = new List<IFormFile>();
        }
    }
}
