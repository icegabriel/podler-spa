using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Podler.Data;
using Podler.Models.Chapters;
using Podler.Models.ImagePages;
using Podler.Services;

namespace Podler.Repositories
{
    public class ImagePagesRepository : RepositoryBase<ImagePage>, IImagePagesRepository
    {
        private readonly IFileService _fileService;

        public ImagePagesRepository(ApplicationContext Context, IFileService fileService) : base(Context)
        {
            _fileService = fileService;
        }

        private async Task<ImagePage> SaveImagePageAsync(ImagePageUpload pageUpload, int chapterId)
        {
            var path = await _fileService.SaveChapterPageAsync(pageUpload.Image, chapterId);
            var imagePage = new ImagePage() { Number = pageUpload.Number, Path = path };

            return imagePage;
        }

        public async Task IncludeImagePageAsync(ImagePageUpload pageUpload, int chapterId)
        {
            var imagePage = await SaveImagePageAsync(pageUpload, chapterId);

            await IncludeAsync(imagePage);
        }

        public async Task IncludeImagePagesAsync(List<ImagePageUpload> pages, Chapter chapterDb)
        {
            var imagePages = new List<ImagePage>();

            foreach (var page in pages)
            {
                var imagePage = await SaveImagePageAsync(page, chapterDb.Id);

                imagePages.Add(imagePage);
            }

            chapterDb.Pages = imagePages;

            await Context.SaveChangesAsync();
        }

        public override async Task RemoveAsync(ImagePage imagePage)
        {
            await _fileService.RemoveChapterPageAsync(imagePage.Path);

            await base.RemoveAsync(imagePage);
        }
    }

    public interface IImagePagesRepository : IRepositoryBase<ImagePage>
    {
        Task IncludeImagePageAsync(ImagePageUpload pageUpload, int chapterId);
        Task IncludeImagePagesAsync(List<ImagePageUpload> pages, Chapter chapterDb);
    }
}
