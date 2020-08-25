using Microsoft.AspNetCore.Http;
using System.IO;

namespace mvCommerce.Libraries.Files
{
    public class FileManager
    {
        public static string RegisterProductImage(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", fileName);

            using(var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Path.Combine("/uploads/temp", fileName).Replace("\\", "/");
        }
        public static bool DeleteProductImage(string path)
        {
            string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.TrimStart('/'));
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
