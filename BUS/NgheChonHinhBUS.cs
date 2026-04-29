using System;
using System.Collections.Generic;
using System.Data;
using DTO;
using DAL;

namespace BUS
{
    public class NgheChonHinhBUS
    {
        private readonly NgheChonHinhDAL dal = new NgheChonHinhDAL();

        public DataTable LayLichSuGame(int idGiaoVien)
        {
            return dal.GetLichSuGame(idGiaoVien);
        }

        public List<CauHoiHinhAnhDTO> LayChiTietGame(int gameInstanceId)
        {
            return dal.GetChiTietGame(gameInstanceId);
        }

        public bool TaoGameMoi(string tenGame, int idGiaoVien, List<CauHoiHinhAnhDTO> danhSachCauHoi)
        {
            // Validate dữ liệu ở tầng Business (nghiệp vụ)
            if (string.IsNullOrWhiteSpace(tenGame))
                throw new Exception("Tên game không được để trống.");

            if (danhSachCauHoi == null || danhSachCauHoi.Count == 0)
                throw new Exception("Danh sách câu hỏi không được trống.");

            return dal.LuuGame(tenGame, idGiaoVien, danhSachCauHoi);
        }

        public bool XoaGame(int gameInstanceId)
        {
            return dal.XoaGame(gameInstanceId);
        }
    }
}