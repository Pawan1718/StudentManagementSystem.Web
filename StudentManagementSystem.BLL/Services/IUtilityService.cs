using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BLL.Interfaces
{
    public interface IUtilityService
    {
        Task<string> SaveImage(string ContainerName, IFormFile file);
        Task<string> EditImage(string ContainerName, IFormFile file, string dbPath);
        Task<string> DeleteImage(string ContainerName, string dbPath);
        Task<byte[]> DownloadCV(string dbPath);
    }
}