using Microsoft.AspNetCore.Http;
using System;

namespace mvCommerce.Libraries.Cookie
{
    public class Cookie
    {
        //Dependency Injection
        private IHttpContextAccessor _context;
        public Cookie(IHttpContextAccessor context)
        {
            _context = context;
        }

        //CRUD 
        public void Register(string key, string value)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);


            _context.HttpContext.Response.Cookies.Append(key, value, options);

        }
        public void Update(string key, string value)
        {
            if (isExisting(key))
            {
                Remove(key);
            }
            Update(key, value);
        }
        public void Remove(string key)
        {
            _context.HttpContext.Response.Cookies.Delete(key);
        }
        public string Consult(string key)
        {
            return _context.HttpContext.Request.Cookies[key];
        }
        public bool isExisting(string key)
        {
            if (_context.HttpContext.Request.Cookies[key] == null) 
             return false; 

            return true;
        }
        public void RemoveAll()
        {
            var cookiesList = _context.HttpContext.Request.Cookies;
            foreach(var cookie in cookiesList)
            {
                Remove(cookie.Key);
            }
        }
    }
}
