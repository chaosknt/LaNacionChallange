using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LaNacion.Common.Helpers.File
{
    public static class FileManager
    {

        public static string PathCombine(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        public static byte[] GetFileBytes(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                return stream.ToArray();
            }
        }

        public static async Task<string> SaveFileAsync(string serverPath, string fileName, string fileContentType, byte[] fileBytes)
        {
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return null;
            }

            string fileExtension = Path.GetExtension(fileName);
            var today = $"{DateTime.Now:yyyyMMdd}";
            string uniqueFileName = $"{Guid.NewGuid()}_{fileExtension}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), serverPath, today, uniqueFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await stream.WriteAsync(fileBytes);
            }

            return path;
        }

        public static void RemoveFileIfExists(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
