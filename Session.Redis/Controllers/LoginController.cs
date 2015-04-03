using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Session.Redis.Helpers.UserManager;
using Session.Redis.Models;

namespace Session.Redis.Controllers
{
    public class LoginController : BaseController
    {

        public LoginController(IUserManager userManager)
            : base(userManager)
        {
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public ActionResult LoginControl(string username, string password)
        {
            try
            {
             
                if (FakeUserList.UserModels.Any(x => x.UserName == username && x.Password == password))
                {
                    var user = FakeUserList.UserModels.First(x => x.UserName == username && x.Password == password);

                    _userManager.User = new UserModel
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Id = user.Id,
                        MiddleName = user.MiddleName,
                        Password = user.Password,
                        UserName = user.UserName
                    };

                    return Json(new
                    {
                        status = 200,
                        data = Url.Action("Index", "Home")
                    });

                }
                return Json(new
                {
                    status = 500,
                    data = "Username or Password Invalid"
                });
            }
            catch (Exception exc)
            {

                return Json(new
                {
                    status = 500,
                    data = string.Format("{0} / {1} ", exc.Message, exc.InnerException != null ? exc.InnerException.Message : string.Empty)
                });
            }
        }
    }
}
