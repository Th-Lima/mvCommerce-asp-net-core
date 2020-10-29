using mvCommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Libraries.ShoppingCart
{
    public class CookieFreight
    {
        private Cookie.Cookie _cookie;
        private string Key = "_cart.valueFreight";

        public CookieFreight(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }

        /*
         * CRUD
         */
        public void Register(Freight freight)
        {
            List<Freight> list;
            if (_cookie.isExisting(Key))
            {
                list = Consult();
                var freightLocalized = list.SingleOrDefault(i => i.CEP == freight.CEP);
                if (freightLocalized == null)
                {
                    list.Add(freight);
                }
                else
                {
                    freightLocalized.ShoppingCartCode = freight.ShoppingCartCode;
                    freightLocalized.ListValues = freight.ListValues;
                }
            }
            else
            {
                list = new List<Freight>();
                list.Add(freight);
            }

            Save(list);
        }
        public void Update(Freight freight)
        {
            var list = Consult();
            var freightLocalized = list.SingleOrDefault(i => i.CEP == freight.CEP);
            if (freightLocalized != null)
            {
                freightLocalized.ShoppingCartCode = freight.ShoppingCartCode;
                freightLocalized.ListValues = freight.ListValues;
                Save(list);
            }
        }
        public void Remove(Freight item)
        {
            var list = Consult();
            var itemLocalized = list.SingleOrDefault(i => i.CEP == item.CEP);
            if (itemLocalized != null)
            {
                list.Remove(itemLocalized);
                Save(list);
            }
        }
        public List<Freight> Consult()
        {
            if (_cookie.isExisting(Key))
            {
                string value = _cookie.Consult(Key);
                return JsonConvert.DeserializeObject<List<Freight>>(value);
            }
            else
            {
                return new List<Freight>();
            }

        }

        public void Save(List<Freight> list)
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
