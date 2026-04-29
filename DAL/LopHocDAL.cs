using DTO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class LopHocDAL : DBConnection
    {
        // 1. Lấy tất cả lớp
        public static DataTable GetAllLop()
        {
            return DataProvider.ExecuteQuery("EXEC sp_GetAllLop");
        }

        public DataTable GetAllLop_Expanded()
        {
            return DataProvider.ExecuteQuery("EXEC sp_GetAllLop_Expanded");
        }

        // 2. Lấy lớp theo khối
        public static DataTable GetLopByKhoi(string khoi)
        {
            string query = "SELECT * FROM LopHoc WHERE Khoi = @Khoi";
            return DataProvider.ExecuteQuery(query, new object[] { khoi });
        }

        // 3. THÊM LỚP (Sửa lại cho đúng vị trí)
        public static bool InsertLop(string tenLop, string khoi)
        {
            string query = "EXEC sp_ThemLop @TenLop , @Khoi , 0";
            try
            {
                return DataProvider.ExecuteNonQuery(query, new object[] { tenLop, khoi }) > 0;
            }
            catch (SqlException ex)
            {
                // Bắt lỗi trùng tên từ SQL
                if (ex.Number == 2627 || ex.Number == 2601) return false;
                throw ex;
            }
        }

        // 4. Thêm lớp dùng DTO (Nếu không dùng thì để return 0)
        public int InsertLop(LopHocDTO lop)
        {
            return 0;
        }

        // 5. Cập nhật lớp
        public bool UpdateLop(LopHocDTO lop)
        {
            string query = "EXEC sp_UpdateLop @MaLop, @TenLop, @Khoi, @SoLuong";
            return DataProvider.ExecuteNonQuery(query, new object[] {
                lop.TenLop, lop.TenLop, lop.Khoi, lop.SoLuongHocSinh
            }) > 0;
        }

        // 6. XÓA LỚP (Code chuẩn)
        public static bool DeleteLopHoc(int maLop)
        {
            // Không dùng try-catch ở đây để lỗi RAISERROR từ SQL truyền thẳng lên BUS
            string query = "EXEC sp_DeleteLopHoc @MaLop";
            return DataProvider.ExecuteNonQuery(query, new object[] { maLop }) > 0;
        }

        // ... CÁC HÀM KHÁC (GetHocSinhByLop, GetLopDayCuaGiaoVien...) GIỮ NGUYÊN ...
        public DataTable GetHocSinhByLop(int maLop)
        {
            string query = "EXEC sp_GetHocSinhByLop @MaLop";
            return DataProvider.ExecuteQuery(query, new object[] { maLop });
        }

        public DataTable GetChiTietHocSinhByLop(int maLop, int maMon)
        {
            string query = "EXEC sp_GetChiTietHocSinhByLop @MaLop , @MaMon";
            return DataProvider.ExecuteQuery(query, new object[] { maLop, maMon });
        }

        public DataTable GetLopDayCuaGiaoVien(int idGV)
        {
            string query = "EXEC sp_GetLopDayCuaGiaoVien @ID_GV";
            return DataProvider.ExecuteQuery(query, new object[] { idGV });
        }

        public bool UpdateDiemHocSinh(int stt, int maMon, float gk1, float ck1, float gk2, float ck2)
        {
            string query = "EXEC sp_UpdateDiemHocSinh @STT , @MaMon , @GK1 , @CK1 , @GK2 , @CK2";
            return DataProvider.ExecuteNonQuery(query, new object[] { stt, maMon, gk1, ck1, gk2, ck2 }) > 0;
        }

        public int GetMaMonCuaGiaoVien(int maLop, int idGV)
        {
            string query = "SELECT MaMon FROM PhanCongGiangDay WHERE MaLop = @MaLop AND ID_GV = @ID_GV";
            object result = DataProvider.ExecuteScalar(query, new object[] { maLop, idGV });
            if (result != null) return Convert.ToInt32(result);
            return 0;
        }

        public bool UpdateTuyenDuong(int stt, int maMon, bool trangThai)
        {
            string query = "EXEC sp_UpdateTuyenDuong @STT , @MaMon , @TrangThai";
            return DataProvider.ExecuteNonQuery(query, new object[] { stt, maMon, trangThai }) > 0;
        }
        // Trong file DAL/LopHocDAL.cs

        // Hàm kiểm tra xem trong bảng LopHoc đã có lớp nào thuộc khối này chưa
        public static bool CheckKhoiTonTai(string khoi)
        {
            // Đếm số lượng lớp thuộc khối này
            string query = "SELECT COUNT(*) FROM LopHoc WHERE Khoi = @Khoi";

            // ExecuteScalar trả về giá trị của cột đầu tiên dòng đầu tiên (số lượng)
            int count = Convert.ToInt32(DataProvider.ExecuteScalar(query, new object[] { khoi }));

            // Nếu đếm được > 0 nghĩa là Khối này đã tồn tại
            return count > 0;
        }
        public string GetTenLop(int maLop)
        {
            string query = "SELECT TenLop FROM LopHoc WHERE MaLop = @MaLop";
            object result = DataProvider.ExecuteScalar(query, new object[] { maLop });
            return result != null ? result.ToString() : "";
        }

        public string GetTenMon(int maMon)
        {
            string query = "SELECT TenMon FROM MonHoc WHERE MaMon = @MaMon";
            object result = DataProvider.ExecuteScalar(query, new object[] { maMon });
            return result != null ? result.ToString() : "";
        }

        public DataTable GetThongKeXepLoai(int maLop, int maMon)
        {
            string query = "EXEC sp_ThongKeXepLoai @MaLop , @MaMon";
            return DataProvider.ExecuteQuery(query, new object[] { maLop, maMon });
        }

        public DataTable GetListLopCuaGV(int idGV)
        {
            string query = "SELECT DISTINCT L.MaLop, L.TenLop FROM PhanCongGiangDay PC JOIN LopHoc L ON PC.MaLop = L.MaLop WHERE PC.ID_GV = @ID_GV";
            return DataProvider.ExecuteQuery(query, new object[] { idGV });
        }

        public DataTable GetListMonCuaGVTrongLop(int idGV, int maLop)
        {
            string query = "SELECT DISTINCT M.MaMon, M.TenMon FROM PhanCongGiangDay PC JOIN MonHoc M ON PC.MaMon = M.MaMon WHERE PC.ID_GV = @ID_GV AND PC.MaLop = @MaLop";
            return DataProvider.ExecuteQuery(query, new object[] { idGV, maLop });
        }

        public bool SaveNote(int idGV, int stt, int maMon, string nhanXet)
        {
            string query = "EXEC sp_SaveNote @ID_GV , @STT , @MaMon , @NhanXet";
            return DataProvider.ExecuteNonQuery(query, new object[] { idGV, stt, maMon, nhanXet }) > 0;
        }

        public string GetNoteHocSinh(int stt, int maMon)
        {
            string query = "EXEC sp_GetNoteHocSinh @STT , @MaMon";
            object result = DataProvider.ExecuteScalar(query, new object[] { stt, maMon });
            return result != null ? result.ToString() : "";
        }

    }
}