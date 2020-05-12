using mvCommerce.Models;
using Newtonsoft.Json;

namespace mvCommerce.Libraries.Login
{
    public class CollaboratorLogin
    {

        private Session.Session _session;
        private readonly string Key = "Login.Collaborator";
        public CollaboratorLogin(Session.Session session)
        {
            _session = session;
        }

        public void Login(Collaborator collaborator)
        {
            //Serialize 
            string collaboratorJSONString = JsonConvert.SerializeObject(collaborator);

            _session.Register(Key, collaboratorJSONString);
        }

        public Collaborator GetCollaborator()
        {
            //Deserialize
            if (_session.isExisting(Key))
            {
                string collaboratorJSONString = _session.Consult(Key);
                return JsonConvert.DeserializeObject<Collaborator>(collaboratorJSONString);
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

