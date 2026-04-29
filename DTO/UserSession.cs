using System;

namespace DTO
{
    // Class này phải là public static để truy cập toàn cục
    public static class UserSession
    {
        public static int TeacherId { get; set; } = 0; // Mặc định là 0
        public static string TeacherName { get; set; } = "";

        // Hàm lưu thông tin khi đăng nhập thành công
        public static void StartSession(int id, string name)
        {
            TeacherId = id;
            TeacherName = name;
        }

        // Hàm xóa thông tin khi đăng xuất
        public static void EndSession()
        {
            TeacherId = 0;
            TeacherName = string.Empty;
        }
    }
}