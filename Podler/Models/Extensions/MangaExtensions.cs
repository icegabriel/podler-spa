using System.Collections.Generic;
using System.Linq;
using Podler.Models.Mangas;

namespace Podler.Models.Extensions
{
    public static class MangaExtensions
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
                Id = mangaUpload.Id,
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

        public static MangaApi ToApi(this Manga manga)
        {
            var genres = manga.Genres.Select(m => m.Genre).ToList();
            var themes = manga.Themes.Select(m => m.Theme).ToList();
            var staffs = manga.Staffs.Select(m => m.Staff).ToList();

            var mangaApi = new MangaApi()
            {
                Id = manga.Id,
                CoverPath = manga.CoverPath,
                Title = manga.Title,
                Summary = manga.Summary,
                ReleaseDate = manga.ReleaseDate,
                Status = manga.Status,
                Chapters = manga.Chapters,
                Genres = genres,
                Themes = themes,
                Staffs = staffs
            };

            return mangaApi;
        }

        public static List<MangaApi> ToApiList(this List<Manga> mangas)
        {
            return mangas.Select(manga => manga.ToApi()).ToList();
        }
    }
}
