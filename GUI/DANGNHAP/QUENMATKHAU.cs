using BUS;
using System;
using System.Net.Mail;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class QUENMATKHAU : Form
    {
        private string userEmail;                 // Email người dùng nhập
        private string verificationCode;          // Mã OTP
        private DateTime codeExpirationTime;      // Hạn sử dụng OTP
        private DangNhapBUS dangNhapBus = new DangNhapBUS();

        public QUENMATKHAU()
        {
            InitializeComponent();
        }

        // Nút quay lại form đăng nhập
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DANGNHAP signIn = new DANGNHAP();
            signIn.Show();
            this.Hide();
        }

        // Gửi mã OTP
        private void btnGuiOTP_Click(object sender, EventArgs e)
        {
            userEmail = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(userEmail))
            {
                MessageBox.Show("Vui lòng nhập Email!");
                return;
            }

            if (dangNhapBus.KiemTraEmail(userEmail))
            {
                GenerateAndSendCode(userEmail);
                MessageBox.Show("✅ Mã xác nhận đã được gửi đến email của bạn!");
            }
            else
            {
                MessageBox.Show("❌ Email không tồn tại trong hệ thống!");
            }
        }

        // Xác nhận mã OTP
        private void btnXacNhanOTP_Click(object sender, EventArgs e)
        {
            // Ghép 6 ô lại thành 1 chuỗi
            string inputCode = txtOTP1.Text + txtOTP2.Text + txtOTP3.Text +
                               txtOTP4.Text + txtOTP5.Text + txtOTP6.Text;

            if (inputCode.Length < 6)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ 6 số!");
                return;
            }

            if (ValidateCode(inputCode))
            {
                MessageBox.Show("✅ Mã hợp lệ! Vui lòng đặt lại mật khẩu.");
                THAYDOIMATKHAU form = new THAYDOIMATKHAU(userEmail, dangNhapBus);
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("❌ Mã không hợp lệ hoặc đã hết hạn!");
            }
        }

        // 🔹 Sinh mã OTP ngẫu nhiên và gửi mail
        private void GenerateAndSendCode(string email)
        {
            Random rnd = new Random();
            verificationCode = rnd.Next(100000, 999999).ToString();
            codeExpirationTime = DateTime.Now.AddMinutes(3);

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("huongkiute1710@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Mã xác nhận đặt lại mật khẩu";
                mail.Body = $"Xin chào!\n\nMã xác nhận của bạn là: {verificationCode}\nMã sẽ hết hạn sau 3 phút.";

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(
                    "ngthmhg1710@gmail.com",
                    "dwrhyfsbqluunncy" // app password (không có \r\n)
                );
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gửi email: " + ex.Message);
            }
        }

        // 🔹 Kiểm tra mã OTP
        private bool ValidateCode(string inputCode)
        {
            return inputCode == verificationCode && DateTime.Now <= codeExpirationTime;
        }

        // Sự kiện Load form
        private void QUENMATKHAU_Load(object sender, EventArgs e)
        {
            // Cài font chữ (nếu bạn có FontManager riêng)
            labelXacNhanEmail.Font = FontManager.GetFontByName("Sigmar One", 24);
            labelEmail.Font = FontManager.GetFontByName("Montserrat", 12);
            labelOTP.Font = FontManager.GetFontByName("Montserrat", 12);
            btnOTP.Font = FontManager.GetFontByName("Montserrat", 12);
            btnXacNhan.Font = FontManager.GetFontByName("Montserrat", 12);

            // Gán sự kiện KeyPress và KeyDown cho 6 ô OTP
            txtOTP1.KeyPress += OTP_KeyPress;
            txtOTP2.KeyPress += OTP_KeyPress;
            txtOTP3.KeyPress += OTP_KeyPress;
            txtOTP4.KeyPress += OTP_KeyPress;
            txtOTP5.KeyPress += OTP_KeyPress;
            txtOTP6.KeyPress += OTP_KeyPress;

            txtOTP1.KeyDown += OTP_KeyDown;
            txtOTP2.KeyDown += OTP_KeyDown;
            txtOTP3.KeyDown += OTP_KeyDown;
            txtOTP4.KeyDown += OTP_KeyDown;
            txtOTP5.KeyDown += OTP_KeyDown;
            txtOTP6.KeyDown += OTP_KeyDown;

        }

        // Tự nhảy qua textbox kế tiếp khi nhập số
        private void OTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            Guna2TextBox currentBox = sender as Guna2TextBox;

            if (!char.IsControl(e.KeyChar))
            {
                // Khi nhập số thì tự nhảy qua ô kế tiếp
                this.SelectNextControl(currentBox, true, true, true, true);
            }
        }

        // Tự quay lại textbox trước khi bấm Backspace
        private void OTP_KeyDown(object sender, KeyEventArgs e)
        {
            Guna2TextBox currentBox = sender as Guna2TextBox;

            if (e.KeyCode == Keys.Back && string.IsNullOrEmpty(currentBox.Text))
            {
                this.SelectNextControl(currentBox, false, true, true, true);
            }
        }
        //Nút quay về Đăng Nhập
        private void btnBack_Click(object sender, EventArgs e)
        {
            DANGNHAP backDangNhap = new DANGNHAP();
            backDangNhap.Show();
            this.Hide();
        }
    }
}
