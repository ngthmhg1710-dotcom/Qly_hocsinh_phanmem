using GUI.DIEMDANH;
using GUI.TROCHOIHOCTAP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class TRANGCHU : Form
    {
        public TRANGCHU()
        {
            InitializeComponent();

            // --- THÊM DÒNG NÀY ---
            LoadCalendar();
        }
        private void TRANGCHU_Load(object sender, EventArgs e)
        {
            LoadCalendar(); // <--- THÊM MỚI: Gọi hàm này
            this.KeyPreview = true; // Bắt key trước các control con


        }

        private void NavigateToQLLopHoc()
        {
            // Tạo Form QLLOPHOC
            QLLOPHOC managerForm = new QLLOPHOC();

            // Hiển thị Form QLLOPHOC
            managerForm.Show();

            // Ẩn Form TRANGCHU này đi
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            NavigateToQLLopHoc();
        }

        // Nút "Điểm Danh" (trên Dashboard)
        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            MENUDIEMDANH menuDiemDanh = new MENUDIEMDANH();
            menuDiemDanh.Show();
        }

        // Nút "Công Cụ" (trên Dashboard)
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            NavigateToQLLopHoc();
        }

        // Nút "Trò Chơi" (trên Dashboard)
        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            NavigateToQLLopHoc();
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            NavigateToQLLopHoc();
            //LoadUC_DanhSachLop();
        }
        //sửa event khi bấm vào ảnh đại diện
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            MENU_CHINHSUA popupMenu = new MENU_CHINHSUA();
            Point screenPoint = btnAvt.PointToScreen(Point.Empty);

            popupMenu.InforButtonClicked += (s, ev) =>
            {
                popupMenu.Close(); // đóng popup
                this.Hide();       // ẩn form hiện tại (trang QL lớp)

                // Mở form THONGTINADMIN
                THONGTINCANHANGV ttAdmin = new THONGTINCANHANGV();
                ttAdmin.Show();
            };

            int x = screenPoint.X - popupMenu.Width + btnAvt.Width;
            int y = screenPoint.Y + btnAvt.Height + 5;
            popupMenu.Location = new Point(x, y);

            popupMenu.Show();
            popupMenu.Activate();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            THONGTINADMIN ttAdmin = new THONGTINADMIN();
            ttAdmin.Show();
            this.Hide(); // Ẩn form đăng nhập
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            DANGNHAP signInForm = new DANGNHAP();
            signInForm.Show();
        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            DANGNHAP signInForm = new DANGNHAP();
            signInForm.Show();
        }
        private void imageBtnCongCu_Click(object sender, EventArgs e)
        {
            MENUCONGCU managerForm = new MENUCONGCU();
            managerForm.Show();

        }

        private void imageBtnTroChoiHocTap_Click(object sender, EventArgs e)
        {
            MENUTROCHOI menuTrochoi = new MENUTROCHOI();
            menuTrochoi.Show();
        }


        private void NavigateToTroChoi()
        {
            // Tạo Form QLLOPHOC
            UC_TROCHOIHOCTAP managerForm = new UC_TROCHOIHOCTAP();

            // Hiển thị Form QLLOPHOC
            managerForm.Show();

            // Ẩn Form TRANGCHU này đi
            this.Hide();
        }

        private void btnTrangChu_Click_1(object sender, EventArgs e)
        {
            NavigateToQLLopHoc();
        }

     

 
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadCalendar()
        {
            // --- CẤU HÌNH ---
            int buttonSize = 40;
            int margin = 4;

            // 1. Lấy ngày hiện tại
            DateTime now = DateTime.Now;
            int currentDay = now.Day;
            int currentMonth = now.Month;
            int currentYear = now.Year;

            // 2. Tính toán ngày đầu tháng
            DateTime startOfMonth = new DateTime(currentYear, currentMonth, 1);
            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);
            int dayOfWeek = (int)startOfMonth.DayOfWeek;

            // 3. Xóa sạch sẽ
            flpCalendar.Controls.Clear();

            // PHẦN 1: TIÊU ĐỀ THÁNG
            Label lblMonth = new Label();
            lblMonth.Text = now.ToString("MMMM yyyy").ToUpper();
            lblMonth.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblMonth.ForeColor = Color.FromArgb(0, 51, 102);
            lblMonth.TextAlign = ContentAlignment.MiddleCenter;
            lblMonth.Width = flpCalendar.Width - 20;
            lblMonth.Height = 40;
            lblMonth.Margin = new Padding(0, 0, 0, 10);
            flpCalendar.Controls.Add(lblMonth);
            flpCalendar.SetFlowBreak(lblMonth, true);

            // PHẦN 2: TIÊU ĐỀ THỨ
            string[] daysOfWeekText = { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
            for (int i = 0; i < 7; i++)
            {
                Label lblDay = new Label();
                lblDay.Text = daysOfWeekText[i];
                lblDay.Size = new Size(buttonSize, buttonSize);
                lblDay.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblDay.TextAlign = ContentAlignment.MiddleCenter;
                lblDay.Margin = new Padding(margin);
                if (daysOfWeekText[i] == "Su") lblDay.ForeColor = Color.Tomato;
                else lblDay.ForeColor = Color.DimGray;
                flpCalendar.Controls.Add(lblDay);
                if (i == 6) flpCalendar.SetFlowBreak(lblDay, true);
            }

            // PHẦN 3: CÁC Ô NGÀY
            int cellCounter = 0;

            // Tạo ô trống đầu tháng
            for (int i = 0; i < dayOfWeek; i++)
            {
                Button btnEmpty = new Button();
                btnEmpty.Size = new Size(buttonSize, buttonSize);
                btnEmpty.FlatStyle = FlatStyle.Flat;
                btnEmpty.FlatAppearance.BorderSize = 0;
                btnEmpty.BackColor = Color.Transparent;
                btnEmpty.Enabled = false;
                btnEmpty.Margin = new Padding(margin);
                flpCalendar.Controls.Add(btnEmpty);
                cellCounter++;
                if (cellCounter % 7 == 0) flpCalendar.SetFlowBreak(btnEmpty, true);
            }

            // Tạo ô ngày chính thức
            for (int i = 1; i <= daysInMonth; i++)
            {
                Button btnDay = new Button();
                btnDay.Text = i.ToString();
                btnDay.Size = new Size(buttonSize, buttonSize);
                btnDay.FlatStyle = FlatStyle.Flat;
                btnDay.FlatAppearance.BorderSize = 0;
                btnDay.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                btnDay.Margin = new Padding(margin);

                // --- [MỚI] THÊM ĐOẠN NÀY ĐỂ CẮT NÚT THÀNH HÌNH TRÒN ---
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, buttonSize, buttonSize);
                btnDay.Region = new Region(path);
                // -----------------------------------------------------

                if (i == currentDay)
                {
                    btnDay.BackColor = Color.FromArgb(0, 51, 102);
                    btnDay.ForeColor = Color.White;
                    btnDay.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else
                {
                    btnDay.BackColor = Color.White;
                    DateTime checkDate = new DateTime(currentYear, currentMonth, i);
                    if (checkDate.DayOfWeek == DayOfWeek.Sunday)
                        btnDay.ForeColor = Color.Tomato;
                    else
                        btnDay.ForeColor = Color.Black;
                }

                btnDay.Click += (s, ev) => { MessageBox.Show("Ngày chọn: " + btnDay.Text); };
                flpCalendar.Controls.Add(btnDay);

                cellCounter++;
                if (cellCounter % 7 == 0) flpCalendar.SetFlowBreak(btnDay, true);
            }
        }


        private void panelMainMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OpenNote()
        {
            using (FRM_QUANLY_NOTE frm = new FRM_QUANLY_NOTE())
            {
                frm.ShowDialog();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.D1 || keyData == Keys.F12) // Bắt cả số 1 và F12
            {
                OpenNote();
                return true; // Chặn key không cho control con xử lý
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnGhiChuNhanh_Click(object sender, EventArgs e)
        {
            OpenNote();
        }

        private void panelMainMenu_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TRANGCHU_Load_1(object sender, EventArgs e)
        {

        }

        

    }
}
