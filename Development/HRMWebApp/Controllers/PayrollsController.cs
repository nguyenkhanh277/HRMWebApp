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
    public class PayrollsController : Controller
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
            DataTable dtb = SqlConnect.GetData("SELECT Username, EmployeeName + ' (' + Username + ')' AS EmployeeName FROM Users WHERE Username != 'administrator' ORDER BY EmployeeName");
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                itemList.Add(new SelectListItem { Selected = false, Text = string.Format("<span class='custom-select-item'>{0}</span>", dtb.Rows[i]["EmployeeName"].ToString()), Value = dtb.Rows[i]["Username"].ToString() });
            }
            return itemList;
        }

        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult PayrollsDetail()
        {
            ViewBag.listMonth = ListMonth(false);
            ViewBag.listYear = ListYear(false);
            ViewBag.listUser = ListUser(true);
            return View();
        }


        [UserAuthenticationFilter(AllUser: true)]
        public ActionResult PayrollsList(string month, string year)
        {
                InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
            DataTable dtb = SqlConnect.GetData(
                @"SELECT 
	                Payrolls.EmployeeCode,
	                Users.EmployeeName, 
	                Payrolls.ActualWorkingDays,
	                Payrolls.BasicSalary,
	                Payrolls.HousingAlowance,
	                Payrolls.TransportAllowance,
	                Payrolls.MobileAllowance,
	                Payrolls.MealAllowance,
	                Payrolls.TotalSalaryAndAllowances,
	                Payrolls.BonusPerformance,
	                Payrolls.NontaxableIncome,
	                Payrolls.CompanyBHXH,
	                Payrolls.CompanyBHTNLD,
	                Payrolls.CompanyBHYT,
	                Payrolls.CompanyBHTN,
	                Payrolls.CompanyTotal,
	                Payrolls.CompanyKPCD,
	                Payrolls.PersonalBHXH,
	                Payrolls.PersonalBHYT,
	                Payrolls.PersonalBHTN,
	                Payrolls.PersonalTotal,
	                Payrolls.PITPayable,
	                Payrolls.SalaryPay
                FROM Payrolls INNER JOIN Users ON Payrolls.EmployeeCode = Users.EmployeeCode 
                WHERE Payrolls.CompanyID = N'" + userInfo.CompanyID + "' AND Payrolls.MonthSalary = N'" + month + @"' AND Payrolls.YearSalary = N'" + year + @"' 
                ORDER BY Payrolls.EmployeeCode");
            List<string[]> lResult = new List<string[]>();
            for (int i = 0; i < dtb.Rows.Count; i++)
            {
                lResult.Add(new string[] {
                    (i + 1).ToString(),
                    dtb.Rows[i]["EmployeeCode"].ToString(),
                    dtb.Rows[i]["EmployeeName"].ToString(),
                    double.Parse(dtb.Rows[i]["ActualWorkingDays"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["BasicSalary"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["HousingAlowance"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["TransportAllowance"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["MobileAllowance"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["MealAllowance"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["TotalSalaryAndAllowances"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["BonusPerformance"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["NontaxableIncome"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["CompanyBHXH"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["CompanyBHTNLD"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["CompanyBHYT"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["CompanyBHTN"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["CompanyTotal"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["CompanyKPCD"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["PersonalBHXH"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["PersonalBHYT"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["PersonalBHTN"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["PersonalTotal"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["PITPayable"].ToString()).ToString("N0"),
                    double.Parse(dtb.Rows[i]["SalaryPay"].ToString()).ToString("N0")
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
        public ActionResult UploadPayrolls()
        {
            bool result = false;
            string message = "";
            int errorRowIndex = 1;
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
                            message = "Tải lên bảng lương thất bại. Bảng lương phải là định dạng EXCEL.";
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
                                        errorRowIndex++;
                                        if (!String.IsNullOrEmpty(dtb.Rows[i][0].ToString()))
                                        {
                                            string employeeCode = dtb.Rows[i][0].ToString();
                                            string actualWorkingDays = (String.IsNullOrEmpty(dtb.Rows[i][7].ToString()) || dtb.Rows[i][7].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][7].ToString()).ToString());
                                            string basicSalary = (String.IsNullOrEmpty(dtb.Rows[i][8].ToString()) || dtb.Rows[i][8].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][8].ToString()).ToString());
                                            string housingAlowance = (String.IsNullOrEmpty(dtb.Rows[i][9].ToString()) || dtb.Rows[i][9].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][9].ToString()).ToString());
                                            string transportAllowance = (String.IsNullOrEmpty(dtb.Rows[i][10].ToString()) || dtb.Rows[i][10].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][10].ToString()).ToString());
                                            string mobileAllowance = (String.IsNullOrEmpty(dtb.Rows[i][11].ToString()) || dtb.Rows[i][11].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][11].ToString()).ToString());
                                            string mealAllowance = (String.IsNullOrEmpty(dtb.Rows[i][12].ToString()) || dtb.Rows[i][12].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][12].ToString()).ToString());
                                            string totalSalaryAndAllowances = (String.IsNullOrEmpty(dtb.Rows[i][13].ToString()) || dtb.Rows[i][13].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][13].ToString()).ToString());
                                            string bonusPerformance = (String.IsNullOrEmpty(dtb.Rows[i][14].ToString()) || dtb.Rows[i][14].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][14].ToString()).ToString());
                                            string nontaxableIncome = (String.IsNullOrEmpty(dtb.Rows[i][15].ToString()) || dtb.Rows[i][15].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][15].ToString()).ToString());
                                            string companyBHXH = (String.IsNullOrEmpty(dtb.Rows[i][16].ToString()) || dtb.Rows[i][16].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][16].ToString()).ToString());
                                            string companyBHTNLD = (String.IsNullOrEmpty(dtb.Rows[i][17].ToString()) || dtb.Rows[i][17].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][17].ToString()).ToString());
                                            string companyBHYT = (String.IsNullOrEmpty(dtb.Rows[i][18].ToString()) || dtb.Rows[i][18].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][18].ToString()).ToString());
                                            string companyBHTN = (String.IsNullOrEmpty(dtb.Rows[i][19].ToString()) || dtb.Rows[i][19].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][19].ToString()).ToString());
                                            string companyTotal = (String.IsNullOrEmpty(dtb.Rows[i][20].ToString()) || dtb.Rows[i][20].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][20].ToString()).ToString());
                                            string companyKPCD = (String.IsNullOrEmpty(dtb.Rows[i][21].ToString()) || dtb.Rows[i][21].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][21].ToString()).ToString());
                                            string personalBHXH = (String.IsNullOrEmpty(dtb.Rows[i][22].ToString()) || dtb.Rows[i][22].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][22].ToString()).ToString());
                                            string personalBHYT = (String.IsNullOrEmpty(dtb.Rows[i][23].ToString()) || dtb.Rows[i][23].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][23].ToString()).ToString());
                                            string personalBHTN = (String.IsNullOrEmpty(dtb.Rows[i][24].ToString()) || dtb.Rows[i][24].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][24].ToString()).ToString());
                                            string personalTotal = (String.IsNullOrEmpty(dtb.Rows[i][25].ToString()) || dtb.Rows[i][25].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][25].ToString()).ToString());
                                            string pITPayable = (String.IsNullOrEmpty(dtb.Rows[i][26].ToString()) || dtb.Rows[i][26].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][26].ToString()).ToString());
                                            string salaryPay = (String.IsNullOrEmpty(dtb.Rows[i][27].ToString()) || dtb.Rows[i][27].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][27].ToString()).ToString());
                                            string taxableIncome = (String.IsNullOrEmpty(dtb.Rows[i][28].ToString()) || dtb.Rows[i][28].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][28].ToString()).ToString());
                                            string personalDeduction = (String.IsNullOrEmpty(dtb.Rows[i][29].ToString()) || dtb.Rows[i][29].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][29].ToString()).ToString());
                                            string numberOfDependant = (String.IsNullOrEmpty(dtb.Rows[i][30].ToString()) || dtb.Rows[i][30].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][30].ToString()).ToString());
                                            string totalDeductionForDependant = (String.IsNullOrEmpty(dtb.Rows[i][31].ToString()) || dtb.Rows[i][31].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][31].ToString()).ToString());
                                            string totalEmployeeDeduction = (String.IsNullOrEmpty(dtb.Rows[i][32].ToString()) || dtb.Rows[i][32].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][32].ToString()).ToString());
                                            string incomeToCalculatePITPayable = (String.IsNullOrEmpty(dtb.Rows[i][33].ToString()) || dtb.Rows[i][33].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][33].ToString()).ToString());
                                            string calculatorPITPayable = (String.IsNullOrEmpty(dtb.Rows[i][34].ToString()) || dtb.Rows[i][34].ToString() == "-" ? "0" : double.Parse(dtb.Rows[i][34].ToString()).ToString());
                                            query +=
                                               SqlConnect.DeleteFromTableString(
                                                   "Payrolls",
                                                   " CompanyID = N'" + userInfo.CompanyID + "' AND EmployeeCode = N'" + dtb.Rows[i][0].ToString() + "' AND MonthSalary = N'" + month + "' AND YearSalary = N'" + year + "'"
                                               );
                                            query +=
                                                SqlConnect.InsertToTableString(
                                                    "Payrolls",
                                                    new string[] { "CompanyID", "MonthSalary", "YearSalary",
                                                    "EmployeeCode", "ActualWorkingDays", "BasicSalary", "HousingAlowance", "TransportAllowance", "MobileAllowance", "MealAllowance", "TotalSalaryAndAllowances", "BonusPerformance", "NontaxableIncome", "CompanyBHXH", "CompanyBHTNLD", "CompanyBHYT", "CompanyBHTN", "CompanyTotal", "CompanyKPCD", "PersonalBHXH", "PersonalBHYT", "PersonalBHTN", "PersonalTotal", "PITPayable", "SalaryPay",
                                                    "TaxableIncome", "PersonalDeduction", "NumberOfDependant", "TotalDeductionForDependant", "TotalEmployeeDeduction", "IncomeToCalculatePITPayable", "CalculatorPITPayable"
                                                    },
                                                    new object[] { userInfo.CompanyID, month, year,
                                                    employeeCode, actualWorkingDays, basicSalary, housingAlowance, transportAllowance, mobileAllowance, mealAllowance, totalSalaryAndAllowances, bonusPerformance, nontaxableIncome, companyBHXH, companyBHTNLD, companyBHYT, companyBHTN, companyTotal, companyKPCD, personalBHXH, personalBHYT, personalBHTN, personalTotal, pITPayable, salaryPay,
                                                    taxableIncome, personalDeduction, numberOfDependant, totalDeductionForDependant, totalEmployeeDeduction, incomeToCalculatePITPayable, calculatorPITPayable
                                                    }
                                                );
                                        }
                                    }
                                    SqlConnect.ExecuteQueryUsingTran(query);
                                    if (SqlConnect.Error)
                                        message = "Lưu bảng lương thất bại.";
                                    else
                                    {
                                        result = true;
                                        message = "Tải lên bảng lương thành công.";
                                    }
                                }
                                else
                                    message = "Tải lên bảng lương thất bại. Bảng lương không hợp lệ.";
                                FileUploader.RemoveUploadFile(filePath);

                            }
                            else
                                message = "Tải lên bảng lương thất bại. Vui lòng thử lại.";
                        }
                    }
                    else
                        message = "Tải lên bảng lương thất bại. Chưa chọn bảng lương.";
                }
                else
                    message = "Tải lên bảng lương thất bại. Chưa chọn bảng lương.";
            }
            catch (Exception ex)
            {
                message = "Tải lên bảng lương thất bại. Lỗi dòng " + errorRowIndex + " (" + ex.ToString() + ")";
            }
            return Json(new
            {
                status = result,
                message = message
            });
        }
    }
}