using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Session.Redis.Helpers.UserManager;

namespace Session.Redis.ViewEngine
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public IUserManager _userManager;
      

        public WebViewPage()
        {
            _userManager = IoC.Resolve<IUserManager>();
           
        }

    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {

    }
}