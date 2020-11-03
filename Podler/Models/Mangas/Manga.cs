using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Podler.Models.Mangas
{
    public class Manga : BaseManga
    {
        [Required]
        public string CoverPath { get; set; }

        public List<Chapter> Chapters { get; set; }
        public List<MangaGenre> Genres { get; set; }
        public List<MangaTheme> Themes { get; set; }
        public List<MangaStaff> Staffs { get; set; }

        public Manga()
        {
            Chapters = new List<Chapter>();
            Genres = new List<MangaGenre>();
            Themes = new List<MangaTheme>();
            Staffs = new List<MangaStaff>();
        }

        internal void IncludeGenre(Genre genre)
        {
            Genres.Add(new MangaGenre() { Genre = genre });
        }

        internal void IncludeTheme(Theme theme)
        {
            Themes.Add(new MangaTheme() { Theme = theme });
        }

        internal void IncludeStaff(Staff staff)
        {
            Staffs.Add(new MangaStaff() { Staff = staff });
        }
    }
}
