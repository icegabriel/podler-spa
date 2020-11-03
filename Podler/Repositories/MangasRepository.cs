using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public MangasRepository(ApplicationContext Context,
                                IGenresRepository genresRepository,
                                IThemesRepository themesRepository,
                                IStaffsRepository staffsRepository,
                                IFileService fileService) : base(Context)
        {
            _genresRepository = genresRepository;
            _themesRepository = themesRepository;
            _staffsRepository = staffsRepository;
            _fileService = fileService;
        }

        public async Task<Manga> GetByTitleAsync(string title)
        {
            return await DbSet.Where(m => m.Title.ToUpper() == title.ToUpper()).FirstOrDefaultAsync();
        }

        public async Task<Manga> SaveMangaAsync(MangaUpload mangaUpload)
        {
            var genres = await this._genresRepository.FindAsync(mangaUpload.Genres.ToArray());
            var themes = await this._themesRepository.FindAsync(mangaUpload.Themes.ToArray());
            var staffs = await this._staffsRepository.FindAsync(mangaUpload.Staffs.ToArray());

            var coverPath = await _fileService.SaveCoverAsync(mangaUpload.Cover);

            if (coverPath != null)
            {
                var manga = mangaUpload.ToManga(coverPath, genres, themes, staffs);

                await IncludeAsync(manga);

                return manga;
            }

            throw new Exception("Error ao tentar upload da capa.");
        }
    }

    public interface IMangasRepository : IRepositoryBase<Manga>
    {
        Task<Manga> GetByTitleAsync(string title);
        Task<Manga> SaveMangaAsync(MangaUpload manga);
    }
}
