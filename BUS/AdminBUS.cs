using System;
using DAL;
using DTO;

namespace BUS
{
    public static class AdminBUS
    {
        // 1. Lấy chi tiết Admin
        public static AdminDTO GetChiTietAdmin(int id)
        {
            return AdminDAL.GetAdminByID(id);
        }

        // 2. Cập nhật hồ sơ Admin
        public static bool UpdateProfileAdmin(int id, string ten, string email, string sdt, string diaChi, DateTime? ngaySinh, string gioiTinh, string passMoi)
        {
            // Kiểm tra dữ liệu
            if (id <= 0) return false;
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(email)) return false;

            // Gọi xuống DAL
            return AdminDAL.UpdateProfileAdmin(id, ten, email, sdt, diaChi, ngaySinh, gioiTinh, passMoi);
        }

        // 3. Lấy Admin theo Email (Dùng cho Đăng nhập)
        public static AdminDTO GetAdminByEmail(string email)
        {
            // Bạn cần đảm bảo DAL đã có hàm này, nếu chưa thì xem lại code DAL/AdminDAL.cs ở bước trước
            // Hoặc tạm thời dùng query trực tiếp nếu cần gấp (nhưng không khuyến khích)
            // Code chuẩn: return AdminDAL.GetAdminByEmail(email);

            // Tạm thời trả về null nếu chưa viết DAL
            return null;
        }
    }
}