using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using WebPortal.Services.Extensions;
using WebPortal.Utilities.Options;

namespace WebPortal.Services.Common
{
    public class StorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "upload";
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AppSettingsOption _appSettings;
        public StorageService(IWebHostEnvironment webHostEnvironment, IOptions<AppSettingsOption> appSettings)
        {
            _userContentFolder = !string.IsNullOrEmpty(appSettings.Value.UploadRootFolder) ? appSettings.Value.UploadRootFolder : Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            _appSettings = appSettings.Value;
            this.webHostEnvironment = webHostEnvironment;
        }

        public string GetFileUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = "noimage.png";
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task<string> SaveFileAsync(Stream mediaBinaryStream, string fileName, string folder)
        {
            string path = _userContentFolder;
            if (!string.IsNullOrEmpty(folder))
            {
                path = Path.Combine(path, folder);
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            //check if folder is existing
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            //check if file is existing
            var saveAsName = fileName;
            var filePath = Path.Combine(path, saveAsName);
            int i = 1;
            while (File.Exists(filePath))
            {
                saveAsName = $"{i}-{fileName}";
                filePath = Path.Combine(path, saveAsName);
                i++;
            }

            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
            return saveAsName;
        }

        public async Task<string> SaveFileAsync(Stream mediaBinaryStream, string fileName)
            => await SaveFileAsync(mediaBinaryStream, fileName, null);

        public async Task DeleteFileAsync(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var filePath = Path.Combine(_userContentFolder, fileName);
                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                }
            }
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Path.GetFileNameWithoutExtension(originalFileName).GetUrlName()}{Path.GetExtension(originalFileName)}";
            fileName = await SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string fileNameSaveAs)
            => await SaveFileAsync(file.OpenReadStream(), $"{fileNameSaveAs}{Path.GetExtension(file.FileName)}");

        public async Task<string> SaveFileAsync(IFormFile file, string fileNameSaveAs, string folderSaveAs)
            => await SaveFileAsync(file.OpenReadStream(), $"{fileNameSaveAs}{Path.GetExtension(file.FileName)}", folderSaveAs);

        public string GetAbsoluteUrl(string fileName)
        {
            return $"{_appSettings.ImageBaseAddess}/{fileName}";
        }

        public string ReplaceRelativeUrl(string searchString, string inputString)
        {
            return inputString.Replace(searchString, _appSettings.ImageBaseAddess);
        }
        public string GetPhysicalPath(string relativePath)
        {
            return Path.Combine(webHostEnvironment.ContentRootPath, relativePath);
        }
    }
}
