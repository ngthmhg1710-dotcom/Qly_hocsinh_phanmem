// File: SqlHelper.cs (Đặt trong namespace DAL)

using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class SqlHelper
    {
        // Sử dụng chuỗi kết nối đã có
        private static string connectionString =
            @"Data Source=DESKTOP-C7KPB1H;Initial Catalog=QuanLyHocSinh2;Integrated Security=True;";

        // 📥 Hàm chạy câu SELECT → trả về DataTable
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    // Thêm tham số an toàn. Đây là điểm khác biệt cốt lõi.
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }
            return data;
        }

        // 📤 Hàm chạy lệnh INSERT/UPDATE/DELETE/EXEC SP
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    // Thêm tham số an toàn.
                    command.Parameters.AddRange(parameters);
                }
                rowsAffected = command.ExecuteNonQuery();
            }
            return rowsAffected;
        }
    }
}