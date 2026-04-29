using System;
using System.Data;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    // Giả sử đây là code của FormDanhSachHocSinh.cs
    public partial class FormDanhSachHocSinh : Form
    {
        private int maLopDaChon; // 1. Lưu MaLop (kiểu int)
        private LopHocBUS bus = new LopHocBUS();

        // 2. Sửa Constructor để nhận INT
        public FormDanhSachHocSinh(int maLop)
        {
            InitializeComponent();
            this.maLopDaChon = maLop;
        }

        // 3. Trong sự kiện Load của Form
        private void FormDanhSachHocSinh_Load(object sender, EventArgs e)
        {
            LoadDanhSachHocSinh();
        }

        private void LoadDanhSachHocSinh()
        {
            try
            {
                // 4. Gọi hàm BUS mới bằng int
                DataTable dtHocSinh = bus.GetHocSinhByLop(this.maLopDaChon);

                // 5. Gán dữ liệu cho DataGridView
                // (Giả sử DataGridView của bạn tên là dgvHocSinh)
                dgvHocSinh.DataSource = dtHocSinh;
            }   
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách học sinh: " + ex.Message);
            }
        }
    }
}
