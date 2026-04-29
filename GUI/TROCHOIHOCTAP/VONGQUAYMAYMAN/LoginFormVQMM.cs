// File: LoginForm.cs
using RANDOMSO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DAL;

namespace GAME1_ // << ĐỔI Namespace thành của project game
{
    // Lớp tĩnh (static) này sẽ lưu thông tin của giáo viên đã đăng nhập.
    // Dữ liệu này có thể được truy cập từ bất kỳ đâu trong ứng dụng.
    public static class UserSession
    {
        public static int TeacherId { get; private set; }
        public static string TeacherName { get; private set; }

        // Hàm này sẽ được gọi khi đăng nhập thành công
        public static void StartSession(int id, string name)
        {
            TeacherId = id;
            TeacherName = name;
        }

        // Hàm này có thể được gọi khi đăng xuất
        public static void EndSession()
        {
            TeacherId = 0;
            TeacherName = string.Empty;
        }
    }


    public partial class LoginFormVQMM : Form
    {
        // Sử dụng lớp DatabaseConnection để lấy chuỗi kết nối
        private readonly string connectionString = KetNoiCSDL.connectionString;

        public LoginFormVQMM()
        {
            InitializeComponent();

            // Dữ liệu mẫu để test nhanh, bạn có thể xóa 2 dòng này đi
            txtEmail.Text = "tranphuc@gmail.com";
            txtPassword.Text = "123456";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ email và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DangNhapGiaoVien", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email_GV", email);
                        cmd.Parameters.AddWithValue("@Password_GV", password);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            int teacherId = Convert.ToInt32(reader["ID_GV"]);
                            string teacherName = reader["HoTen"].ToString();

                            // Lưu thông tin giáo viên vào UserSession để các form khác sử dụng
                            UserSession.StartSession(teacherId, teacherName);

                            MessageBox.Show($"Chào mừng {teacherName} đã quay trở lại!", "Đăng nhập thành công");

                            // --- LOGIC CHUYỂN FORM ---
                            LSVONGQUAY mainMenu = new LSVONGQUAY();

                            // Khi form Menu chính đóng -> Đóng form đăng nhập (Thoát App)
                            mainMenu.FormClosed += (s, args) => this.Close();

                            mainMenu.Show(); // Hiện Menu
                            this.Hide();     // Ẩn Login
                        }
                        else
                        {
                            MessageBox.Show("Email hoặc mật khẩu không chính xác.", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}