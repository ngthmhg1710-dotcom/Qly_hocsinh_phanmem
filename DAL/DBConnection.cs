using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DBConnection
    {
        // 🔹 Chuỗi kết nối đến SQL Server (có thể chỉnh cho đúng)
        protected static string strCon =
            @"Data Source=DESKTOP-C7KPB1H;Initial Catalog=QuanLyHocSinh;Integrated Security=True;";

        // 🔹 Property public để lấy ConnectionString
        public static string ConnectionString
        {
            get { return strCon; }
        }

        // 🔹 SqlConnection protected
        protected SqlConnection conn = new SqlConnection(strCon);

        // 🔹 Hàm mở kết nối
        protected void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        // 🔹 Hàm đóng kết nối
        protected void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        // 🔹 Kiểm tra chuỗi kết nối
        public bool TestConnection()
        {
            try
            {
                OpenConnection();
                CloseConnection();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // 🔹 Thực thi SELECT → DataTable
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // 🔹 Thực thi INSERT/UPDATE/DELETE
        public void ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Rất quan trọng
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
