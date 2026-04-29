using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DiemDanhDAL
    {
        // Chuỗi kết nối (Nên để thống nhất 1 chỗ, tạm thời giữ như của bạn)
        private static string connStr = @"Data Source=DESKTOP-C7KPB1H;Initial Catalog=QuanLyHocSinh;Integrated Security=True;";

        // 1. Thêm điểm danh (INSERT)
        public static bool ThemDiemDanh(int stt, int maLop, int maMon)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertDiemDanh", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền đủ tham số xuống SQL
                    cmd.Parameters.AddWithValue("@STT", stt);
                    cmd.Parameters.AddWithValue("@MaLop", maLop);
                    cmd.Parameters.AddWithValue("@MaMon", maMon); // <--- QUAN TRỌNG
                    cmd.Parameters.AddWithValue("@Ngay", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@Buoi", "Sáng"); // Hoặc logic lấy theo giờ
                    cmd.Parameters.AddWithValue("@TinhTrang", "Có mặt");
                    cmd.Parameters.AddWithValue("@ThoiGian", DateTime.Now.ToString("HH:mm:ss"));

                    cmd.ExecuteNonQuery();
                }
                return true; // Thành công
            }
            catch (SqlException ex)
            {
                // Mã lỗi 2627 hoặc 2601 là lỗi trùng Khóa Chính (Duplicate Key)
                // Nghĩa là HS này đã được điểm danh Môn này rồi -> Trả về false
                if (ex.Number == 2627 || ex.Number == 2601)
                    return false;

                throw ex; // Lỗi khác (mất mạng, sai tên bảng...) thì ném ra
            }
            catch
            {
                return false;
            }
        }

        // 2. Lấy danh sách điểm danh theo Lớp + Môn (SELECT)
        public static DataTable LayDiemDanhTheoLopMon(int maLop, int maMon)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_GetDiemDanhByLop", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaLop", maLop);
                cmd.Parameters.AddWithValue("@MaMon", maMon);
                cmd.Parameters.AddWithValue("@Ngay", DateTime.Now.Date);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}