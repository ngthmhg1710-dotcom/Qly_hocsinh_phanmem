namespace DTO
{
    public class LopHocDTO
    {
        public string TenLop { get; set; }
        public string TenGiaoVien { get; set; }
        public int SoLuongHocSinh { get; set; }
        public string MonGiangDay { get; set; }
        public string Khoi { get; set; }

        public LopHocDTO() { }

        public LopHocDTO(string tenLop, string tenGiaoVien, int soLuong, string monGiangDay, string khoi)
        {
            TenLop = tenLop;
            TenGiaoVien = tenGiaoVien;
            SoLuongHocSinh = soLuong;
            MonGiangDay = monGiangDay;
            Khoi = khoi;
        }
    }
}
