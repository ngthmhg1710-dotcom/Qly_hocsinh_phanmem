using System.Data;

public static class MonHocBUS
{
    public static bool InsertMonHoc(string tenMon)
    {
        return MonHocDAL.InsertMonHoc(tenMon);
    }

    public static DataTable GetAllMonHoc()
    {
        return MonHocDAL.GetAllMonHoc();
    }
    public static bool DeleteMonHoc(int maMon)
    {
        return MonHocDAL.DeleteMonHoc(maMon);
    }

}
