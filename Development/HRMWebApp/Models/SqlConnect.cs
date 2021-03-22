using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace HRMWebApp
{
    class SqlConnect
    {
        public static bool Error = false;
        public static string ErrorMessage = "";
        static SqlConnection _SqlConnection = new SqlConnection();
        public static string ConnectionString
        {
            get { return _SqlConnection.ConnectionString; }
            set
            {
                try
                {
                    _SqlConnection = new SqlConnection(value);
                    _SqlConnection.Open();
                    _SqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Error = true;
                    ErrorMessage = ex.Message;
                }
            }
        }

        /*------------------ Các phương thức trả về một DataTable từ một câu lệnh truy vấn ------------------*/

        #region Các phương thức trả về một DataTable từ một câu lệnh truy vấn
        /// <summary>
        /// Phương thức thực thi câu lệnh truy vấn trả về kết quả là một DataTable
        /// </summary>
        /// <param name="TruyVan">Câu lệnh truy vấn</param>
        /// <returns>DataTable chứa kết quả truy vấn</returns>
        public static DataTable GetData(string Query)
        {
            Error = false;
            DataTable tbl = new DataTable();
            try
            {
                _SqlConnection.Open();
                SqlDataAdapter adp = new SqlDataAdapter(Query, _SqlConnection);
                adp.Fill(tbl);
                adp.Dispose();
            }
            catch (Exception ex)
            {
                Error = true;
                ErrorMessage = ex.Message;
            }
            finally
            {
                if (_SqlConnection.State != ConnectionState.Closed)
                    _SqlConnection.Close();
            }
            return tbl;
        }
        #endregion
        /*---------------------------------------------------------------------------------------------------*/


        /*---------------------------------- Các phương thức xử lý dữ liệu ----------------------------------*/

        #region Các phương thức xử lý dữ liệu
        /// <summary>
        /// Phương thức cho phép thực thi một câu lệnh sửa đổi dữ liệu
        /// </summary>
        /// <param name="Query">Câu lệnh Insert, Update hoặc Delete</param>
        public static void ExecuteQuery(string Query)
        {
            Error = false;
            try
            {
                _SqlConnection.Open();
                SqlCommand command = new SqlCommand(Query);
                command.Connection = _SqlConnection;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Error = true;
                ErrorMessage = ex.Message;
            }
            finally
            {
                if (_SqlConnection.State != ConnectionState.Closed)
                    _SqlConnection.Close();
            }
        }

        /// <summary>
        /// Phương thức thực thi một câu lệnh truy vấn sử dụng TRANSACTION
        /// </summary>
        /// <param name="Query">Câu lệnh Insert, Update hoặc Delete</param>
        public static void ExecuteQueryUsingTran(string Query)
        {
            ExecuteQuery("BEGIN TRANSACTION;" + Query + "; COMMIT;");
        }

        public static string InsertToTableString(string TableName, string[] Columns, object[] Values)
        {
            if (Columns.Length != Values.Length)
            {
                Error = true;
                ErrorMessage = "Số phần tử trong hai mảng Columns và Values phải bằng nhau";
                return "error";
            }
            string Query = "INSERT INTO " + TableName + "(";
            foreach (string st in Columns)
            {
                Query += st + ",";
            }
            Query = Query.Substring(0, Query.Length - 1) + ") VALUES(";
            foreach (object st in Values)
            {
                if (st == null)
                    Query += "null, ";
                else
                    if (st.ToString().IndexOf("'::money") > 0)
                    Query += st.ToString() + ", ";
                else
                    Query += "N'" + st.ToString() + "',";

            }
            Query = Query.Substring(0, Query.Length - 1) + ")";
            return Query + ";";
        }

        public static string UpdateToTableString(string TableName, string[] Columns, object[] Values, string Condition)
        {
            if (Columns.Length != Values.Length)
            {
                Error = true;
                ErrorMessage = "Số phần tử trong hai mảng Columns và Values phải bằng nhau";
                return "error";
            }
            string Query = "UPDATE " + TableName + " SET ";
            for (int i = 0; i < Columns.Length; i++)
            {
                if (Values[i] == null)
                    Query += "null, ";
                else
                    if (Values[i].ToString().IndexOf("'::money") > 0)
                    Query += Columns[i] + " = " + Values[i].ToString() + ", ";
                else
                    Query += Columns[i] + " = N'" + Values[i].ToString() + "',";
            }
            if (Condition == "")
                Query = Query.Substring(0, Query.Length - 1);
            else
                Query = Query.Substring(0, Query.Length - 1) + " WHERE " + Condition;
            return Query + ";";
        }

        public static string DeleteFromTableString(string TableName, string Condition)
        {
            return "DELETE FROM " + TableName + " WHERE " + Condition + ";";
        }
        #endregion
        /*---------------------------------------------------------------------------------------------------*/
    }
}
