using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Podler.Data;
using Podler.Models.Extensions;
using Podler.Models.Mangas;
using Podler.Services;

namespace Podler.Repositories
{
    public class MangasRepository : RepositoryBase<Manga>, IMangasRepository
    {
        private readonly IGenresRepository _genresRepository;
        private readonly IThemesRepository _themesRepository;
        private readonly IStaffsRepository _staffsRepository;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;

        private string DefaultCoverPath { get => _configuration["Storage:DefaultCoverPath"]; }

        public MangasRepository(ApplicationContext Context,
                                IGenresRepository genresRepository,
                                IThemesRepository themesRepository,
                                IStaffsRepository staffsRepository,
                                IFileService fileService,
                                IConfiguration configuration) : base(Context)
        {
            _genresRepository = genresRepository;
            _themesRepository = themesRepository;
            _staffsRepository = staffsRepository;
            _fileService = fileService;
            _configuration = configuration;
        }

        public async Task<Manga> GetByTitleAsync(string title)
        {
            return await DbSet.Where(m => m.Title.ToUpper() == title.ToUpper()).FirstOrDefaultAsync();
        }

        public async Task<Manga> IncludeMangaAsync(MangaUpload mangaUpload)
        {
            if (mangaUpload.Cover != null && mangaUpload.Cover.Length > 0)
            {
                var coverPath = await _fileService.SaveCoverAsync(mangaUpload.Cover);
                var manga = await ToMangaHelperAsync(mangaUpload, coverPath);

                await IncludeAsync(manga);

                return manga;
            }
            else
            {
                var manga = await ToMangaHelperAsync(mangaUpload, DefaultCoverPath);
                await IncludeAsync(manga);

                return manga;
            }
        }

        public async Task UpdateMangaAsync(MangaUpload mangaUpload, Manga mangaDb)
        {
            var coverPath = "";

            if (mangaUpload.Cover != null && mangaUpload.Cover.Length > 0)
            {
                if (mangaDb.CoverPath != DefaultCoverPath)
                    await _fileService.RemoveCoverAsync(mangaDb.CoverPath);

                coverPath = await _fileService.SaveCoverAsync(mangaUpload.Cover);
            }
            else
            {
                coverPath = mangaDb.CoverPath;
            }

            var manga = await ToMangaHelperAsync(mangaUpload, coverPath);

            mangaDb.Update(manga);

            await UpdateAsync(mangaDb);
        }

        private async Task<Manga> ToMangaHelperAsync(MangaUpload mangaUpload, string coverPath)
        {
            var genres = await this._genresRepository.FindAsync(mangaUpload.Genres.ToArray());
            var themes = await this._themesRepository.FindAsync(mangaUpload.Themes.ToArray());
            var staffs = await this._staffsRepository.FindAsync(mangaUpload.Staffs.ToArray());

            return mangaUpload.ToManga(coverPath, genres, themes, staffs);
        }

        public async override Task RemoveAsync(Manga manga)
        {
            if (manga.CoverPath != DefaultCoverPath)
                await _fileService.RemoveCoverAsync(manga.CoverPath);

            DbSet.Remove(manga);
            await Context.SaveChangesAsync();
        }
    }

    public interface IMangasRepository : IRepositoryBase<Manga>
    {
        Task<Manga> GetByTitleAsync(string title);
        Task<Manga> IncludeMangaAsync(MangaUpload manga);
        Task UpdateMangaAsync(MangaUpload mangaUpload, Manga mangaDb);
    }
}
