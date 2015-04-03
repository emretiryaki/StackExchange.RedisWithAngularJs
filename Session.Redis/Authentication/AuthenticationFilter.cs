using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Session.Redis.Helpers.UserManager;

namespace Session.Redis.Authentication
{
    public sealed class AuthenticationFilter : AuthorizeAttribute
    {
        public AuthenticationFilter(IUserManager userManager)
        {
            UserManager = userManager;
        }

        private IUserManager UserManager { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return UserManager != null && UserManager.IsAuthenticated;
        }

        [DebuggerStepThrough] 
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (UserManager != null && !(UserManager.IsAuthenticated))
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}