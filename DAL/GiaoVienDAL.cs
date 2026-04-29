using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Để bắt lỗi SQL
using DTO;

namespace DAL
{
    public static class GiaoVienDAL
    {
        // =============================================================
        // PHẦN 1: QUẢN LÝ GIÁO VIÊN (Dành cho Admin)
        // =============================================================

        // 1. Lấy danh sách giáo viên
        public static DataTable GetAllGiaoVien()
        {
            // Gọi SP mới bạn vừa chạy
            return DataProvider.ExecuteQuery("EXEC sp_GetAllGiaoVien_ChiTiet");
        }

        // 2. Thêm giáo viên mới (3 tham số)
        public static bool ThemGiaoVien(GiaoVienDTO gv)
        {
            string query = "EXEC sp_InsertGiaoVien @HoTen , @Email_GV , @Password_GV";
            return DataProvider.ExecuteNonQuery(query, new object[] {
                gv.HoTen,
                gv.Email_GV,
                gv.Password_GV
            }) > 0;
        }

        // 3. Cập nhật thông tin cơ bản (Admin sửa)
        public static bool UpdateGiaoVien(GiaoVienDTO gv)
        {
            string query = "EXEC sp_UpdateGiaoVien @ID_GV , @HoTen , @Email_GV";
            return DataProvider.ExecuteNonQuery(query, new object[] {
                gv.ID_GV,
                gv.HoTen,
                gv.Email_GV
            }) > 0;
        }

        // 4. Khóa / Mở tài khoản
        public static bool KhoaMoTaiKhoan(int idGV, bool trangThaiKhoa)
        {
            // Giả sử bảng GiaoVien có cột BiKhoa (bit)
            string query = "UPDATE GiaoVien SET BiKhoa = @TrangThai WHERE ID_GV = @ID_GV";
            // Hoặc dùng SP: "EXEC sp_KhoaMoTaiKhoan @ID_GV , @TrangThai"
            return DataProvider.ExecuteNonQuery(query, new object[] { trangThaiKhoa, idGV }) > 0;
        }

        // 5. Xóa giáo viên
        public static bool DeleteGiaoVien(int id_gv)
        {
            return DataProvider.ExecuteNonQuery("EXEC sp_DeleteGiaoVien @ID_GV", new object[] { id_gv }) > 0;
        }

        // =============================================================
        // PHẦN 2: THÔNG TIN CÁ NHÂN (Dành cho Giáo viên tự sửa)
        // =============================================================

        // 6. Lấy chi tiết 1 giáo viên theo ID
        public static GiaoVienDTO GetGiaoVienByID(int id)
        {
            string query = "SELECT * FROM GiaoVien WHERE ID_GV = " + id;
            DataTable dt = DataProvider.ExecuteQuery(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return new GiaoVienDTO
                {
                    ID_GV = Convert.ToInt32(dr["ID_GV"]),
                    HoTen = dr["HoTen"].ToString(),
                    Email_GV = dr["Email_GV"].ToString(),
                    // Xử lý null an toàn
                    SoDienThoai = dr.Table.Columns.Contains("SoDienThoai") && dr["SoDienThoai"] != DBNull.Value ? dr["SoDienThoai"].ToString() : "",
                    GioiTinh = dr.Table.Columns.Contains("GioiTinh") && dr["GioiTinh"] != DBNull.Value ? dr["GioiTinh"].ToString() : "",
                    NgaySinh = dr.Table.Columns.Contains("NgaySinh") && dr["NgaySinh"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["NgaySinh"]) : null
                };
            }
            return null;
        }

        // 7. Cập nhật Profile đầy đủ
        public static bool UpdateProfileGV(int id, string ten, string email, string sdt, string gioiTinh, DateTime? ngaySinh, string passMoi)
        {
            string query = "EXEC sp_UpdateProfile_GiaoVien @ID_GV , @HoTen , @Email_GV , @SoDienThoai , @GioiTinh , @Password_New";

            // Nếu pass rỗng -> DBNull
            object passParam = string.IsNullOrEmpty(passMoi) ? (object)DBNull.Value : passMoi;

            // Nếu Procedure của bạn chưa có tham số NgaySinh thì xóa dòng này đi
            // Ở đây mình giả định bạn đã chạy SP cập nhật ở các bước trước

            return DataProvider.ExecuteNonQuery(query, new object[] {
                id, ten, email, sdt, gioiTinh, passParam 
                // Lưu ý: Nếu SP của bạn có @NgaySinh thì thêm biến ngaySinh vào đây
            }) > 0;
        }

        // 8. Lấy chuỗi Môn dạy (Display Only)
        public static string GetMonDayCuaGV(int id)
        {
            string query = "SELECT TenMon FROM MonHoc WHERE MaMon IN (SELECT DISTINCT MaMon FROM PhanCongGiangDay WHERE ID_GV = " + id + ")";
            DataTable dt = DataProvider.ExecuteQuery(query);
            List<string> list = new List<string>();
            foreach (DataRow dr in dt.Rows) list.Add(dr["TenMon"].ToString());
            return string.Join(", ", list);
        }

