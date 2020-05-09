using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvCommerce.Libraries.Session
{
    public class Session
    {
        //Dependancy Injection
        IHttpContextAccessor _context;
        public Session(IHttpContextAccessor context)
        {
            _context = context;
        }

        //CRUD 
        public void Register(string key, string value)
        {
            _context.HttpContext.Session.SetString(key, value);
        }
        public void Update(string key, string value)
        {
            if (isExisting(key)) 
            {
                _context.HttpContext.Session.Remove(key); 
            }
            
            _context.HttpContext.Session.SetString(key, value);
        }
        public void Remove(string key)
        {
            _context.HttpContext.Session.Remove(key);
        }
        public string Consult(string key)
        {
            return _context.HttpContext.Session.GetString(key);
        }
        public bool isExisting(string key)
        {
           if(_context.HttpContext.Session.GetString(key) == null) { return false; }
           
           return true;
        }
        public void RemoveAll()
        {
            _context.HttpContext.Session.Clear();
        }
    }
}
