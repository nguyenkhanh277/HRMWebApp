using HRMWebApp.Helper;
using HRMWebApp.Models;
using System;
using System.Configuration;
using System.Data;
using System.Web.Mvc;

namespace HRMWebApp.Controllers
{
    public class HomeController : Controller
    {
        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult Index()
        {
            return View();
        }

        #region Security
        [HttpGet]
        public ActionResult Login()
        {
            SqlConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (Session[GlobalConstants.SESSION_KEY_USER] != null) return RedirectToAction("Index");
            ViewBag.Message = new ServerMessage();
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult ToDoLogin()
        {
            ViewBag.Message = new ServerMessage() { Code = "", Message = "" };
            string username = Request.Form["txtUsername"].ToString();
            string password = Request.Form["txtPassword"].ToString();
            if (String.IsNullOrEmpty(username))
            {
                ViewBag.Message = new ServerMessage() { Code = "text-warning", Message = "Chưa điền tài khoản." };
            }
            else if (String.IsNullOrEmpty(password))
            {
                ViewBag.Message = new ServerMessage() { Code = "text-warning", Message = "Chưa điền mật khẩu." };
            }
            else
            {
                DataTable dtb = SqlConnect.GetData("SELECT Username, FullName, Password, Salt FROM Users WHERE UPPER(Username) = UPPER(N'" + username + "')");

                if (dtb.Rows.Count > 0)
                {
                    String encryptedPassword = SecurityHelper.GenerateMD5(password, dtb.Rows[0]["Salt"].ToString());
                    if (dtb.Rows[0]["Password"].ToString() != encryptedPassword)
                    {
                        ViewBag.Message = new ServerMessage() { Code = "text-warning", Message = "Mật khẩu không đúng." };
                    }
                    else
                    {
                        InfoLogin _infoLogin = new InfoLogin() { UserName = dtb.Rows[0]["Username"].ToString(), FullName = dtb.Rows[0]["FullName"].ToString(), LoginTime = DateTime.Now };
                        Session[GlobalConstants.SESSION_KEY_USER] = _infoLogin;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.Message = new ServerMessage() { Code = "text-warning", Message = "Tài khoản hoặc mật khẩu không chính xác." };
                }
            }
            return View("Login");
        }
        #endregion
    }
}