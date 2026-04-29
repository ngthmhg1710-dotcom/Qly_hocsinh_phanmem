using System.Collections.Generic;
using DAL;
using DTO;
using DTO.Game; // <--- THÊM DÒNG NÀY ĐỂ NHẬN DIỆN ĐÚNG CLASS MỚI

namespace BUS
{
    public class XuLyNghiepVu
    {
        private TruyCapDuLieu dal = new TruyCapDuLieu();

        // Hàm này sẽ tự động hiểu CauHoiRandomDTO là DTO.Game.CauHoiRandomDTO
        public bool LuuGame(string tenGame, int idGV, List<CauHoiRandomDTO> questions)
        {
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