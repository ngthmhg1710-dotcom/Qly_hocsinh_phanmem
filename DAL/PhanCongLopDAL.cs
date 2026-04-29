// File: PhanCongLopDAL.cs
using DTO;
using System.Data;
using System.Data.SqlClient; // Cần thêm

namespace DAL
{
    public class PhanCongLopDAL
    {
        public void XoaTheoGV(int idGV)
        {
            // Sử dụng SP đã định nghĩa: sp_XoaPhanCongLopChoGV
            string query = "EXEC sp_XoaPhanCongLopChoGV @ID_GV";
            SqlParameter param = new SqlParameter("@ID_GV", idGV);
            // Thay thế DataProvider bằng SqlHelper
            SqlHelper.ExecuteNonQuery(query, new SqlParameter[] { param });
        }

        public void LuuLop(PhanCongLopDTO dto)
        {
            // Sử dụng SP đã định nghĩa: sp_PhanCongGVLop
            string query = "EXEC sp_PhanCongGVLop @ID_GV , @MaLop , @VaiTro";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID_GV", dto.ID_GV),
                new SqlParameter("@MaLop", dto.MaLop),
                new SqlParameter("@VaiTro", dto.VaiTro)
            };
            // Thay thế DataProvider bằng SqlHelper
            SqlHelper.ExecuteNonQuery(query, parameters);
        }

        public DataTable GetLopTheoGV(int idGV)
        {
            // ⭐️ SỬA TÊN BẢNG TỪ 'PhanCongLop' -> 'GiaoVien_LopHoc' ⭐️
            string query = "SELECT MaLop, VaiTro FROM GiaoVien_LopHoc WHERE ID_GV=@ID_GV";

            SqlParameter param = new SqlParameter("@ID_GV", idGV);
            // Thay thế DataProvider bằng SqlHelper
            return SqlHelper.ExecuteQuery(query, new SqlParameter[] { param });
        }
    }
}