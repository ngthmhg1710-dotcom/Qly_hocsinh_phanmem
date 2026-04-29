using System;
using System.Drawing; // Dùng nếu muốn thay đổi màu sắc khi hover
using System.Windows.Forms;
using DTO;

namespace GUI
{
    public partial class UC_MON_GV : UserControl
    {
        // 1. Thuộc tính để lưu Mã Lớp (ẩn, dùng để xử lý logic khi click)
        public int MaLop { get; private set; }
        public int MaMon { get; private set; }

        // 2. Sự kiện (Event) để báo ra bên ngoài khi thẻ này được click
        public event EventHandler OnCardClick;

        public UC_MON_GV()
        {
            InitializeComponent();

            // Đăng ký sự kiện Click cho chính UserControl này
            this.Click += UC_MON_GV_Click;

            // Đăng ký sự kiện Click cho TẤT CẢ các control con bên trong 
            // (Để dù click vào chữ hay hình thì vẫn nhận sự kiện)
            RegisterClickEvent(this);
        }

        // Hàm đệ quy để gán sự kiện click cho mọi control con
        private void RegisterClickEvent(Control control)
        {
            foreach (Control c in control.Controls)
            {
                // Gán sự kiện click
                c.Click += UC_MON_GV_Click;

                // Gán hiệu ứng con trỏ chuột (Hand) để người dùng biết là bấm được
                c.Cursor = Cursors.Hand;

                // Nếu control con lại có control con nữa (VD: Panel chứa Label) -> Đệ quy tiếp
                if (c.HasChildren)
                {
                    RegisterClickEvent(c);
                }
            }
        }

        // 3. Hàm Public để truyền dữ liệu vào hiển thị
        public void SetData(int maLop, int maMon, string tenLop, string tenMon, int soLuong)
        {
            this.MaLop = maLop;
            this.MaMon = maMon;
            // Gán dữ liệu vào các Label giao diện
            // Kiểm tra null để tránh lỗi nếu lỡ tay xóa control bên Design
            if (lblTenLop != null) lblTenLop.Text = tenLop;

            if (lblTenMon != null)
                lblTenMon.Text = tenMon; // Hoặc "Môn: " + tenMon

            if (lblSoLuong != null)
                lblSoLuong.Text = "Sĩ số: " + soLuong.ToString();

            // Ví dụ: Có thể đổi màu nền ngẫu nhiên hoặc theo môn nếu muốn
            // this.BackColor = Color.LightBlue; 
        }

        // 4. Xử lý sự kiện chung: Khi click vào bất cứ đâu trên thẻ
        private void UC_MON_GV_Click(object sender, EventArgs e)
        {
            // Kích hoạt sự kiện OnCardClick để Form cha (UC_DANHSACHLOPVERSION2) bắt được
            OnCardClick?.Invoke(this, EventArgs.Empty);
        }

        // Các hàm sự kiện riêng lẻ do Designer sinh ra (có thể giữ hoặc bỏ nếu đã dùng RegisterClickEvent)
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            // Đã được xử lý chung bởi RegisterClickEvent, nhưng giữ lại cũng không sao
            OnCardClick?.Invoke(this, EventArgs.Empty);
        }

        // 5. (Tùy chọn) Hiệu ứng Hover chuột
        private void UC_MON_GV_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(230, 230, 230); // Làm tối màu một chút
        }

        private void UC_MON_GV_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White; // Trả về màu gốc
        }
    }
}