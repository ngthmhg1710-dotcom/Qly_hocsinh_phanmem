using System;
using System.Collections.Generic;
using System.Data;
using DAL; // Kết nối DAL
using DTO; // Kết nối DTO

namespace BUS
{
    public static class GiaoVienBUS
    {
        // =========================================================
        // PHẦN 1: QUẢN LÝ GIÁO VIÊN (Dành cho Admin - UC_QLGIAOVIEN)
        // =========================================================

        // 1. Lấy danh sách giáo viên
        public static DataTable LayDanhSachGiaoVien()
        {
            return GiaoVienDAL.GetAllGiaoVien();
        }

        // 2. Thêm giáo viên mới
        public static bool ThemGiaoVien(GiaoVienDTO gv)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(gv.HoTen) || string.IsNullOrEmpty(gv.Email_GV))
                return false;

            return GiaoVienDAL.ThemGiaoVien(gv);
        }

        // 3. Cập nhật thông tin giáo viên (Admin sửa Tên/Email)
        public static bool UpdateGiaoVien(GiaoVienDTO gv)
        {
            if (gv.ID_GV <= 0) return false;
            if (string.IsNullOrEmpty(gv.HoTen) || string.IsNullOrEmpty(gv.Email_GV))
                return false;

            return GiaoVienDAL.UpdateGiaoVien(gv);
        }

        // 4. Khóa / Mở tài khoản
        public static bool KhoaMoTaiKhoan(int idGV, bool trangThaiKhoa)
        {
            return GiaoVienDAL.KhoaMoTaiKhoan(idGV, trangThaiKhoa);
        }

        // 5. Xóa giáo viên (Nếu có dùng)
        public static bool XoaGiaoVien(int id)
        {
            return GiaoVienDAL.DeleteGiaoVien(id);
        }

        // =========================================================
        // PHẦN 2: THÔNG TIN CÁ NHÂN (Dành cho Giáo viên tự sửa)
        // =========================================================

        // 6. Lấy chi tiết 1 giáo viên theo ID
        public static GiaoVienDTO GetChiTietGiaoVien(int id)
        {
            return GiaoVienDAL.GetGiaoVienByID(id);
        }

        // 7. Giáo viên tự cập nhật hồ sơ (Có thêm SĐT, Ngày sinh...)
        public static bool UpdateProfileGV(int id, string ten, string email, string sdt, string gioiTinh, DateTime? ngaySinh, string passMoi)
        {
            if (id <= 0) return false;
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(email)) return false;

            return GiaoVienDAL.UpdateProfileGV(id, ten, email, sdt, gioiTinh, ngaySinh, passMoi);
        }

        // 8. Lấy thông tin Môn/Lớp dạy (Hiển thị ReadOnly)
        public static string GetMonDay(int id) => GiaoVienDAL.GetMonDayCuaGV(id);
        public static string GetLopDay(int id) => GiaoVienDAL.GetLopDayCuaGV(id);

        // =========================================================
        // PHẦN 3: HỖ TRỢ ĐĂNG NHẬP & PHÂN CÔNG
        // =========================================================

        // 9. Lấy thông tin GV theo Email (Dùng khi Login)
        public static GiaoVienDTO GetThongTinGiaoVien(string email)
        {
            return GiaoVienDAL.GetGiaoVienByEmail(email);
        }

        // 10. Lấy chi tiết phân công
        public static DataTable LayChiTietPhanCong(int idGV)
        {
            return GiaoVienDAL.GetChiTietPhanCong(idGV);
        }

        // 11. Lưu phân công (Checkbox hoặc List)
        public static void LuuPhanCong(int idGV, List<int> listMaMon, List<int> listMaLop)
        {
            GiaoVienDAL.LuuPhanCong(idGV, listMaMon, listMaLop);
        }

        public static void LuuPhanCongChinhXac(int idGV, DataTable dtPhanCong)
        {
            GiaoVienDAL.LuuPhanCongChinhXac(idGV, dtPhanCong);
        }

        // Các hàm hỗ trợ khác nếu cần (Load combobox...)
        public static DataTable LayToanBoMonHoc() { return GiaoVienDAL.GetAllMonHoc(); }
        public static DataTable LayToanBoLopHoc() { return GiaoVienDAL.GetAllLopHoc(); }

        public static List<int> LayDanhSachMaMonCuaGV(int idGV)
        {
            DataTable dt = GiaoVienDAL.GetMonCuaGV(idGV);
            List<int> list = new List<int>();
            foreach (DataRow dr in dt.Rows) list.Add(Convert.ToInt32(dr["MaMon"]));
            return list;
        }

        public static List<int> LayDanhSachMaLopCuaGV(int idGV)
        {
            DataTable dt = GiaoVienDAL.GetLopCuaGV(idGV);
            List<int> list = new List<int>();
            foreach (DataRow dr in dt.Rows) list.Add(Convert.ToInt32(dr["MaLop"]));
            return list;
        }
        public static string SuaGiaoVienFull(GiaoVienDTO gv)
        {
            // Gọi hàm DAO
            int ketQua = GiaoVienDAL.UpdateGiaoVienFull(gv);

            if (ketQua == 1)
            {
                return "Success"; // Thành công
            }
            else if (ketQua == 0)
            {
                return "DuplicateEmail"; // Trùng email
            }
            else
            {
                return "Failed"; // Lỗi khác (SQL, mất kết nối, v.v.)
            }
        }
    }
}