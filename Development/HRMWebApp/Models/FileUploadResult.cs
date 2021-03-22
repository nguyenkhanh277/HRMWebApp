using HRMWebApp.Helper;
using System;
using System.Web;

namespace HRMWebApp.Models
{
    public class FileUploadResult
    {
        public bool Success { get; set; }
        public string FileName { get; set; }
        public string ErrorMessage { get; set; }
    }
}