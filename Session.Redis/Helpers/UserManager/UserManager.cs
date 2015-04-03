using System.Linq;
using Session.Redis.Helpers.SessionProvider;

namespace Session.Redis.Helpers.UserManager
{
    public class UserManager : IUserManager
    {
        private const string CurrentUserKey = "CurrentUser";
        private const string CurrentUserPermissionKey = "CurrentUserPermissionKey";

        private readonly ISessionProvider _sessionProvider;
        public UserManager(ISessionProvider sessionProvider)
        {
            this._sessionProvider = sessionProvider;
        }


        public bool IsAuthenticated
        {
            get { return _sessionProvider.Contains(CurrentUserKey); }
        }

        public UserModel User
        {
            get
            {
                return !(_sessionProvider.Contains(CurrentUserKey))
                    ? null
                    : _sessionProvider.Get<UserModel>(CurrentUserKey);
            }
            set
            {
                _sessionProvider.Add(CurrentUserKey,value);
            }
        }

        public UserPermissionModel UserPermissions
        {
            get
            {
                return !(_sessionProvider.Contains(CurrentUserPermissionKey))
                    ? null
                    : _sessionProvider.Get<UserPermissionModel>(CurrentUserPermissionKey);
            }
            set { _sessionProvider.Add(CurrentUserPermissionKey, value); }
        }
        public bool SslEnabled { get; set; }
        public bool HasPermission(params string[] permissions)
        {
            bool hasAuthorization = false;

            foreach (var permissionIdForCheck in permissions)
            {
                hasAuthorization = permissions.Contains(permissionIdForCheck);
                if (hasAuthorization)
                    break;
            }
            return hasAuthorization;
        }

        public bool HasPermission(string permissionType)
        {
            return UserPermissions.Permissions.Contains(permissionType);
        }

        public void ResetUser()
        {
            _sessionProvider.Remove(CurrentUserKey);
           
        }
    }
}