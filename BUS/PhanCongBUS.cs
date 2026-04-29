using DAL;
using DTO;
using System.Data;
using System.Collections.Generic;

namespace BUS
{
    public class PhanCongBUS
    {
        private readonly PhanCongMonDAL dalMon = new PhanCongMonDAL();
        private readonly PhanCongLopDAL dalLop = new PhanCongLopDAL();

        // XÓA HẾT MÔN + LỚP TRƯỚC KHI LƯU MỚI
        public void XoaPhanCong(int idGV)
        {
            dalMon.XoaTheoGV(idGV);
            dalLop.XoaTheoGV(idGV);
        }

        // LƯU DANH SÁCH MÔN
        public void LuuDanhSachMon(int idGV, List<int> dsMon)
        {
            foreach (int mon in dsMon)
                dalMon.LuuMon(new PhanCongMonDTO(idGV, mon));
        }

        // LƯU DANH SÁCH LỚP
        public void LuuDanhSachLop(int idGV, List<PhanCongLopDTO> dsLop)
        {
            foreach (var lop in dsLop)
                dalLop.LuuLop(lop);
        }

        // LẤY MÔN
        public DataTable GetMon(int idGV)
        {
            return dalMon.GetMonTheoGV(idGV);
        }

        // LẤY LỚP
        public DataTable GetLop(int idGV)
        {
            return dalLop.GetLopTheoGV(idGV);
        }
    }
}
