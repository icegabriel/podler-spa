using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Podler.Data;
using Podler.Models.Chapters;
using Podler.Models.Mangas;
using Podler.Services;

namespace Podler.Repositories
{
    public class ChaptersRepository : RepositoryBase<Chapter>, IChaptersRepository
    {
        private readonly IFileService _fileService;
        private readonly IImagePagesRepository _imagePagesRepository;

        public ChaptersRepository(ApplicationContext Context,
                                  IFileService fileService,
                                  IImagePagesRepository imagePagesRepository) : base(Context)
        {
            _fileService = fileService;
            _imagePagesRepository = imagePagesRepository;
        }

        public async override Task<Chapter> GetAsync(int id)
        {
            return await DbSet.Where(c => c.Id == id)
                              .Include(c => c.Manga)
                              .Include(c => c.Pages)
                              .SingleOrDefaultAsync();
        }

        public async Task<Chapter> IncludeChapterAsync(ChapterUpload chapterUpload, Manga mangaDb)
        {
            var chapter = new Chapter(){

                Title = chapterUpload.Title,
                Number = chapterUpload.Number,
                ReleaseDate = chapterUpload.ReleaseDate
            };

            mangaDb.Chapters.Add(chapter);

            await Context.SaveChangesAsync();

            await _imagePagesRepository.IncludeImagePagesAsync(chapterUpload.Pages, chapter);
            
            return chapter;
        }

        public async Task UpdateChapterAsync(ChapterUpload chapterUpload, Chapter chapterDb)
        {
            if (chapterUpload.Pages.Count > 0)
            {
                
            }
        }
    }

    public interface IChaptersRepository : IRepositoryBase<Chapter>
    {
        Task<Chapter> IncludeChapterAsync(ChapterUpload chapterUpload, Manga mangaDb);
        Task UpdateChapterAsync(ChapterUpload chapterUpload, Chapter chapterDb);
    }
}
