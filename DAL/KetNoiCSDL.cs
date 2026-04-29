namespace DAL
{
    public static class KetNoiCSDL
    {
        // QUAN TRỌNG: Hãy thay đổi các thông số dưới đây cho phù hợp với CSDL của bạn!
        // Your_Server_Name: Tên Server SQL của bạn (ví dụ: DESKTOP-ABC\SQLEXPRESS)
        // QuanLyHocSinh: Tên Database bạn đã tạo
        // Your_Username và Your_Password: Nếu bạn dùng SQL Server Authentication
        public static string connectionString =
            @"Data Source=DESKTOP-C7KPB1H;Initial Catalog=QuanLyHocSinh;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        // Nếu dùng SQL Server Authentication, chuỗi kết nối sẽ giống như sau:
        // public static string connectionString = @"Server=Your_Server_Name;Database=QuanLyHocSinh;User Id=Your_Username;Password=Your_Password;";
    }
}