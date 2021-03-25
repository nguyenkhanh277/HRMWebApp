using HRMWebApp.Helper;
using HRMWebApp.Models;
using Newtonsoft.Json.Converters;
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
        public ActionResult PayrollsList(string month, string year)
        {
            InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
            DataTable dtb = SqlConnect.GetData(
                @"SELECT 
	                Payrolls.ID,
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
                    "<div><button type='button' class='btn btn-danger btn-remove' onclick=\"remove('" + dtb.Rows[i]["ID"].ToString() + "')\"><i class='fa fa-remove'></i></div>",
                    //"<div><button type='button' class='btn btn-success btn-edit' onclick=\"showForm('" + dtb.Rows[i]["ID"].ToString() + "')\"><i class='fa fa-edit'></i></div>",
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
        public JsonResult RemovePayrolls(string id)
        {
            bool status = false;
            string message = "Xóa thất bại";
            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    string query = SqlConnect.DeleteFromTableString(
                                        "Payrolls",
                                        " ID = N'" + id + "'"
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

        [UserAuthenticationFilter(AllUser: true), HttpPost]
        public JsonResult RemovePayrollsAll(string month, string year)
        {
            bool status = false;
            string message = "Xóa thất bại";
            try
            {
                InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
                if (!String.IsNullOrEmpty(month) && !String.IsNullOrEmpty(year))
                {
                    string query = SqlConnect.DeleteFromTableString(
                                        "Payrolls",
                                        " CompanyID = N'" + userInfo.CompanyID + "' AND MonthSalary = N'" + month + "' AND YearSalary = N'" + year + "'"
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
                                            string personalTaxCode = dtb.Rows[i][1].ToString();
                                            string employeeName = dtb.Rows[i][2].ToString();
                                            string position = dtb.Rows[i][3].ToString();
                                            string department = dtb.Rows[i][4].ToString();
                                            string startDate = "1900-01-01";
                                            try
                                            {
                                                startDate = DateTime.Parse(dtb.Rows[i][5].ToString()).ToString("yyyy-MM-dd");
                                            }
                                            catch { }
                                            string typeOfContact = dtb.Rows[i][6].ToString();
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

                                            string salt = SecurityHelper.CreateSalt(GlobalConstants.DEFAULT_SALT_LENGTH);
                                            string password = SecurityHelper.GenerateMD5(employeeCode, salt);
                                            query += "IF NOT EXISTS (SELECT Username FROM Users WHERE EmployeeCode = N'" + employeeCode + "') BEGIN " +
                                                SqlConnect.InsertToTableString(
                                                    "Users",
                                                    new string[] { "Username", "CompanyID", "Salt", "Password", "EmployeeCode", "EmployeeName", "PersonalTaxCode", "Position", "Department", "StartDate", "TypeOfContact", "Status", "CreatedAt", "CreatedBy" },
                                                    new object[] { employeeCode, userInfo.CompanyID, salt, password, employeeCode, employeeName, personalTaxCode, position, department, startDate, typeOfContact, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userInfo.Username }
                                                ) + " END;";
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
        [UserAuthenticationFilter(AllUser: true), HttpPost]
        public ActionResult PayrollDetails(string month, string year, string username)
        {
            bool status = false;
            string message = "Lọc bảng lương chi tiết thất bại";
            double actualWorkingDays = 0;
            double basicSalary = 0;
            double housingAlowance = 0;
            double transportAllowance = 0;
            double mobileAllowance = 0;
            double mealAllowance = 0;
            double totalSalaryAndAllowances = 0;
            double bonusPerformance = 0;
            double nontaxableIncome = 0;
            double companyBHXH = 0;
            double companyBHTNLD = 0;
            double companyBHYT = 0;
            double companyBHTN = 0;
            double companyTotal = 0;
            double companyKPCD = 0;
            double personalBHXH = 0;
            double personalBHYT = 0;
            double personalBHTN = 0;
            double personalTotal = 0;
            double pITPayable = 0;
            double salaryPay = 0;
            try
            {
                InfoLogin userInfo = InfoLogin.GetCurrentUser(System.Web.HttpContext.Current);
                DataTable dtb = SqlConnect.GetData(
                   @"SELECT 
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
                WHERE Payrolls.CompanyID = N'" + userInfo.CompanyID + "' AND Payrolls.MonthSalary = N'" + month + @"' AND Payrolls.YearSalary = N'" + year + @"' AND Users.Username = N'" + username + @"'");
                if (dtb.Rows.Count > 0)
                {
                    actualWorkingDays = double.Parse(dtb.Rows[0]["ActualWorkingDays"].ToString());
                    basicSalary = double.Parse(dtb.Rows[0]["BasicSalary"].ToString());
                    housingAlowance = double.Parse(dtb.Rows[0]["HousingAlowance"].ToString());
                    transportAllowance = double.Parse(dtb.Rows[0]["TransportAllowance"].ToString());
                    mobileAllowance = double.Parse(dtb.Rows[0]["MobileAllowance"].ToString());
                    mealAllowance = double.Parse(dtb.Rows[0]["MealAllowance"].ToString());
                    totalSalaryAndAllowances = double.Parse(dtb.Rows[0]["TotalSalaryAndAllowances"].ToString());
                    bonusPerformance = double.Parse(dtb.Rows[0]["BonusPerformance"].ToString());
                    nontaxableIncome = double.Parse(dtb.Rows[0]["NontaxableIncome"].ToString());
                    companyBHXH = double.Parse(dtb.Rows[0]["CompanyBHXH"].ToString());
                    companyBHTNLD = double.Parse(dtb.Rows[0]["CompanyBHTNLD"].ToString());
                    companyBHYT = double.Parse(dtb.Rows[0]["CompanyBHYT"].ToString());
                    companyBHTN = double.Parse(dtb.Rows[0]["CompanyBHTN"].ToString());
                    companyTotal = double.Parse(dtb.Rows[0]["CompanyTotal"].ToString());
                    companyKPCD = double.Parse(dtb.Rows[0]["CompanyKPCD"].ToString());
                    personalBHXH = double.Parse(dtb.Rows[0]["PersonalBHXH"].ToString());
                    personalBHYT = double.Parse(dtb.Rows[0]["PersonalBHYT"].ToString());
                    personalBHTN = double.Parse(dtb.Rows[0]["PersonalBHTN"].ToString());
                    personalTotal = double.Parse(dtb.Rows[0]["PersonalTotal"].ToString());
                    pITPayable = double.Parse(dtb.Rows[0]["PITPayable"].ToString());
                    salaryPay = double.Parse(dtb.Rows[0]["SalaryPay"].ToString());
                }
                status = true;
                message = "Lọc bảng lương chi tiết thành công";
            }
            catch
            { }

            return new JsonResult()
            {
                Data = new { status = status, message = message,
                    actualWorkingDays = actualWorkingDays.ToString("N0"),
                    basicSalary = basicSalary.ToString("N0"),
                    housingAlowance = housingAlowance.ToString("N0"),
                    transportAllowance = transportAllowance.ToString("N0"),
                    mobileAllowance = mobileAllowance.ToString("N0"),
                    mealAllowance = mealAllowance.ToString("N0"),
                    totalSalaryAndAllowances = totalSalaryAndAllowances.ToString("N0"),
                    bonusPerformance = bonusPerformance.ToString("N0"),
                    nontaxableIncome = nontaxableIncome.ToString("N0"),
                    companyBHXH = companyBHXH.ToString("N0"),
                    companyBHTNLD = companyBHTNLD.ToString("N0"),
                    companyBHYT = companyBHYT.ToString("N0"),
                    companyBHTN = companyBHTN.ToString("N0"),
                    companyTotal = companyTotal.ToString("N0"),
                    companyKPCD = companyKPCD.ToString("N0"),
                    personalBHXH = personalBHXH.ToString("N0"),
                    personalBHYT = personalBHYT.ToString("N0"),
                    personalBHTN = personalBHTN.ToString("N0"),
                    personalTotal = personalTotal.ToString("N0"),
                    pITPayable = pITPayable.ToString("N0"),
                    salaryPay = salaryPay.ToString("N0")
                },
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet

            };
        }
    }
}