using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<string> MoveProductImage(List<string> listTemporaryPath, string productId)
        {
            /**
             * Create product folder
             */
            var definitivePathProductFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", productId);
            if (!Directory.Exists(definitivePathProductFolder))
            {
                Directory.CreateDirectory(definitivePathProductFolder);
            }

            /**
             *Move image to definitive folder
             */
            List<string> ListPathDefinitive = new List<string>();
            foreach (var pathTemp in listTemporaryPath)
            {
                if (!string.IsNullOrEmpty(pathTemp))
                {

                    var fileName = Path.GetFileName(pathTemp);
                    var pathAbsoluteTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", pathTemp);
                    var pathAbsoluteDefinitive = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uplodas", productId, fileName);

                    if (File.Exists(pathAbsoluteTemp))
                    {
                        File.Copy(pathAbsoluteTemp, pathAbsoluteDefinitive);
                        if (File.Exists(pathAbsoluteDefinitive))
                        {
                            File.Delete(pathAbsoluteTemp);
                        }
                        ListPathDefinitive.Add(Path.Combine("/uplodas", productId, fileName).Replace("\\", "/"));
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return ListPathDefinitive;
        }
    }
}
