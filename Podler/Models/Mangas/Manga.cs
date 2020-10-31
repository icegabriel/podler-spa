using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Podler.Models.Mangas
{
    public class Manga : BaseManga
    {
        [Required]
        public int CoverPath { get; set; }

        public List<Chapter> Chapters { get; set; }

        public Manga()
        {
            Chapters = new List<Chapter>();
        }
    }
}
