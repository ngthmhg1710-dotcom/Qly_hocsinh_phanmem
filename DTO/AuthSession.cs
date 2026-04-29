using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public static class AuthSession
    {
        // Biến này sẽ lưu ID giáo viên sau khi đăng nhập thành công
        // Nếu là Admin, biến này sẽ được gán giá trị = -1
        public static int CurrentIdGV { get; set; } = 0;

        public static string CurrentTenGV { get; set; }
        public static string CurrentEmail { get; set; }
    }
}