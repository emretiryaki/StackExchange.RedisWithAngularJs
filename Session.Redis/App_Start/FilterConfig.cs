using System.Web;
using System.Web.Mvc;
using Session.Redis.Authentication;
using Session.Redis.Helpers.UserManager;

namespace Session.Redis
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthenticationFilter(IoC.Resolve<IUserManager>()));
        }
    }
}