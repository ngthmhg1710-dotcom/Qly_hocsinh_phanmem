using System;

public class AdminDTO
{
    public int ID_Admin { get; set; }
    public string HoTen { get; set; }
    public string Email_Admin { get; set; }
    public string Password_Admin { get; set; }
    // Các trường mới thêm
    public string SoDienThoai { get; set; }
    public string DiaChi { get; set; }
    public DateTime? NgaySinh { get; set; }
    public string GioiTinh { get; set; }
}