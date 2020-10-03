using mvCommerce.Models.ProductAggregator;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace mvCommerce.Libraries.ShoppingCart
{
    public class ShoppingCart
    {
        private Cookie.Cookie _cookie;
        private string Key = "_cart.shopping";

        public ShoppingCart(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }

        /*
         * CRUD
         */
        public void Register(ProductItem item)
        {
            List<ProductItem> list;
            if (_cookie.isExisting(Key))
            {
                list = Consult();
                var itemLocalized = list.SingleOrDefault(i => i.Id == item.Id);
                if (itemLocalized != null)
                {
                    list.Add(item);
                }
                else
                {
                    itemLocalized.Amount = itemLocalized.Amount++; 
                }
                list.Add(item);
            }
            else
            {
                list = new List<ProductItem>();
                list.Add(item);
            }

            Save(list);
        }
        public void Update(ProductItem item)
        {
            var list = Consult();
            var itemLocalized = list.SingleOrDefault(i => i.Id == item.Id);
            if (itemLocalized != null)
            {
                itemLocalized.Amount = item.Amount;
                Save(list);
            }
        }
        public void Remove(ProductItem item)
        {
            var list = Consult();
            var itemLocalized = list.SingleOrDefault(i => i.Id == item.Id);
            if(itemLocalized != null)
            {
                list.Remove(itemLocalized);
                Save(list);
            }
        }
        public List<ProductItem> Consult()
        {
            if (_cookie.isExisting(Key))
            {
                string value = _cookie.Consult(Key);
                return JsonConvert.DeserializeObject<List<ProductItem>>(value);
            }
            else
            {
                return new List<ProductItem>();
            }
            
        }

        public void Save(List<ProductItem> list)
        {
            string value = JsonConvert.SerializeObject(list);
            _cookie.Register(Key, value);
        }

        public bool isExisting(string key)
        {
            if (_cookie.isExisting(Key))
                return false;

            return true;
        }
        public void RemoveAll()
        {
            _cookie.Remove(Key);
        }
    }
}
