using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // Kế thừa từ DBConnection để sử dụng lại kết nối và các hàm tiện ích
    public class FlashcardDAL : DBConnection
    {
        // Lấy lịch sử các bộ thẻ
        public DataTable GetLichSuBoThe(int giaoVienID)
        {
            DataTable dt = new DataTable();
            try
            {
                // Sử dụng hàm mở kết nối từ lớp cha
                OpenConnection();

                // Sử dụng biến conn (protected) từ lớp cha
                using (SqlCommand cmd = new SqlCommand("sp_GetLichSuBoThe_Flashcard", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GV", giaoVienID);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                // Đảm bảo đóng kết nối
                CloseConnection();
            }
            return dt;
        }

        // Lấy chi tiết các thẻ trong 1 bộ
        public DataTable GetChiTietBoThe(int gameInstanceID)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand("sp_GetChiTietBoThe_Flashcard", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GameInstance", gameInstanceID);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // Lưu bộ thẻ mới
        public bool SaveBoThe(string tenBoThe, int giaoVienID, DataTable dtCards)
        {
            try
            {
                // Chuẩn bị tham số cho Stored Procedure
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@TenBoThe", tenBoThe),
                    new SqlParameter("@ID_GV", giaoVienID),
                    // Tham số kiểu Table-Valued Parameter (TVP)
                    new SqlParameter("@DanhSachThe", dtCards)
                    {
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "dbo.FlashcardTableType"
                    }
                };

                // Gọi hàm ExecuteNonQuery có sẵn ở DBConnection
                // Lưu ý: DBConnection.ExecuteNonQuery đã tự set CommandType = StoredProcedure
                ExecuteNonQuery("sp_LuuBoThe_Flashcard", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Xóa bộ thẻ
        public bool DeleteBoThe(int gameInstanceID)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID_GameInstance", gameInstanceID)
                };

                // Gọi hàm ExecuteNonQuery có sẵn ở DBConnection
                ExecuteNonQuery("sp_XoaBoThe_Flashcard", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}