using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using StudentManagementSystem.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Implementations
{
    public class UtilityService : IUtilityService
    {
        private IWebHostEnvironment _environment;
        private IHttpContextAccessor _httpContextAccessor;

        public UtilityService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> DeleteImage(string ContainerName, string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                return null;
            }
            var fileName = Path.GetFileName(dbPath);
            var completePath = Path.Combine(_environment.WebRootPath, ContainerName, fileName);
            if (File.Exists(completePath))
            {
                File.Delete(completePath);
            }
            return Task.FromResult(dbPath);
        }

        public async Task<byte[]> DownloadCV(string dbPath)
        {
            if (File.Exists(dbPath))
            {
              string filePath = Path.Combine(_environment.WebRootPath, dbPath); 
                return await File.ReadAllBytesAsync(dbPath);
            }
            else
            {
                throw new FileNotFoundException("The CV file does not exist.", dbPath);
            }
        }

        public async Task<string> EditImage(string ContainerName, IFormFile file, string dbPath)
        {
            await DeleteImage(ContainerName, dbPath);
            return await SaveImage(ContainerName, file);
        }

        public async Task<string> SaveImage(string ContainerName, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_environment.WebRootPath, ContainerName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string filePath = Path.Combine(folder, fileName);
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                // Write the content to the file
                await File.WriteAllBytesAsync(filePath, content);
            }
            var basePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var completePath = Path.Combine(basePath, ContainerName, fileName).Replace("\\", "/");
            return completePath;
        }
    }
}