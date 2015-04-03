using System;

namespace Session.Redis.Helpers.UserManager
{
    [Serializable]
    public class UserModel
    {
        public string Email;
        public string FirstName;
        public long Id;
        public string LastName;
        public string MiddleName;
        public string UserName;
        public string Password;

    }
}