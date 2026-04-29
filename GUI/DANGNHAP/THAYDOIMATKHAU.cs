using BUS;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class THAYDOIMATKHAU : Form
    {
        private string userEmail;        // Email được truyền từ form QUENMATKHAU
        private DangNhapBUS dangNhapBus; // Dùng để cập nhật mật khẩu

        // ✅ Constructor mặc định (Visual Studio cần để load Designer)
        public THAYDOIMATKHAU()
        {
            InitializeComponent();
        }

        // ✅ Constructor mới nhận 2 tham số (email + BUS)
        public THAYDOIMATKHAU(string email, DangNhapBUS bus)
        {
            InitializeComponent();
            userEmail = email;
            dangNhapBus = bus;
        }

        // Xử lý khi nhấn nút "Xác nhận thay đổi mật khẩu"
        private void btnXacNhanThayDoiMatKhau_Click(object sender, EventArgs e)
        {
            string matKhauMoi = txtMatKhauMoi.Text.Trim();
            string xacNhanMK = txtXacNhanMK.Text.Trim();

            if (string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(xacNhanMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (matKhauMoi != xacNhanMK)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool result = dangNhapBus.CapNhatMatKhau(userEmail, matKhauMoi);
            if (result)
            {
                MessageBox.Show("✅ Cập nhật mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Quay lại form đăng nhập
                DANGNHAP signIn = new DANGNHAP();
                signIn.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("❌ Không thể cập nhật mật khẩu. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void THAYDOIMATKHAU_Load(object sender, EventArgs e)
        {
            labelThayDoiMK.Font = FontManager.GetFontByName("Sigmar One", 28);
            labelMKMoi.Font = FontManager.GetFontByName("Montserrat", 12);
            labelXacNhanMK.Font = FontManager.GetFontByName("Montserrat", 12);
            checkBoxHienMK.Font = FontManager.GetFontByName("Montserrat", 12);
            btnThayDoiMK.Font = FontManager.GetFontByName("Montserrat", 10);
            txtMatKhauMoi.PasswordChar = '*'; // mặc định ẩn
            txtXacNhanMK.PasswordChar = '*'; // mặc định ẩn

        }

 
        //Ẩn/ hiện mật khẩu khi thay đổi mật khẩu
        private void checkBoxHienMK_CheckedChanged_1(object sender, EventArgs e)
        {
            bool hien = checkBoxHienMK.Checked;
            txtMatKhauMoi.PasswordChar = hien ? '\0' : '●';
            txtXacNhanMK.PasswordChar = hien ? '\0' : '●';
        }
        //Quay về Quên mật khẩu
        private void btnBack_Click(object sender, EventArgs e)
        {
            QUENMATKHAU backQuenMK = new QUENMATKHAU();
            backQuenMK.Show();
            this.Hide();
        }
    }
}
