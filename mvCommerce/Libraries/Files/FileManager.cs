using Microsoft.AspNetCore.Http;
using mvCommerce.Models;
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

            using (var stream = new FileStream(path, FileMode.Create))
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

        public static List<Image> MoveProductImage(List<string> listTemporaryPath, int productId)
        {
            /**
             * Create product folder
             */
            var definitivePathProductFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", productId.ToString());
            if (!Directory.Exists(definitivePathProductFolder))
            {
                Directory.CreateDirectory(definitivePathProductFolder);
            }

            /**
             *Move image to definitive folder
             */
            List<Image> listImagesDefinitives = new List<Image>();
            foreach (var pathTemp in listTemporaryPath)
            {
                if (!string.IsNullOrEmpty(pathTemp))
                {
                    var fileName = Path.GetFileName(pathTemp);
                    var pathDef = Path.Combine("/uploads", productId.ToString(), fileName).Replace("\\", "/");

                    if (pathDef != pathTemp)
                    {
                        var pathAbsoluteTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", fileName);
                        var pathAbsoluteDefinitive = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", productId.ToString(), fileName);

                        if (File.Exists(pathAbsoluteTemp))
                        {
                            //Delete File in destiny path 
                            if (File.Exists(pathAbsoluteDefinitive))
                            {
                                File.Delete(pathAbsoluteDefinitive);
                            }
                            //Copy temporary folder to destiny
                            File.Copy(pathAbsoluteTemp, pathAbsoluteDefinitive);
                           
                            //Delete file of temporary folder
                            if (File.Exists(pathAbsoluteDefinitive))
                            {
                                File.Delete(pathAbsoluteTemp);
                            }
                            listImagesDefinitives.Add(new Image() { Path = Path.Combine("/uploads", productId.ToString(), fileName).Replace("\\", "/"), ProductId = productId });
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        listImagesDefinitives.Add(new Image() { Path = Path.Combine("/uploads", productId.ToString(), fileName).Replace("\\", "/"), ProductId = productId });
                    }
                }
            }
            return listImagesDefinitives;
        }

        public static void DeleteAllProductsImages(List<Image> listImage)
        {
            int productId = 0;
            foreach (var image in listImage)
            {
                DeleteProductImage(image.Path);
                productId = image.ProductId;
            }
            var folderProduct = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", productId.ToString());
            if (Directory.Exists(folderProduct))
            {
                Directory.Delete(folderProduct);
            }
        }
    }
}
