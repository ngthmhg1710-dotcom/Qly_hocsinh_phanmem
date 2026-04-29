using System.Collections.Generic;
using DAL;
using DTO;

namespace BUS
{
    public class XuLyNghiepVu
    {
        private TruyCapDuLieu dal = new TruyCapDuLieu();

        public bool LuuGame(string tenGame, int idGV, List<CauHoiRandomDTO> questions)
        {
            // Logic kiểm tra cơ bản
            if (string.IsNullOrWhiteSpace(tenGame) || questions.Count == 0)
                return false;

            return dal.LuuGameVaoDatabase(tenGame, idGV, questions);
        }

        public List<LichSuGameDTO> LayDanhSachLichSu(int idGV)
        {
            return dal.LayLichSuGame(idGV);
        }

        public bool XoaGame(int idGameInstance)
        {
            return dal.XoaGame(idGameInstance);
        }

        public List<CauHoiRandomDTO> LayChiTietGame(int idGameInstance)
        {
            return dal.LayChiTietGame(idGameInstance);
        }
    }
}