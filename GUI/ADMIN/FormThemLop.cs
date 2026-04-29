using System;
using System.Windows.Forms;
using BUS; // ⭐️ Nhớ using BUS

namespace GUI
{
    public partial class formThemLop : Form
    {
        public formThemLop()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Báo cho form cha biết là đã hủy
            this.Close();
        }

        // ⭐️ TẠO HÀM NÀY BẰNG CÁCH CLICK ĐÚP VÀO NÚT "LƯU" TRONG [DESIGN] ⭐️
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ 2 TextBox
            // (Hãy chắc chắn tên TextBox là txtTenLop và txtKhoi)
            string tenLop = txtTenLop.Text.Trim();
            string khoi = guna2TextBox1.Text.Trim(); // Dùng đúng tên TextBox

            // 2. Kiểm tra xem người dùng đã nhập đủ chưa
            if (string.IsNullOrEmpty(tenLop) || string.IsNullOrEmpty(khoi))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên Lớp và Khối.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng lại nếu thiếu
            }

            // 3. Gọi BUS để lưu (Giả sử LopHocBUS là static)
            try // Dùng try-catch để bắt lỗi từ BUS/DAL/SQL
            {
                // Gọi hàm ThemLop mà chúng ta đã chuẩn bị ở BUS
                bool success = LopHocBUS.ThemLop(tenLop, khoi);

                // 4. Phản hồi kết quả
                if (success)
                {
                    MessageBox.Show("Thêm lớp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Báo cho form cha biết là thành công
                    this.Close(); // Đóng form popup
                }
                else
                {
                    MessageBox.Show("Thêm lớp thất bại! Có thể tên lớp đã tồn tại hoặc có lỗi khác.", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi chi tiết nếu có vấn đề ở BUS/DAL/SQL
                MessageBox.Show("Đã xảy ra lỗi khi thêm lớp: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // (Hàm Load form nếu bạn cần thêm code gì khi form mở)
        private void formThemLop_Load(object sender, EventArgs e)
        {
            // Ví dụ: Đặt focus vào TextBox đầu tiên
            txtTenLop.Focus();
        }
    }
}