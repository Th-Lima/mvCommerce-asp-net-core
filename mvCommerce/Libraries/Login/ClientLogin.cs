using mvCommerce.Models;
using Newtonsoft.Json;

namespace mvCommerce.Libraries.Login
{
    public class ClientLogin
    {
        private Session.Session _session;
        private readonly string Key = "Login.Client";
        public ClientLogin(Session.Session session)
        {
            _session = session;
        }

        public void Login(Client client)
        {
            //Serialize 
            string clientJSONString = JsonConvert.SerializeObject(client);

            _session.Register(Key, clientJSONString);
        }
        
        public Client GetClient()
        {
            //Deserialize
            if(_session.isExisting(Key))
            {
                string clientJSONString = _session.Consult(Key);
                return JsonConvert.DeserializeObject<Client>(clientJSONString);
            }
            else
            {
                return null;
            }
        }
        public void Logout()
        {
            _session.RemoveAll();
        }
    }
}
