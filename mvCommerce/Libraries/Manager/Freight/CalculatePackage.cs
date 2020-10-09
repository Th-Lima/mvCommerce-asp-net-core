using mvCommerce.Models;
using mvCommerce.Models.ProductAggregator;
using System.Collections.Generic;

namespace mvCommerce.Libraries.Manager.Freight
{
    public class CalculatePackage
    {
        public List<Package> CalculatingPackage(List<ProductItem> products)
        {
            List<Package> packages = new List<Package>();

            Package package = new Package();
            foreach(var product in products)
            {
                for(int i = 0; i < product.AmountProductsCart; i++)
                {
                    var weight = package.Weight + product.Weight;
                    var length = (package.Length > product.Length) ? package.Length : product.Length;
                    var width = (package.Width > product.Width) ? package.Width : product.Width; ;
                    var height = package.Height + product.Height;
                    var dimension = length + width + height;

                    if(weight > 30 || dimension > 200)
                    {
                        packages.Add(package);
                        
                        package = new Package();
                    }

                   package.Weight += product.Weight;
                   package.Length = (package.Length > product.Length) ? package.Length : product.Length;
                   package.Width = (package.Width > product.Width) ? package.Width : product.Width; ;
                   package.Height += product.Height;
                }
            }
            packages.Add(package);

            return packages;
        }  
    }
}
