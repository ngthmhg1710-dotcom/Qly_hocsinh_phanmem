using BUS;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class formThemHocSinh : Form
    {
        private int _maLop; // Biến lưu mã lớp hiện tại

        // Constructor nhận vào Mã Lớp
        public formThemHocSinh(int maLop)
        {
            InitializeComponent();
            _maLop = maLop; // Lưu mã lớp lại để dùng khi bấm nút Lưu

            // Cài đặt mặc định
            cmbGioiTinh.SelectedIndex = 0; // Mặc định chọn Nam
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Chuẩn hóa dữ liệu đầu vào
            // Xóa khoảng trắng thừa đầu/cuối và khoảng trắng kép ở giữa
            string hoTen = System.Text.RegularExpressions.Regex.Replace(txtHoTen.Text.Trim(), @"\s+", " ");
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = cmbGioiTinh.Text;
            string danToc = txtDanToc.Text.Trim();

            // 2. KIỂM TRA RỖNG
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            // 3. KIỂM TRA ĐẦY ĐỦ HỌ TÊN (Phải có ít nhất 2 từ)
            string[] cacTu = hoTen.Split(' '); // Cắt chuỗi theo khoảng trắng
            if (cacTu.Length < 2)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Họ và Tên (ví dụ: Nguyễn Văn A).\nTên nhập vào quá ngắn.",
                                "Tên không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            try
            {
                // 4. KIỂM TRA TRÙNG TÊN TRONG LỚP (Gọi hàm BUS mới thêm ở Bước 1)
                if (HocSinhBUS.KiemTraTenTonTai(hoTen, _maLop))
                {
                    MessageBox.Show($"Học sinh '{hoTen}' đã tồn tại trong lớp này rồi!\nVui lòng kiểm tra lại hoặc thêm ký hiệu phân biệt (A, B).",
                                    "Trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtHoTen.SelectAll();
                    txtHoTen.Focus();
                    return;
                }

                // 5. THỰC HIỆN THÊM
                if (HocSinhBUS.ThemHocSinh(hoTen, ngaySinh, gioiTinh, danToc, _maLop))
                {
                    MessageBox.Show("Thêm học sinh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}