using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DAL
{
    public class NgheChonHinhDAL
    {
        // 1. Lấy lịch sử game
        public DataTable GetLichSuGame(int idGiaoVien)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetLichSuGame_NgheChonHinh", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GV", idGiaoVien);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // 2. Lấy chi tiết câu hỏi của một game
        public List<CauHoiHinhAnhDTO> GetChiTietGame(int gameInstanceId)
        {
            List<CauHoiHinhAnhDTO> list = new List<CauHoiHinhAnhDTO>();
            using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetChiTietGame_NgheChonHinh", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GameInstance", gameInstanceId);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            List<string> anh = new List<string>
                            {
                                reader["DuongDanAnh1"].ToString(),
                                reader["DuongDanAnh2"].ToString(),
                                reader["DuongDanAnh3"].ToString()
                            };
                            string amThanh = reader["DuongDanAmThanh"].ToString();
                            int dapAn = Convert.ToInt32(reader["DapAnDung"]);

                            list.Add(new CauHoiHinhAnhDTO(anh, amThanh, dapAn));
                        }
                    }
                }
            }
            return list;
        }

        // 3. Lưu game mới
        public bool LuuGame(string tenGame, int idGiaoVien, List<CauHoiHinhAnhDTO> danhSachCauHoi)
        {
            try
            {
                // Tạo DataTable để map với User-Defined Table Type trong SQL
                DataTable cauHoiTable = new DataTable();
                cauHoiTable.Columns.Add("DuongDanAnh1", typeof(string));
                cauHoiTable.Columns.Add("DuongDanAnh2", typeof(string));
                cauHoiTable.Columns.Add("DuongDanAnh3", typeof(string));
                cauHoiTable.Columns.Add("DuongDanAmThanh", typeof(string));
                cauHoiTable.Columns.Add("DapAnDung", typeof(int));

                foreach (var ch in danhSachCauHoi)
                {
                    cauHoiTable.Rows.Add(ch.Anh[0], ch.Anh[1], ch.Anh[2], ch.AmThanh, ch.DapAn);
                }

                using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_LuuGame_NgheChonHinh", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenGame", tenGame);
                        cmd.Parameters.AddWithValue("@ID_GV", idGiaoVien);

                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@DanhSachCauHoi", cauHoiTable);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.CauHoiTableType"; // Đảm bảo tên Type trong SQL đúng

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                throw; // Ném lỗi về BUS để xử lý hoặc ghi log
            }
        }

        // 4. Xóa game
        public bool XoaGame(int gameInstanceId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DBConnection.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_XoaGame_NgheChonHinh", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_GameInstance", gameInstanceId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}