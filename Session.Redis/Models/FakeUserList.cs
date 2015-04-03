using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Session.Redis.Helpers.UserManager;

namespace Session.Redis.Models
{
    public static  class FakeUserList 
    {
        public static ICollection<UserModel> UserModels  = new Collection<UserModel>();

        
    }
}