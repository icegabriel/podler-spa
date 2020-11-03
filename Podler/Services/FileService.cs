using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Podler.Services
{
    public class FileService : IFileService
    {
        public async Task<string> SaveCoverAsync(IFormFile cover)
        {
            if (cover != null && cover.Length > 0)
            {
                var fileExtension = Path.GetExtension(cover.FileName);
                var fileName = $@"{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/covers", fileName);

                using (var stream = File.Create(filePath))
                {
                    await cover.CopyToAsync(stream);
                }
            
                return "/images/covers/" + fileName;
            }

            return null;
        }
    }

    public interface IFileService
    {
        Task<string> SaveCoverAsync(IFormFile cover);
    }
}
