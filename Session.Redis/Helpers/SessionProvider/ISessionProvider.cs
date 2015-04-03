using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Session.Redis.Helpers.SessionProvider
{
    public interface ISessionProvider
    {
        bool Contains(string key);
        T Get<T>(string key);
        void Add(string key, object value);
        void Remove(string key);

    }
}