

using System.Web;
using System.Web.SessionState;

namespace Session.Redis.Helpers.SessionProvider
{
    public class HttpContextSessionProvider : ISessionProvider
    {
        private static HttpSessionState SessionState
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
        public bool Contains(string key)
        {
            return SessionState[key] != null;
        }

        public T Get<T>(string key)
        {
            return (T)SessionState[key];
        }

        public void Add(string key, object value)
        {
            SessionState.Add(key, value);
        }

        public void Remove(string key)
        {
            SessionState.Remove(key);
        }
    }
}