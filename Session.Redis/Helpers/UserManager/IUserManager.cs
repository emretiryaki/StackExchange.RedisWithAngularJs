using System.Linq;
using System.Web;

namespace Session.Redis.Helpers.UserManager
{
    public interface IUserManager
    {
        bool IsAuthenticated { get; }
        UserModel User { get; set; }
        UserPermissionModel UserPermissions { get; set; }
        bool SslEnabled { get; set; }
        bool HasPermission(params string[] permissionTypes);
        bool HasPermission(string permissionType); 
        void ResetUser();
    }
}