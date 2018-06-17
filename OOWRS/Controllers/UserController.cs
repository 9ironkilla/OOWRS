using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OOWRS.Models;
using OOWRS.Repository;


namespace OOWRS.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository = new UserRepository();

      
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(UserModel user)
        {
            if(_userRepository.FindUser(user) != null)
            {

                HttpCookie cookie = HttpContext.Request.Cookies["UserCookie"] ?? new HttpCookie("UserCookie ");
                cookie.Values["UserName"] = user.Name;
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Add(cookie);

                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                return View("LoginError");
            }
        }
    }
}