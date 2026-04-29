using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Game;
using DTO;


namespace DAL
{
    // Phải có từ khóa 'public' ở đây
    public class TruyCapDuLieu
    {
        public bool LuuGameVaoDatabase(string tenGame, int idGV, List<CauHoiRandomDTO> questions)
        {
            try
            {
                DataTable questionTable = new DataTable();
                questionTable.Columns.Add("SoThuTu", typeof(int));
                questionTable.Columns.Add("NoiDungVanBan", typeof(string));
                questionTable.Columns.Add("DuongDanAnh", typeof(string));

                foreach (var q in questions)
                {
                    questionTable.Rows.Add(q.QuestionNumber, q.QuestionText, q.ImagePath);
                }

                using (SqlConnection conn = new SqlConnection(KetNoiCSDL.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_LuuGame_RandomSo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TenGame", tenGame);
                        cmd.Parameters.AddWithValue("@ID_GV", idGV);
                        SqlParameter tvpParam = cmd.Parameters.AddWithValue("@DanhSachCauHoi", questionTable);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        tvpParam.TypeName = "dbo.CauHoiRandomSoTableType";

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LichSuGameDTO> LayLichSuGame(int idGV)
        {
            List<LichSuGameDTO> list = new List<LichSuGameDTO>();
            using (SqlConnection conn = new SqlConnection(KetNoiCSDL.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetLichSuGame_RandomSo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GV", idGV);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new LichSuGameDTO
                        {
                            ID_GameInstance = Convert.ToInt32(reader["ID_GameInstance"]),
                            TenGame = reader["TenGame"].ToString(),
                            NgayTao = Convert.ToDateTime(reader["NgayTao"])
                        });
                    }
                }
            }
            return list;
        }

        public bool XoaGame(int idGameInstance)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(KetNoiCSDL.connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_XoaGame_RandomSo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_GameInstance", idGameInstance);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<CauHoiRandomDTO> LayChiTietGame(int idGameInstance)
        {
            List<CauHoiRandomDTO> questions = new List<CauHoiRandomDTO>();
            using (SqlConnection conn = new SqlConnection(KetNoiCSDL.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetChiTietGame_RandomSo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GameInstance", idGameInstance);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    foreach (DataRow row in dt.Rows)
                    {
                        questions.Add(new CauHoiRandomDTO
                        {
                            QuestionNumber = Convert.ToInt32(row["QuestionNumber"]),
                            QuestionText = row["QuestionText"].ToString(),
                            ImagePath = row["ImagePath"].ToString()
                        });
                    }
                }
            }
            return questions;
        }
    }
}