using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; // Cần thiết để dùng DataRow/DataTable
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO; // Cần thiết để lưu Session

namespace GUI
{
    public partial class DANGNHAP : Form
    {
        // Khởi tạo BUS
        private DangNhapBUS busDangNhap = new DangNhapBUS();

        public DANGNHAP()
        {
            InitializeComponent();
        }

        private void DANGNHAP_Load(object sender, EventArgs e)
        {
            // Cài đặt Font chữ và giao diện ban đầu
            try
            {
                labelDangNhap.Font = FontManager.GetFontByName("Sigmar One", 28);
                labelMatKhau.Font = FontManager.GetFontByName("Montserrat", 12);
                labelEmail.Font = FontManager.GetFontByName("Montserrat", 12);
                checkboxShowPass.Font = FontManager.GetFontByName("Montserrat", 12);
                btnQuenMatKhau.Font = FontManager.GetFontByName("Montserrat", 10);
                btnLogin.Font = FontManager.GetFontByName("Montserrat", 10);
            }
            catch
            {
                // Nếu không tải được font thì bỏ qua để tránh lỗi crash
            }

            txtMatKhau.PasswordChar = '*'; // Mặc định ẩn mật khẩu
        }

        // =================================================================================
        // SỰ KIỆN NÚT ĐĂNG NHẬP (QUAN TRỌNG NHẤT)
        // =================================================================================
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // 1. Kiểm tra rỗng
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Email và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DangNhapDTO dn = new DangNhapDTO(email, matKhau);
            BUS.DangNhapBUS busDangNhap = new BUS.DangNhapBUS(); // Khởi tạo BUS nếu chưa có

            // 2. Xử lý Đăng nhập ADMIN
            if (busDangNhap.KiemTraLaAdmin(email))
            {
                if (busDangNhap.DangNhapAdmin(dn))
                {
                    // --- Lưu Session cho Admin ---
                    // Lưu AuthSession (cho các form cũ)
                    AuthSession.CurrentIdGV = -1;
                    AuthSession.CurrentTenGV = "Quản Trị Viên";

                    // Lưu UserSession (để tránh lỗi nếu Admin mở Lịch)
                    DTO.UserSession.StartSession(-1, "Quản Trị Viên");

                    MessageBox.Show("Đăng nhập Admin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TRANGCHUADMIN home = new TRANGCHUADMIN();
                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu Admin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // 3. Xử lý Đăng nhập GIÁO VIÊN
            else if (busDangNhap.KiemTraLaGiaoVien(email))
            {
                int ketQua = busDangNhap.DangNhapGiaoVien_ChiTiet(email, matKhau);

                if (ketQua == 1) // Thành công
                {
                    try
                    {
                        DataRow dr = busDangNhap.LayThongTinGiaoVien(email);

                        if (dr != null)
                        {
                            int id = Convert.ToInt32(dr["ID_GV"]);
                            string ten = dr["HoTen"].ToString();
                            string mail = dr["Email_GV"].ToString();

                            // --- BƯỚC 1: Vẫn giữ AuthSession của bạn (cho TRANGCHU) ---
                            AuthSession.CurrentIdGV = id;
                            AuthSession.CurrentTenGV = ten;
                            AuthSession.CurrentEmail = mail;

                            // --- BƯỚC 2: QUAN TRỌNG - THÊM DÒNG NÀY ---
                            // Phải có dòng này thì MainForm (Lịch) mới nhận diện được người dùng
                            DTO.UserSession.StartSession(id, ten);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi lấy thông tin: " + ex.Message);
                    }

                    MessageBox.Show($"Xin chào, {AuthSession.CurrentTenGV}!", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Chuyển sang Trang Chủ
                    // Lưu ý: Nếu TRANGCHU có nút mở Lịch (MainForm), nó sẽ hoạt động tốt nhờ Bước 2
                    TRANGCHU home = new TRANGCHU();
                    home.Show();
                    this.Hide();
                }
                else if (ketQua == -1)
                {
                    MessageBox.Show("Tài khoản đã bị KHÓA! Vui lòng liên hệ Admin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Email không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Sự kiện chuyển sang form Quên Mật Khẩu
        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            QUENMATKHAU forgotPass = new QUENMATKHAU();
            forgotPass.Show();
            this.Hide();
        }

        // Sự kiện ẩn/hiện mật khẩu
        private void checkboxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu check thì hiện chữ (null char), không check thì hiện dấu *
            txtMatKhau.PasswordChar = checkboxShowPass.Checked ? '\0' : '*';
        }

        // Các event TextChanged để trống (có thể xóa nếu không dùng)
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtMatKhau_TextChanged(object sender, EventArgs e) { }
        private void guna2PictureBox1_Click(object sender, EventArgs e) { }

        private void btnTatChuongTrinh_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn thoát chương trình?",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Application.Exit();
        }
    }
}