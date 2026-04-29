using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO; // Gọi Project DTO

namespace DAL
{
    public class GameSapXepCauDAL : DBConnection
    {
        // 1. Lấy lịch sử game theo giáo viên
        public List<GameSapXepCauDTO> GetLichSuGame(int idGV)
        {
            List<GameSapXepCauDTO> list = new List<GameSapXepCauDTO>();
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_GetLichSuGame_SapXepCau", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GV", idGV);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new GameSapXepCauDTO()
                            {
                                ID_GameInstance = Convert.ToInt32(reader["ID_GameInstance"]),
                                TenGame = reader["TenGame"].ToString(),
                                NgayTao = Convert.ToDateTime(reader["NgayTao"])
                            });
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }

        // 2. Lấy chi tiết câu hỏi
        public List<string> GetChiTietGame(int idGame)
        {
            List<string> listCauHoi = new List<string>();
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_GetChiTietGame_SapXepCau", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GameInstance", idGame);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listCauHoi.Add(reader["NoiDungCau"].ToString());
                        }
                    }
                }
            }
            finally
            {
                CloseConnection();
            }

            return listCauHoi;
        }

        // 3. Xóa game
        public bool XoaGame(int idGame)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_XoaGame_SapXepCau", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GameInstance", idGame);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        // 4. Lưu game mới
        public bool LuuGame(string tenGame, int idGV, List<string> danhSachCau)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_LuuGame_SapXepCau", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenGame", tenGame);
                    cmd.Parameters.AddWithValue("@ID_GV", idGV);

                    DataTable table = new DataTable();
                    table.Columns.Add("NoiDungCau", typeof(string));
                    foreach (var cau in danhSachCau)
                    {
                        table.Rows.Add(cau);
                    }

                    SqlParameter paramList = cmd.Parameters.AddWithValue("@DanhSachCauHoi", table);
                    paramList.SqlDbType = SqlDbType.Structured;
                    paramList.TypeName = "dbo.SapXepCauTableType";

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
