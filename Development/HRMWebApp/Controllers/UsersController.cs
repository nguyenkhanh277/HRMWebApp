using HRMWebApp.Helper;
using HRMWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace HRMWebApp.Controllers
{
    public class UsersController : Controller
    {
        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult Index()
        {
            return View();
        }

        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult UsersAddEdit(string username)
        {
            DataTable dtb = SqlConnect.GetData("SELECT Username, EmployeeCode, EmployeeName, PersonalTaxCode, Position, Department, StartDate, TypeOfContact FROM Users WHERE Username = N'" + username + "'");
            string employeeCode = "";
            string employeeName = "";
            string personalTaxCode = "";
            string position = "";
            string department = "";
            string startDate = "";
            string typeOfContact = "";
            if (dtb.Rows.Count > 0)
            {
                employeeCode = dtb.Rows[0]["EmployeeCode"].ToString();
                employeeName = dtb.Rows[0]["EmployeeName"].ToString();
                personalTaxCode = dtb.Rows[0]["PersonalTaxCode"].ToString();
                position = dtb.Rows[0]["Position"].ToString();
                department = dtb.Rows[0]["Department"].ToString();
                startDate = dtb.Rows[0]["StartDate"].ToString();
                typeOfContact = dtb.Rows[0]["TypeOfContact"].ToString();
            }
            ViewBag.username = username;
            ViewBag.employeeCode = employeeCode;
            ViewBag.employeeName = employeeName;
            ViewBag.personalTaxCode = personalTaxCode;
            ViewBag.position = position;
            ViewBag.department = department;
            ViewBag.startDate = startDate;
            ViewBag.typeOfContact = typeOfContact;
            return PartialView("UsersAddEdit");
        }

        [UserAuthenticationFilter(AllUser: true), HttpPost]
        public JsonResult UsersAddEdit()
        {
            bool status = false;
            string message = "Cập nhật thất bại.";
            try
            {
                InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
                string username = Request.Form["Username"].ToString();
                string employeeCode = Request.Form["EmployeeCode"].ToString();
                string employeeName = Request.Form["EmployeeName"].ToString();
                string personalTaxCode = Request.Form["PersonalTaxCode"].ToString();
                string position = Request.Form["Position"].ToString();
                string department = Request.Form["Department"].ToString();
                string startDate = Request.Form["StartDate"].ToString();
                string typeOfContact = Request.Form["TypeOfContact"].ToString();
                if (String.IsNullOrEmpty(username))
                    message = "Chưa điền tài khoản";
                else if (String.IsNullOrEmpty(employeeCode))
                    message = "Chưa điền mã nhân viên";
                else if (String.IsNullOrEmpty(employeeName))
                    message = "Chưa điền tên nhân viên";
                else
                {
                    DataTable dtb = SqlConnect.GetData("SELECT Username FROM Users WHERE Username = N'" + username + "'");
                    if (dtb.Rows.Count > 0)
                    {
                        string query = SqlConnect.UpdateToTableString(
                            "Users",
                            new string[] { "EmployeeCode", "EmployeeName", "PersonalTaxCode", "Position", "Department", "StartDate", "TypeOfContact" },
                            new object[] { employeeCode, employeeName, personalTaxCode, position, department, startDate, typeOfContact },
                            "Username = N'" + username + "'");
                        SqlConnect.ExecuteQueryUsingTran(query);
                        if (!SqlConnect.Error)
                        {
                            status = true;
                            message = "Thành công.";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(username))
                            message = "Chưa điền tài khoản";
                        else
                        {
                            string salt = SecurityHelper.CreateSalt(GlobalConstants.DEFAULT_SALT_LENGTH);
                            string rawPassword = Request.Form["Password"].ToString();
                            string password = SecurityHelper.GenerateMD5(rawPassword, salt);
                            string query = SqlConnect.InsertToTableString(
                                "Users",
                                new string[] { "Username", "CompanyID", "Salt", "Password", "EmployeeCode", "EmployeeName", "PersonalTaxCode", "Position", "Department", "StartDate", "TypeOfContact", "Status", "CreatedAt", "CreatedBy" },
                                new object[] { username, userInfo.CompanyID, salt, password, employeeCode, employeeName, personalTaxCode, position, department, startDate, typeOfContact, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userInfo.Username });

                            SqlConnect.ExecuteQueryUsingTran(query);
                            if (!SqlConnect.Error)
                            {
                                status = true;
                                message = "Thành công.";
                            }
                        }
                    }
                }
            }
            catch
            { }
            return new JsonResult
            {
                Data = new { status = status, message = message },
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [UserAuthenticationFilter(AllUser: true), HttpPost]
        public JsonResult RemoveUsers(string username)
        {
            bool status = false;
            string message = "Xóa thất bại";
            try
            {
                if (!String.IsNullOrEmpty(username))
                {
                    string query = SqlConnect.DeleteFromTableString(
                                        "Users",
                                        " Username = N'" + username + "'"
                                    );
                    SqlConnect.ExecuteQueryUsingTran(query);
                    if (!SqlConnect.Error)
                    {
                        status = true;
                        message = "Thành công.";
                    }
                }
            }
            catch { }
            return new JsonResult
            {
                Data = new { status = status, message = message },
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult UsersList(string companyID)
        {
            InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
            DataTable dtb = SqlConnect.GetData("SELECT Username, EmployeeCode, EmployeeName, PersonalTaxCode, Position, Department, StartDate, TypeOfContact FROM Users WHERE Username != 'administrator' AND CompanyID = '" + userInfo.CompanyID + "'");
            List<string[]> lResult = new List<string[]>();
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                lResult.Add(new string[] {
                    "<div><button type='button' class='btn btn-danger btn-remove' onclick=\"remove('" + dtb.Rows[i]["Username"].ToString() + "')\"><i class='fa fa-remove'></i></div>",
                    "<div><button type='button' class='btn btn-success btn-edit' onclick=\"showForm('" + dtb.Rows[i]["Username"].ToString() + "')\"><i class='fa fa-edit'></i></div>",
                    (i + 1).ToString(),
                    dtb.Rows[i]["Username"].ToString(),
                    dtb.Rows[i]["EmployeeCode"].ToString(),
                    dtb.Rows[i]["EmployeeName"].ToString(),
                    dtb.Rows[i]["PersonalTaxCode"].ToString(),
                    dtb.Rows[i]["Position"].ToString(),
                    dtb.Rows[i]["Department"].ToString(),
                    (String.IsNullOrEmpty(dtb.Rows[i]["StartDate"].ToString()) ? "" : DateTime.Parse(dtb.Rows[i]["StartDate"].ToString()).ToString("dd/MM/yy")),
                    dtb.Rows[i]["TypeOfContact"].ToString()
                });
            }

            return new JsonResult()
            {
                Data = new { data = lResult.ToArray() },
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet

            };
        }

        [UserAuthenticationFilter(AllUser: true), HttpPost]
        public JsonResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            bool status = false;
            string message = "Đổi mật khẩu thất bại";
            try
            {
                if (String.IsNullOrEmpty(oldPassword))
                {
                    message = "Chưa điền mật khẩu cũ";
                }
                else if (String.IsNullOrEmpty(newPassword))
                {
                    message = "Chưa điền mật khẩu mới";
                }
                else if (String.IsNullOrEmpty(confirmPassword))
                {
                    message = "Chưa điền xác nhận mật khẩu";
                }
                else if (newPassword != confirmPassword)
                {
                    message = "Xác nhận mật khẩu không đúng";
                }
                else
                {
                    InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
                    DataTable dtb = SqlConnect.GetData("SELECT Username, Password, Salt FROM Users WHERE UPPER(Username) = UPPER(N'" + userInfo.Username + "')");
                    if (dtb.Rows.Count > 0)
                    {
                        String encryptedPassword = SecurityHelper.GenerateMD5(oldPassword, dtb.Rows[0]["Salt"].ToString());
                        if (dtb.Rows[0]["Password"].ToString() != encryptedPassword)
                        {
                            message = "Mật khẩu cũ không đúng";
                        }
                        else
                        {
                            string password = SecurityHelper.GenerateMD5(newPassword, dtb.Rows[0]["Salt"].ToString());
                            string query = SqlConnect.UpdateToTableString(
                                            "Users",
                                            new string[] { "Password" },
                                            new object[] { password },
                                            " Username = N'" + userInfo.Username + "'"
                                        );
                            SqlConnect.ExecuteQueryUsingTran(query);
                            if (!SqlConnect.Error)
                            {
                                status = true;
                                message = "Đổi mật khẩu thành công.";
                            }
                        }
                    }
                }
            }
            catch { }
            return new JsonResult
            {
                Data = new { status = status, message = message },
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}