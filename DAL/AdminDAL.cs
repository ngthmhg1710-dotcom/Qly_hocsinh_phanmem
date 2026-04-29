using System;
using System.Data;
using DTO;

namespace DAL
{
    public static class AdminDAL
    {
        // 1. Lấy chi tiết Admin
        public static AdminDTO GetAdminByID(int id)
        {
            string query = "SELECT * FROM Admin WHERE ID_Admin = " + id;
            DataTable dt = DataProvider.ExecuteQuery(query);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                return new AdminDTO
                {
                    ID_Admin = Convert.ToInt32(dr["ID_Admin"]),
                    HoTen = dr["HoTen"].ToString(),
                    Email_Admin = dr["Email_Admin"].ToString(),
                    SoDienThoai = dr["SoDienThoai"] != DBNull.Value ? dr["SoDienThoai"].ToString() : "",
                    DiaChi = dr["DiaChi"] != DBNull.Value ? dr["DiaChi"].ToString() : "",
                    GioiTinh = dr["GioiTinh"] != DBNull.Value ? dr["GioiTinh"].ToString() : "",
                    NgaySinh = dr["NgaySinh"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["NgaySinh"]) : null
                };
            }
            return null;
        }

        // 2. Cập nhật Profile Admin
        public static bool UpdateProfileAdmin(int id, string ten, string email, string sdt, string diaChi, DateTime? ngaySinh, string gioiTinh, string passMoi)
        {
            string query = "EXEC sp_UpdateProfile_Admin @ID_Admin , @HoTen , @Email_Admin , @SoDienThoai , @DiaChi , @NgaySinh , @GioiTinh , @Password_New";

            object passParam = string.IsNullOrEmpty(passMoi) ? (object)DBNull.Value : passMoi;
            object ngaySinhParam = ngaySinh.HasValue ? (object)ngaySinh.Value : DBNull.Value;

            return DataProvider.ExecuteNonQuery(query, new object[] {
                id, ten, email, sdt, diaChi, ngaySinhParam, gioiTinh, passParam
            }) > 0;
        }
    }
}