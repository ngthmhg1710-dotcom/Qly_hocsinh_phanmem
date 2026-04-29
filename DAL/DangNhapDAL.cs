using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DangNhapDAL : DBConnection
    {
        //Kiểm tra xem email có phải Admin không
        public bool KiemTraLaAdmin(string email)
        {
            string query = "SELECT COUNT(*) FROM Admin WHERE Email_Admin = @Email";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }

        //Kiểm tra xem email có phải Giáo viên không
        public bool KiemTraLaGiaoVien(string email)
        {
            string query = "SELECT COUNT(*) FROM GiaoVien WHERE Email_GV = @Email";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }

        //Hàm đăng nhập Admin
        public bool DangNhapAdmin(string email, string password)
        {
            string query = "SELECT COUNT(*) FROM Admin WHERE Email_Admin = @Email AND Password_Admin = @Password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }
        // Trong file DAL/DangNhapDAL.cs

        public int DangNhapGiaoVien_ChiTiet(string email, string password)
        {
            // Câu lệnh lấy trạng thái BiKhoa nếu Email và Pass đúng
            string query = "SELECT BiKhoa FROM GiaoVien WHERE Email_GV = @Email AND Password_GV = @Password";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar(); // Lấy giá trị cột BiKhoa

                // Trường hợp 1: Không tìm thấy dòng nào (result == null)
                // => Sai Email hoặc Sai Mật khẩu
                if (result == null)
                {
                    return 0;
                }

                // Trường hợp 2: Tìm thấy, kiểm tra cột BiKhoa
                // Nếu BiKhoa = true (1) => Bị khóa
                if (result != DBNull.Value && Convert.ToBoolean(result) == true)
                {
                    return -1;
                }

                // Trường hợp 3: Còn lại => Thành công
                return 1;
            }
            finally
            {
                conn.Close();
            }
        }
        //Hàm đăng nhập Giáo viên
        // Trong file DangNhapDAL.cs

        // Hàm đăng nhập Giáo viên (Đã cập nhật kiểm tra BiKhoa)
        public bool DangNhapGiaoVien(string email, string password)
        {
            // Chỉ đếm những tài khoản đúng Email + Pass VÀ KHÔNG BỊ KHÓA (BiKhoa = 0 hoặc NULL)
            string query = @"
        SELECT COUNT(*) 
        FROM GiaoVien 
        WHERE Email_GV = @Email 
          AND Password_GV = @Password 
          AND (BiKhoa = 0 OR BiKhoa IS NULL)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            try
            {
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Nếu count > 0 nghĩa là đăng nhập thành công và không bị khóa
            }
            finally
            {
                conn.Close();
            }
        }
        // Kiểm tra email tồn tại (trong Admin hoặc Giáo viên)
        public bool KiemTraEmailTonTai(string email)
        {
            string query = @"
        SELECT COUNT(*) 
        FROM (
            SELECT Email_Admin AS Email FROM Admin
            UNION ALL
            SELECT Email_GV AS Email FROM GiaoVien
        ) AS AllEmails
        WHERE Email = @Email";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);

            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }
        // 🔹 Cập nhật mật khẩu mới theo email
        public bool CapNhatMatKhau(string email, string newPassword)
        {
            string query = @"
                UPDATE Admin SET Password_Admin = @Password WHERE Email_Admin = @Email;
                UPDATE GiaoVien SET Password_GV = @Password WHERE Email_GV = @Email;";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", newPassword);

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();

            return rows > 0;
        }
        public DataTable GetGiaoVienInfo(string email)
        {
            string query = "SELECT ID_GV, HoTen, Email_GV FROM GiaoVien WHERE Email_GV = @Email";
            return DataProvider.ExecuteQuery(query, new object[] { email });
        }

    }
}
