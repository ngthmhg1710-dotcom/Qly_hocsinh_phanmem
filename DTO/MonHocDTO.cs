namespace DTO
{
    public class MonHocDTO
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; }

        // Constructor mặc định
        public MonHocDTO() { }

        // Constructor có tham số (dùng khi cần khởi tạo nhanh)
        public MonHocDTO(int maMon, string tenMon)
        {
            MaMon = maMon;
            TenMon = tenMon;
        }
    }
}