        // 9. Lấy chuỗi Lớp dạy (Display Only)
        public static string GetLopDayCuaGV(int id)
        {
            string query = "SELECT TenLop FROM LopHoc WHERE MaLop IN (SELECT DISTINCT MaLop FROM PhanCongGiangDay WHERE ID_GV = " + id + ")";
            DataTable dt = DataProvider.ExecuteQuery(query);
            List<string> list = new List<string>();
            foreach (DataRow dr in dt.Rows) list.Add(dr["TenLop"].ToString());
            return string.Join(", ", list);
        }

        // =============================================================
        // PHẦN 3: HỖ TRỢ ĐĂNG NHẬP & TÌM KIẾM
        // =============================================================

        // 10. Lấy GV theo Email
        public static GiaoVienDTO GetGiaoVienByEmail(string email)
        {
            string query = "SELECT * FROM GiaoVien WHERE Email_GV = @Email";
            DataTable dt = DataProvider.ExecuteQuery(query, new object[] { email });

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return new GiaoVienDTO
                {
                    ID_GV = Convert.ToInt32(dr["ID_GV"]),
                    HoTen = dr["HoTen"].ToString(),
                    Email_GV = dr["Email_GV"].ToString(),
                    // Mấy cái này map tương tự như hàm GetByID
                    SoDienThoai = dr.Table.Columns.Contains("SoDienThoai") && dr["SoDienThoai"] != DBNull.Value ? dr["SoDienThoai"].ToString() : ""
                };
            }
            return null;
        }

        // =============================================================
        // PHẦN 4: PHÂN CÔNG GIẢNG DẠY
        // =============================================================

        public static DataTable GetChiTietPhanCong(int idGV)
        {
            string query = @"
                SELECT PC.MaLop, LH.TenLop, PC.MaMon, MH.TenMon 
                FROM PhanCongGiangDay PC
                JOIN LopHoc LH ON PC.MaLop = LH.MaLop
                JOIN MonHoc MH ON PC.MaMon = MH.MaMon
                WHERE PC.ID_GV = " + idGV;
            return DataProvider.ExecuteQuery(query);
        }

        public static void LuuPhanCongChinhXac(int idGV, DataTable dtPhanCong)
        {
            // Xóa cũ
            DataProvider.ExecuteNonQuery("EXEC sp_XoaPhanCongCuaGV @ID_GV", new object[] { idGV });

            // Thêm mới
            foreach (DataRow dr in dtPhanCong.Rows)
            {
                int maLop = Convert.ToInt32(dr["MaLop"]);
                int maMon = Convert.ToInt32(dr["MaMon"]);
                DataProvider.ExecuteNonQuery("EXEC sp_ThemPhanCong @ID_GV , @MaLop , @MaMon", new object[] { idGV, maLop, maMon });
            }
        }

        public static void LuuPhanCong(int idGV, List<int> listMaMon, List<int> listMaLop)
        {
            DataProvider.ExecuteNonQuery("EXEC sp_XoaPhanCongCuaGV @ID_GV", new object[] { idGV });

            foreach (int maLop in listMaLop)
            {
                foreach (int maMon in listMaMon)
                {
                    DataProvider.ExecuteNonQuery("EXEC sp_ThemPhanCong @ID_GV , @MaLop , @MaMon", new object[] { idGV, maLop, maMon });
                }
            }
        }

        // Các hàm hỗ trợ load Checkbox
        public static DataTable GetAllMonHoc() { return DataProvider.ExecuteQuery("SELECT * FROM MonHoc"); }
        public static DataTable GetAllLopHoc() { return DataProvider.ExecuteQuery("SELECT * FROM LopHoc"); }

        public static DataTable GetMonCuaGV(int idGV)
        {
            return DataProvider.ExecuteQuery("SELECT DISTINCT MaMon FROM PhanCongGiangDay WHERE ID_GV = " + idGV);
        }
        public static DataTable GetLopCuaGV(int idGV)
        {
            return DataProvider.ExecuteQuery("SELECT DISTINCT MaLop FROM PhanCongGiangDay WHERE ID_GV = " + idGV);
        }
        public static int UpdateGiaoVienFull(GiaoVienDTO gv)
        {
            // Lưu ý: Đảm bảo thứ tự tham số đúng y hệt như trong Stored Procedure
            string query = "EXEC sp_UpdateGiaoVienFull @ID_GV , @HoTen , @Email , @SoDienThoai , @NgaySinh , @GioiTinh";

            object[] param = {
        gv.ID_GV,
        gv.HoTen,
        gv.Email_GV,
        gv.SoDienThoai,
        gv.NgaySinh,
        gv.GioiTinh
    };

            try
            {
                // ExecuteScalar sẽ lấy giá trị của dòng SELECT đầu tiên trong SP (0 hoặc 1)
                object result = DataProvider.ExecuteScalar(query, param);

                if (result != null)
                    return Convert.ToInt32(result);

                return -1; // Lỗi không xác định
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần: Console.WriteLine(ex.Message);
                return -1; // Lỗi hệ thống/SQL
            }
        }
    }
}