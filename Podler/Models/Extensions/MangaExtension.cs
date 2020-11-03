using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Podler.Models.Mangas;

namespace Podler.Models.Extensions
{
    public static class MangaExtension
    {
        public static Manga ToManga(
            this MangaUpload mangaUpload,
            string coverPath,
            List<Genre> genres,
            List<Theme> themes,
            List<Staff> staffs)
        {
            var manga = new Manga()
            {
                CoverPath = coverPath,
                Title = mangaUpload.Title,
                Summary = mangaUpload.Summary,
                ReleaseDate = mangaUpload.ReleaseDate,
                Status = mangaUpload.Status
            };

            genres.ForEach(genre => manga.IncludeGenre(genre));
            themes.ForEach(theme => manga.IncludeTheme(theme));
            staffs.ForEach(staff => manga.IncludeStaff(staff));

            return manga;
        }
    }
}
