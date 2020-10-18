using mvCommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Libraries.ShoppingCart
{
    public class CookieValueDeadlineFreight
    {
        private Cookie.Cookie _cookie;
        private string Key = "_cart.ValueDeadlineFreight";

        public CookieValueDeadlineFreight(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }

        public void Register(List<ValueDeadlineFreight> list)
        {
            var JSONstring = JsonConvert.SerializeObject(list);
            _cookie.Register(Key, JSONstring);
        }

        public List<ValueDeadlineFreight> Consult()
        {
            if (_cookie.isExisting(Key))
            {
                string value = _cookie.Consult(Key);
                return JsonConvert.DeserializeObject<List<ValueDeadlineFreight>>(value);
            }
            else
            {
                //TODO - ANALISAR MELHOR RETORNO
                return null;
            }
        }

        public void Remove()
        {
            _cookie.Remove(Key);
        }
    }
}
