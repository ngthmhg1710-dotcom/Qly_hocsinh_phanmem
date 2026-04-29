// File: PhanCongMonDAL.cs

using DTO;
using System.Data;
using System.Data.SqlClient; // Cần thêm

namespace DAL
{
    public class PhanCongMonDAL
    {
        public void XoaTheoGV(int idGV)
        {
            // SỬ DỤNG Store Procedure (SP) đã định nghĩa: sp_XoaPhanCongMonChoGV
            string query = "EXEC sp_XoaPhanCongMonChoGV @ID_GV";
            SqlParameter param = new SqlParameter("@ID_GV", idGV);
            SqlHelper.ExecuteNonQuery(query, new SqlParameter[] { param });
        }

        public void LuuMon(PhanCongMonDTO dto)
        {
            // SỬ DỤNG Store Procedure (SP) đã định nghĩa: sp_PhanCongMonChoGV
            string query = "EXEC sp_PhanCongMonChoGV @ID_GV , @MaMon";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ID_GV", dto.ID_GV),
                new SqlParameter("@MaMon", dto.MaMon)
            };
            SqlHelper.ExecuteNonQuery(query, parameters);
        }

        public DataTable GetMonTheoGV(int idGV)
        {
            // ⭐️ SỬA TÊN BẢNG TỪ 'PhanCongMon' -> 'GiaoVien_MonHoc' ⭐️
            string query = "SELECT MaMon FROM GiaoVien_MonHoc WHERE ID_GV=@ID_GV";

            SqlParameter param = new SqlParameter("@ID_GV", idGV);
            return SqlHelper.ExecuteQuery(query, new SqlParameter[] { param });

            // Hoặc có thể dùng SP đã định nghĩa trong SQLQuery1.sql:
            // string query = "EXEC sp_GetPhanCongMon @ID_GV"; 
            // SqlParameter param = new SqlParameter("@ID_GV", idGV);
            // return SqlHelper.ExecuteQuery(query, new SqlParameter[] { param }); 
        }
    }
}