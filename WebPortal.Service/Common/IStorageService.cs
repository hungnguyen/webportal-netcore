using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Services.Common
{
    public interface IStorageService
    {
        string GetAbsoluteUrl(string fileName);
        string GetFileUrl(string fileName);

        Task<string> SaveFileAsync(Stream mediaBinaryStream, string fileName, string folder);

        Task<string> SaveFileAsync(Stream mediaBinaryStream, string fileName);

        Task DeleteFileAsync(string fileName);

        Task<string> SaveFileAsync(IFormFile file);

        Task<string> SaveFileAsync(IFormFile file, string fileNameSaveAs);

        Task<string> SaveFileAsync(IFormFile file, string fileNameSaveAs, string folderSaveAs);

        string ReplaceRelativeUrl(string searchString, string replaceString);

        string GetPhysicalPath(string relativePath);
    }
}
