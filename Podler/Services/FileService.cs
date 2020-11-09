using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Podler.Models;

namespace Podler.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        private string CoversPath { get => _configuration["Storage:CoversPath"]; }
        private string ChaptersPath { get => _configuration["Storage:ChaptersPath"]; }

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task RemoveCoverAsync(string coverPath)
        {
            return Task.Factory.StartNew(() => {

                var fileName = Path.GetFileName(coverPath);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), CoversPath, fileName);

                File.Delete(filePath);
            });
        }

        public async Task<string> SaveCoverAsync(IFormFile cover)
        {
            var fileExtension = Path.GetExtension(cover.FileName);
            var fileName = $@"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), CoversPath, fileName);

            using (var stream = File.Create(filePath))
                await cover.CopyToAsync(stream);
        
            return $"/images/covers/{fileName}";
        }

        public async Task<string> SaveChapterPageAsync(IFormFile imagePage, int chapterId)
        {
            var fileExtension = Path.GetExtension(imagePage.FileName);
            var fileName = $@"{Guid.NewGuid()}{fileExtension}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), ChaptersPath, chapterId.ToString());
            
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, fileName);

            using (var stream = File.Create(filePath))
                await imagePage.CopyToAsync(stream);

            return $"/images/chapters/{chapterId}/{fileName}";
        }

        public async Task<List<ImagePage>> SaveChapterPageListAsync(List<IFormFile> imagePagesUpload, int chapterId)
        {
            var imagePages = new List<ImagePage>();

            for (int i = 0; i < imagePagesUpload.Count; i++)
            {
                var imagePage = new ImagePage();
                imagePage.Number = i;
                imagePage.Path = await SaveChapterPageAsync(imagePagesUpload[i], chapterId);

                imagePages.Add(imagePage);
            }

            return imagePages;
        }
    }

    public interface IFileService
    {
        Task<string> SaveCoverAsync(IFormFile cover);
        Task RemoveCoverAsync(string coverPath);
        Task<string> SaveChapterPageAsync(IFormFile imagePage, int chapterId);
        Task<List<ImagePage>> SaveChapterPageListAsync(List<IFormFile> imagePages, int chapterId);
    }
}
