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
    public class PayslipController : Controller
    {
        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult Index()
        {
            ViewBag.listMonth = ListMonth(false);
            ViewBag.listYear = ListYear(false);
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
            if (All)
            {
                itemList.Add(new SelectListItem { Selected = false, Text = string.Format("<span class='custom-select-item'>{0}</span>", "Tất cả"), Value = "ALL" });
            }
            DataTable dtb = SqlConnect.GetData("SELECT Username, FullName + ' (' + Username + ')' AS FullName FROM Users WHERE Username != 'administrator' ORDER BY FullName");
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                itemList.Add(new SelectListItem { Selected = false, Text = string.Format("<span class='custom-select-item'>{0}</span>", dtb.Rows[i]["FullName"].ToString()), Value = dtb.Rows[i]["Username"].ToString() });
            }
            return itemList;
        }

        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult PayslipDetail()
        {
            ViewBag.listMonth = ListMonth(false);
            ViewBag.listYear = ListYear(false);
            ViewBag.listUser = ListUser(true);
            return View();
        }


        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult PayslipList(string month, string year)
        {
            DataTable dtb = SqlConnect.GetData("SELECT Users.FullName, Payslip.LuongCung, Payslip.PhuCap, Payslip.ThucLinh FROM Payslip INNER JOIN Users ON Payslip.Username = Users.Username WHERE Payslip.PayslipMonth = N'" + month + "' AND Payslip.PayslipYear = N'" + year + "' ORDER BY Payslip.Username");
            List<string[]> lResult = new List<string[]>();
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                lResult.Add(new string[] {
                    (i + 1).ToString(),
                    dtb.Rows[i]["FullName"].ToString(),
                    double.Parse(dtb.Rows[i]["LuongCung"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["PhuCap"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["ThucLinh"].ToString()).ToString("N0")
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
        public ActionResult UploadPayslip()
        {
            bool result = false;
            string message = "";
            try
            {
                InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
                int month = int.Parse(Request.Form["ddlMonth"].ToString());
                int year = int.Parse(Request.Form["ddlYear"].ToString());
                String uploadPath = GlobalConstants.FILE_UPLOAD_PATH;
                FileUploader uploader = new FileUploader { UploadPath = uploadPath };
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0] as HttpPostedFileBase;
                    if (file != null && file.ContentLength > 0)
                    {
                        if (!GlobalHelper.ValidateExtensionEXCEL(Path.GetExtension(file.FileName).ToString().ToLower()))
                        {
                            message = "Tải lên bảng lương thất bại. Bảng lương phải là định dạng EXCEL.";
                        }
                        else
                        {
                            string ext = Path.GetExtension(file.FileName);
                            FileUploadResult fileResult = uploader.RenameUploadFile(file, ext, string.Format("{0}_{1}", file.FileName.Substring(0, file.FileName.LastIndexOf(".")), DateTime.Now.ToString("yyyyMMddHHmmss")));
                            if (fileResult.Success)
                            {
                                string filePath = Server.MapPath(Path.Combine(uploadPath, fileResult.FileName).Replace("~", "").Replace("\\", "/"));
                                DataTable dtb = FileUploader.ReadFileExcel(filePath, ext);
                                if (dtb.Rows.Count > 0)
                                {
                                    string query = "";
                                    for (int i = 0; i < dtb.Rows.Count; i++)
                                    {
                                        query +=
                                           SqlConnect.DeleteFromTableString(
                                               "Payslip",
                                               " Username = N'" + dtb.Rows[i][0].ToString() + "' AND PayslipMonth = N'" + dtb.Rows[i][2].ToString() + "' AND PayslipYear = N'" + dtb.Rows[i][3].ToString() + "'"
                                           );
                                        query +=
                                            SqlConnect.InsertToTableString(
                                                "Payslip",
                                                new string[] { "Username", "PayslipMonth", "PayslipYear", "LuongCung", "PhuCap", "ThucLinh" },
                                                new object[] { dtb.Rows[i][0].ToString(), dtb.Rows[i][2].ToString(), dtb.Rows[i][3].ToString(), dtb.Rows[i][4].ToString(), dtb.Rows[i][5].ToString(), dtb.Rows[i][6].ToString() }
                                            );
                                    }
                                    SqlConnect.ExecuteQueryUsingTran(query);
                                    if (SqlConnect.Error)
                                    {
                                        message = "Lưu bảng lương thất bại.";
                                    }
                                    else
                                    {
                                        result = true;
                                        message = "Tải lên bảng lương thành công.";
                                    }
                                }
                                else
                                {
                                    message = "Tải lên bảng lương thất bại. Bảng lương không hợp lệ.";
                                }
                                FileUploader.RemoveUploadFile(filePath);

                            }
                            else
                            {
                                message = "Tải lên bảng lương thất bại. Vui lòng thử lại.";
                            }
                        }
                    }
                    else
                    {
                        message = "Tải lên bảng lương thất bại. Chưa chọn bảng lương.";
                    }
                }
                else
                {
                    message = "Tải lên bảng lương thất bại. Chưa chọn bảng lương.";
                }
            }
            catch
            {
                message = "Tải lên bảng lương thất bại. (ErrorCode: 500)";
            }
            return Json(new { status = result, message = message });
        }
    }
}