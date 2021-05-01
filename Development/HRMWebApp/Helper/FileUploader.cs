using HRMWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace HRMWebApp.Helper
{
    public class FileUploader
    {
        public string UploadPath { get; set; }

        public FileUploadResult RenameUploadFile(HttpPostedFileBase file, string ext = "", string savedName = "")
        {
            var fileName = Path.GetFileName(file.FileName);
            var oldExt = fileName.Substring(fileName.LastIndexOf("."));

            string finalFileName = savedName != "" ? savedName : Guid.NewGuid().ToString();
            // finalFileName+= (ext.Equals("")) ? ".css" : "";
            //int position = fileName.LastIndexOf(".");
            //if (position > 0)
            //    finalFileName += ext;//+= fileName.Substring(position).ToLower() + ext
            if (ext.Equals(".cshtml"))
                finalFileName += oldExt + ext;
            else
                finalFileName += String.IsNullOrEmpty(ext) ? oldExt : ext;

            if (System.IO.File.Exists
                (HttpContext.Current.Request.MapPath(UploadPath + finalFileName)))
            {
                //file exists => add country try again
                return RenameUploadFile(file);
            }
            //file doesn't exist, upload item but validate first
            return UploadFile(file, finalFileName);
        }



        private FileUploadResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            FileUploadResult fileResult = new FileUploadResult { Success = true, ErrorMessage = null };

            var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);
            string extension = Path.GetExtension(file.FileName);

            //make sure the file is valid
            if (!ValidateExtension(extension))
            {
                fileResult.Success = false;
                fileResult.ErrorMessage = "Invalid Extension";
                return fileResult;
            }

            try
            {
                if (!System.IO.File.Exists(HttpContext.Current.Request.MapPath(UploadPath)))
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Request.MapPath(UploadPath));
                file.SaveAs(path);
                fileResult.FileName = fileName;
                return fileResult;
            }
            catch (Exception ex)
            {
                // you might NOT want to show the exception error for the user
                // this is generaly logging or testing
                GlobalConstants._log.Error(ex.ToString());
                fileResult.Success = false;
                fileResult.ErrorMessage = ex.Message;
                return fileResult;
            }
        }

        private bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".xls":
                case ".xlsx":
                    return true;

                default:
                    return false;
            }
        }

        public static void RemoveUploadFile(string filePath)
        {
            if (String.IsNullOrEmpty(filePath)) return;
            try
            {
                File.Delete(filePath);
            }
            catch
            { }
        }
        public static DataTable ReadFileExcel(string filePath, string fileExt)
        {
            return QueryExcelData(filePath, fileExt);

        }

        private static DataTable QueryExcelData(string filePath, string fileExt)
        {
            DataTable tbl = new DataTable();
            try
            {
                string conStr = "";
                if (fileExt.ToUpper() == ".XLS" || fileExt.ToUpper() == "XLS")
                {
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + filePath + "'; Extended Properties ='Excel 8.0;HDR=Yes'";
                }
                else if (fileExt.ToUpper() == ".XLSX" || fileExt.ToUpper() == "XLSX")
                {
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + filePath + "'; Extended Properties ='Excel 12.0;HDR=Yes'";
                }
                conStr = string.Format(conStr, filePath);
                OleDbConnection cn = new OleDbConnection();
                cn.ConnectionString = conStr;
                OleDbCommand command = new OleDbCommand();
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                command.CommandText = "Select * From [Sheet1$]";
                command.Connection = cn;
                adapter.Fill(tbl);
            }
            catch (Exception ex)
            {
                GlobalConstants._log.Error(ex.ToString());
            }
            return tbl;
        }
    }
}