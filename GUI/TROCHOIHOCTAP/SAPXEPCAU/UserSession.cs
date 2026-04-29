using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI // <-- QUAN TRỌNG: Phải trùng với namespace của DANGNHAP và MENUTROCHOI
{
    public static class UserSession
    {
        // Biến toàn cục lưu ID giáo viên sau khi đăng nhập
        public static int GiaoVienID { get; set; } = -1;

        // Lưu thêm Email nếu cần
        public static string Email { get; set; } = "";
    }
}