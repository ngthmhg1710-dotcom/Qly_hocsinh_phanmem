using System;

namespace DTO
{
    public class GiaoVienDTO
    {

        public int ID_GV { get; set; }
        public string HoTen { get; set; }
        public string Email_GV { get; set; }
        public string Password_GV { get; set; }


        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Mon { get; set; }
        public string SoDienThoai { get; set; }

        // 2. Constructor (Hàm dựng)
        public GiaoVienDTO() { }

        public GiaoVienDTO(int id, string hoTen, string email, string password, DateTime? ngaySinh, string gioiTinh, string mon, string sdt)
        {
            this.ID_GV = id;
            this.HoTen = hoTen;
            this.Email_GV = email;
            this.Password_GV = password;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.Mon = mon;
            this.SoDienThoai = sdt;
        }
    }
}