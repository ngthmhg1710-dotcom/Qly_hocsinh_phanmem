using DTO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class HocSinhDAL : DBConnection
    {
        // Hàm thêm học sinh gọi Store Procedure: sp_InsertHocSinh
        public static bool InsertHocSinh(string hoTen, DateTime ngaySinh, string gioiTinh, string danToc, int maLop)
        {
            string query = "EXEC sp_InsertHocSinh @HoTen , @NgaySinh , @GioiTinh , @MaLop , @DanToc";

            return DataProvider.ExecuteNonQuery(query, new object[] {
                hoTen,
                ngaySinh,
                gioiTinh,
                maLop, // Mã lớp được truyền ngầm vào, người dùng không cần nhập
                danToc
            }) > 0;
        }
        // Thêm vào class HocSinhDAL
        // Trong file DAL/HocSinhDAL.cs

        public static bool DeleteHocSinh(int stt) // Hoặc int maHocSinh tùy code bạn
            {
                // Câu lệnh xóa (tùy theo SP của bạn tên là gì)
                string query = "DELETE FROM HocSinh WHERE STT = @STT";
                // Hoặc query = "EXEC sp_DeleteHocSinh @STT";

                try
                {
                    return DataProvider.ExecuteNonQuery(query, new object[] { stt }) > 0;
                }
                catch (SqlException ex)
                {
                    // 2. BẮT LỖI RÀNG BUỘC KHÓA NGOẠI (Mã lỗi 547)
                    if (ex.Number == 547)
                    {
                        // Ném lỗi tiếng Việt ra để GUI hiển thị
                        throw new Exception("Học sinh này đã có dữ liệu Điểm Danh hoặc Điểm Số.\nKhông thể xóa được!");
                    }

                    // Nếu là lỗi khác thì ném ra bình thường
                    throw ex;
                }
            }

    // Lấy thông tin 1 học sinh theo ID (STT)
    public static DataRow GetHocSinhById(int stt)
        {
            string query = "EXEC sp_GetHocSinhByID @STT"; // Thủ tục này đã có trong DB của bạn
            DataTable dt = DataProvider.ExecuteQuery(query, new object[] { stt });
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        // Cập nhật học sinh
        public static bool UpdateHocSinh(int stt, string hoTen, DateTime ngaySinh, string gioiTinh, string danToc)
        {
            string query = "EXEC sp_UpdateHocSinh @STT , @HoTen , @NgaySinh , @GioiTinh , @DanToc";
            return DataProvider.ExecuteNonQuery(query, new object[] { stt, hoTen, ngaySinh, gioiTinh, danToc }) > 0;
        }
        // Kiểm tra xem tên này đã có trong lớp chưa
        public static bool CheckTenTonTai(string hoTen, int maLop)
        {
            // Kiểm tra không phân biệt hoa thường (SQL mặc định là vậy)
            string query = "SELECT COUNT(*) FROM HocSinh WHERE HoTen = @HoTen AND MaLop = @MaLop";
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, new object[] { hoTen, maLop }));
            return count > 0;
        }
    }
}