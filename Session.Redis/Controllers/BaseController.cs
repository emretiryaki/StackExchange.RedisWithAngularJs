using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Session.Redis.Helpers.UserManager;

namespace Session.Redis.Controllers
{
    public class BaseController : Controller
    {
        public IUserManager _userManager { get; set; }


        public BaseController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");
            var cache = filterContext.HttpContext.Response.Cache;
            cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            cache.SetValidUntilExpires(false);
            cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            cache.SetCacheability(HttpCacheability.NoCache);
            cache.SetNoStore();

            bool skipAuthentication =
            (
                filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            );

            if (!skipAuthentication && _userManager.User == null)
            {
               Logger.logger.Info("UnauthorizedAccessException occured in OnActionExecuting ! Host: " + Dns.GetHostName() );

                throw new UnauthorizedAccessException("Oturum Süresi Doldu");
            }
            base.OnActionExecuting(filterContext);


        }
    }
}
