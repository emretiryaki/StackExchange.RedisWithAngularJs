using System;
using System.Collections.Generic;

namespace Session.Redis.Helpers.UserManager
{
    [Serializable]
    public class UserPermissionModel
    {
        public List<string> Permissions;
        public bool HasPermission(string permissionType)
        {
            return this.Permissions.Contains(permissionType);
        }

    }
}