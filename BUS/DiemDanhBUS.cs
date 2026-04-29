using System.Data;
using DAL;

namespace BUS
{
    public class DiemDanhBUS
    {
        // Gọi xuống DAL để thêm
        public static bool ThemDiemDanh(int stt, int maLop, int maMon)
        {
            return DiemDanhDAL.ThemDiemDanh(stt, maLop, maMon);
        }

        // Gọi xuống DAL để lấy danh sách hiển thị lên lưới
        public static DataTable LayDanhSachTheoLopMon(int maLop, int maMon)
        {
            return DiemDanhDAL.LayDiemDanhTheoLopMon(maLop, maMon);
        }
    }
}