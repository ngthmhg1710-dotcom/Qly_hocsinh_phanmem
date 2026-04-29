using DAL;
using System;
using System.Data;

namespace BUS
{
    public class HocSinhBUS
    {
        public static bool ThemHocSinh(string hoTen, DateTime ngaySinh, string gioiTinh, string danToc, int maLop)
        {
            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrWhiteSpace(hoTen)) return false;

            // Gọi DAL để lưu
            return HocSinhDAL.InsertHocSinh(hoTen, ngaySinh, gioiTinh, danToc, maLop);
        }
        // Thêm vào class HocSinhBUS
        public static bool DeleteHocSinh(int stt)
        {
            return HocSinhDAL.DeleteHocSinh(stt);
        }

        public static DataRow GetHocSinhById(int stt)
        {
            return HocSinhDAL.GetHocSinhById(stt);
        }

        public static bool UpdateHocSinh(int stt, string hoTen, DateTime ngaySinh, string gioiTinh, string danToc)
        {
            return HocSinhDAL.UpdateHocSinh(stt, hoTen, ngaySinh, gioiTinh, danToc);
        }
        public static bool KiemTraTenTonTai(string hoTen, int maLop)
        {
            return HocSinhDAL.CheckTenTonTai(hoTen, maLop);
        }
    }
}