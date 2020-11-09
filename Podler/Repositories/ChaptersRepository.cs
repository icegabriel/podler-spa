using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Podler.Data;
using Podler.Models.Chapters;
using Podler.Models.Mangas;
using Podler.Services;

namespace Podler.Repositories
{
    public class ChaptersRepository : RepositoryBase<Chapter>, IChaptersRepository
    {
        private readonly IFileService _fileService;

        public ChaptersRepository(ApplicationContext Context,
                                  IFileService fileService) : base(Context)
        {
            _fileService = fileService;
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

            chapter.Pages = await _fileService.SaveChapterPageListAsync(chapterUpload.Pages, chapter.Id);

            await Context.SaveChangesAsync();

            return chapter;
        }
    }

    public interface IChaptersRepository : IRepositoryBase<Chapter>
    {
        Task<Chapter> IncludeChapterAsync(ChapterUpload chapterUpload, Manga mangaDb);
    }
}
