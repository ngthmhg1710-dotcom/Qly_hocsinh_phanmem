using System.Collections.Generic;
using DAL; // Gọi DAL
using DTO; // Gọi DTO

namespace BUS
{
    public class GameSapXepCauBUS
    {
        private GameSapXepCauDAL dal = new GameSapXepCauDAL();

        public List<GameSapXepCauDTO> LayLichSuGame(int idGV)
        {
            return dal.GetLichSuGame(idGV);
        }

        public List<string> LayChiTietGame(int idGame)
        {
            return dal.GetChiTietGame(idGame);
        }

        public bool XoaGame(int idGame)
        {
            return dal.XoaGame(idGame);
        }

        public bool TaoGameMoi(string tenGame, int idGV, List<string> danhSachCau)
        {
            if (string.IsNullOrWhiteSpace(tenGame) || danhSachCau == null || danhSachCau.Count == 0)
                return false;

            return dal.LuuGame(tenGame, idGV, danhSachCau);
        }
    }
}