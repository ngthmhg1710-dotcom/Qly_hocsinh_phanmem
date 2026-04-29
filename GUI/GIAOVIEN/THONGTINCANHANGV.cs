using BUS;
using DTO;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class THONGTINCANHANGV : Form
    {
        public THONGTINCANHANGV()
        {
            InitializeComponent();
        }

        private void THONGTINCANHANGV_Load(object sender, EventArgs e)
        {
            // 1. Kiểm tra đăng nhập
            if (AuthSession.CurrentIdGV == 0)
            {
                MessageBox.Show("Phiên đăng nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            try
            {
                // 2. Load thông tin cá nhân
                GiaoVienDTO gv = GiaoVienBUS.GetChiTietGiaoVien(AuthSession.CurrentIdGV);
                if (gv != null)
                {
                    txtHoTen.Text = gv.HoTen;
                    txtEmail.Text = gv.Email_GV;
                    txtSoDienThoai.Text = gv.SoDienThoai;
                    txtGioiTinh.Text = gv.GioiTinh; // Hoặc gán vào ComboBox: cboGioiTinh.Text = gv.GioiTinh;

                    if (gv.NgaySinh != null)
                        dtpNgaySinh.Value = gv.NgaySinh.Value;
                }

                // 3. Load thông tin giảng dạy (Chỉ xem, không sửa)
                txtMonDay.Text = GiaoVienBUS.GetMonDay(AuthSession.CurrentIdGV);
                txtLopDay.Text = GiaoVienBUS.GetLopDay(AuthSession.CurrentIdGV);

                // Khóa 2 ô này lại để tránh sửa nhầm
                txtMonDay.ReadOnly = true;
                txtLopDay.ReadOnly = true;
                // Nếu dùng Guna2TextBox thì dùng: txtMonDay.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string gioiTinh = txtGioiTinh.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;

            // Xử lý mật khẩu
            string passMoi = txtMatKhauMoi.Text.Trim();
            string xacNhan = txtXacNhanMatKhau.Text.Trim();

            if (passMoi != xacNhan)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Gọi BUS cập nhật
                if (GiaoVienBUS.UpdateProfileGV(AuthSession.CurrentIdGV, hoTen, email, sdt, gioiTinh, ngaySinh, passMoi))
                {
                    MessageBox.Show("Cập nhật hồ sơ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Xóa trắng ô mật khẩu để bảo mật
                    txtMatKhauMoi.Text = "";
                    txtXacNhanMatKhau.Text = "";

                    // Cập nhật lại tên hiển thị trên Session (để các form khác cập nhật theo)
                    AuthSession.CurrentTenGV = hoTen;
                    AuthSession.CurrentEmail = email;
                }
            }
            catch (Exception ex)
            {
                // Lỗi từ SQL (ví dụ trùng Email) sẽ hiện ở đây
                MessageBox.Show(ex.Message, "Không thể cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}