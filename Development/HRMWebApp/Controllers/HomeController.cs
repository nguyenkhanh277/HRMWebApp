using HRMWebApp.Helper;
using HRMWebApp.Models;
using System;
using System.Collections.Generic;
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
            ViewBag.listMonth = ListMonth(false);
            ViewBag.listYear = ListYear(false);
            ViewBag.listUser = ListUser(false);
            return View();
        }

        public static List<SelectListItem> ListMonth(bool All)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            if (All)
            {
                itemList.Add(new SelectListItem { Selected = false, Text = string.Format("<span class='custom-select-item'>{0}</span>", "Tất cả"), Value = "ALL" });
            }
            for (int i = 1; i <= 12; i++)
            {
                itemList.Add(new SelectListItem
                {
                    Selected = false,
                    Text = string.Format("<span class='custom-select-item'>{0}</span>", i.ToString("00")),
                    Value = i.ToString()
                });
            }
            return itemList;
        }

        public static List<SelectListItem> ListYear(bool All)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            if (All)
            {
                itemList.Add(new SelectListItem { Selected = false, Text = string.Format("<span class='custom-select-item'>{0}</span>", "Tất cả"), Value = "ALL" });
            }
            for (int i = 2021; i <= DateTime.Now.Year; i++)
            {
                itemList.Add(new SelectListItem { Selected = false, Text = string.Format("<span class='custom-select-item'>{0}</span>", i.ToString()), Value = i.ToString() });
            }
            return itemList;
        }

        public static List<SelectListItem> ListUser(bool All)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            try
            {
                InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
                if (All)
                {
                    itemList.Add(new SelectListItem { Selected = false, Text = string.Format("<span class='custom-select-item'>{0}</span>", "Tất cả"), Value = "ALL" });
                }
                DataTable dtb = new DataTable();
                if (userInfo.Username == "administrator")
                {
                    dtb = SqlConnect.GetData("SELECT Username, EmployeeName + ' (' + Username + ')' AS EmployeeName FROM Users WHERE Username != '" + userInfo.Username + "' ORDER BY EmployeeName");
                }
                else
                {
                    dtb = SqlConnect.GetData("SELECT Username, EmployeeName + ' (' + Username + ')' AS EmployeeName FROM Users WHERE Username = '" + userInfo.Username + "' ORDER BY EmployeeName");
                }
                for (int i = 0; i < dtb.Rows.Count; i++)
                {
                    itemList.Add(new SelectListItem { Selected = false, Text = string.Format("<span class='custom-select-item'>{0}</span>", dtb.Rows[i]["EmployeeName"].ToString()), Value = dtb.Rows[i]["Username"].ToString() });
                }
            }
            catch { }
            return itemList;
        }

        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult ChangePassword()
        {
            return View();
        }

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
                DataTable dtb = SqlConnect.GetData("SELECT Username, CompanyID, EmployeeName, Password, Salt FROM Users WHERE UPPER(Username) = UPPER(N'" + username + "')");

                if (dtb.Rows.Count > 0)
                {
                    String encryptedPassword = SecurityHelper.GenerateMD5(password, dtb.Rows[0]["Salt"].ToString());
                    if (dtb.Rows[0]["Password"].ToString() != encryptedPassword)
                    {
                        ViewBag.Message = new ServerMessage() { Code = "text-warning", Message = "Mật khẩu không đúng." };
                    }
                    else
                    {
                        InfoLogin _infoLogin = new InfoLogin() { Username = dtb.Rows[0]["Username"].ToString(), CompanyID = dtb.Rows[0]["CompanyID"].ToString(), EmployeeName = dtb.Rows[0]["EmployeeName"].ToString(), LoginTime = DateTime.Now };
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
    }
}