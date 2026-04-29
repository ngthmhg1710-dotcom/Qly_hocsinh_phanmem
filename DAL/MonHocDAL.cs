

using DAL;
using System.Data;
using System.Data.SqlClient; // Thêm thư viện này để bắt SqlException

public static class MonHocDAL
{
    // SỬA HÀM INSERT: Để xử lý lỗi trùng tên
    public static bool InsertMonHoc(string tenMon)
    {
        tenMon = tenMon.Trim();
        string query = "EXEC sp_InsertMonHoc @TenMon";
        try
        {
            return DataProvider.ExecuteNonQuery(query, new object[] { tenMon }) > 0;
        }
        catch (SqlException ex)
        {
            // Mã lỗi 2627/2601: Vi phạm khóa Unique (Trùng tên)
            if (ex.Number == 2627 || ex.Number == 2601) return false;
            throw ex; // Lỗi khác thì ném ra
        }
    }

    // CÁC HÀM KHÁC GIỮ NGUYÊN HOẶC CẬP NHẬT NHƯ SAU:
    public static DataTable GetAllMonHoc()
    {
        return DataProvider.ExecuteQuery("SELECT * FROM MonHoc");
    }

    public static bool DeleteMonHoc(int maMon)
    {
        // Cho phép ném lỗi ra ngoài để GUI bắt (vì Procedure đã xử lý Cascade)
        return DataProvider.ExecuteNonQuery("EXEC sp_DeleteMonHoc @MaMon", new object[] { maMon }) > 0;
    }
}