using DAL;
using DTO;
using System.Data;

namespace BUS
{
    public class DangNhapBUS
    {
        private DangNhapDAL dalDangNhap = new DangNhapDAL();

        public bool KiemTraLaAdmin(string email)
        {
            return dalDangNhap.KiemTraLaAdmin(email);
        }

        public bool KiemTraLaGiaoVien(string email)
        {
            return dalDangNhap.KiemTraLaGiaoVien(email);
        }

        public bool DangNhapAdmin(DangNhapDTO dn)
        {
            return dalDangNhap.DangNhapAdmin(dn.Email, dn.Password);
        }

        public bool DangNhapGiaoVien(DangNhapDTO dn)
        {
            return dalDangNhap.DangNhapGiaoVien(dn.Email, dn.Password);
        }
        // 🔹 Kiểm tra email tồn tại

        public bool KiemTraEmail(string email)
        {
            return dalDangNhap.KiemTraEmailTonTai(email);
        }
        // 🔹 Cập nhật mật khẩu
        public bool CapNhatMatKhau(string email, string newPassword)
        {
            return dalDangNhap.CapNhatMatKhau(email, newPassword);
        }
        // Trong file BUS/DangNhapBUS.cs

        public int DangNhapGiaoVien_ChiTiet(string email, string password)
        {
            return dalDangNhap.DangNhapGiaoVien_ChiTiet(email, password);
        }
        // Trả về DataRow đầu tiên tìm thấy
        public DataRow LayThongTinGiaoVien(string email)
        {
            DataTable dt = dalDangNhap.GetGiaoVienInfo(email);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

    }
}
