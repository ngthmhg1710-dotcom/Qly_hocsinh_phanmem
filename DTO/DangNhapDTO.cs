namespace DTO
{
    public class DangNhapDTO
    {
        //Thông tin giáo viên
        public int? ID_GV { get; set; }
        public string HoTen { get; set; }
        public string Email_GV { get; set; }
        public string Mon { get; set; }

        //Thông tin admin
        public int? ID_Admin { get; set; }
        public string Email_Admin { get; set; }

        //Tài khoản chung
        public string Email { get; set; }   
        public string Password { get; set; }

        public DangNhapDTO(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

    }
}
