using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public static class DataProvider
    {
        // 🔗 Chuỗi kết nối (Giữ nguyên của bạn)
        private static string connectionString = @"Data Source=DESKTOP-C7KPB1H;Initial Catalog=QuanLyHocSinh;Integrated Security=True;";

        // 1. Hàm SELECT (Trả về DataTable)
        public static DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                AddParameters(command, query, parameter); // <--- Gọi hàm xử lý tham số
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }
            return data;
        }

        // 2. Hàm INSERT/UPDATE/DELETE (Trả về số dòng)
        public static int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                AddParameters(command, query, parameter);
                data = command.ExecuteNonQuery();
            }
            return data;
        }

        // 3. Hàm Đếm/Lấy 1 giá trị (Scalar)
        public static object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                AddParameters(command, query, parameter);
                data = command.ExecuteScalar();
            }
            return data;
        }

        // 🔥 HÀM XỬ LÝ THAM SỐ (Kết hợp cả Mới và Cũ)
        private static void AddParameters(SqlCommand command, string query, object[] parameter)
        {
            if (parameter != null)
            {
                // CASE 1: LOGIC MỚI (Dành cho Stored Procedure - Không có khoảng trắng)
                // Ví dụ: query = "sp_InsertHocSinh"
                if (!query.Contains(" "))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Tự động lấy tham số từ SQL
                    SqlCommandBuilder.DeriveParameters(command);

                    // Xóa tham số @RETURN_VALUE mặc định
                    if (command.Parameters.Count > 0 && command.Parameters[0].Direction == ParameterDirection.ReturnValue)
                    {
                        command.Parameters.RemoveAt(0);
                    }

                    // Gán giá trị
                    for (int i = 0; i < parameter.Length; i++)
                    {
                        if (i < command.Parameters.Count)
                        {
                            command.Parameters[i].Value = parameter[i];
                        }
                    }
                }
                // CASE 2: LOGIC CŨ CỦA BẠN (Dành cho câu SQL thường)
                // Ví dụ: query = "SELECT * FROM HocSinh WHERE ID = @id"
                else
                {
                    command.CommandType = CommandType.Text;

                    // 👇 ĐÂY LÀ ĐOẠN CODE CŨ CỦA BẠN (Giữ nguyên xi)
                    string[] listPara = query.Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
            }
        }
    }
}