using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Podler.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;

        private string CoversPath { get => _configuration["Storage:CoversPath"]; }

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
            {
                await cover.CopyToAsync(stream);
            }
        
            return "/images/covers/" + fileName;
        }
    }

    public interface IFileService
    {
        Task<string> SaveCoverAsync(IFormFile cover);
        Task RemoveCoverAsync(string coverPath);
    }
}
