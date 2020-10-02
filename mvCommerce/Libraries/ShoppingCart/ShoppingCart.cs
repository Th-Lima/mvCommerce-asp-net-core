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
        public void Register(Item item)
        {
            List<Item> list;
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
                list = new List<Item>();
                list.Add(item);
            }

            Save(list);
        }
        public void Update(Item item)
        {
            var list = Consult();
            var itemLocalized = list.SingleOrDefault(i => i.Id == item.Id);
            if (itemLocalized != null)
            {
                itemLocalized.Amount = item.Amount;
                Save(list);
            }
        }
        public void Remove(Item item)
        {
            var list = Consult();
            var itemLocalized = list.SingleOrDefault(i => i.Id == item.Id);
            if(itemLocalized != null)
            {
                list.Remove(itemLocalized);
                Save(list);
            }
        }
        public List<Item> Consult()
        {
            if (_cookie.isExisting(Key))
            {
                string value = _cookie.Consult(Key);
                return JsonConvert.DeserializeObject<List<Item>>(value);
            }
            else
            {
                return new List<Item>();
            }
            
        }

        public void Save(List<Item> list)
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

    public class Item
    {
        public int? Id { get; set; }
        public int? Amount { get; set; }
    }
}
