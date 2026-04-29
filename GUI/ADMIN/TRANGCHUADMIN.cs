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
    public partial class TRANGCHUADMIN : Form
    {
        public TRANGCHUADMIN()
        {
            InitializeComponent();
        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
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


        private void btnQLGV_Click(object sender, EventArgs e)
        {
            this.Hide();
            QLGIAOVIEN signInForm = new QLGIAOVIEN();
            signInForm.Show();
        }

        private void btnQLMonHoc_Click(object sender, EventArgs e)
        {

        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }

        private void btnChinhSuaThongTin_Click(object sender, EventArgs e)
        {
            this.Hide();
            THONGTINADMIN chinhSuaTT = new THONGTINADMIN();
            chinhSuaTT.Show();
        }

        private void btnAvt_Click(object sender, EventArgs e)
        {
            MENU_CHINHSUA popupMenu = new MENU_CHINHSUA();
            Point screenPoint = btnAvt.PointToScreen(Point.Empty);

            popupMenu.InforButtonClicked += (s, ev) =>
            {
                popupMenu.Close(); // đóng popup
                this.Hide();       // ẩn form hiện tại (trang QL lớp)

                // Mở form THONGTINADMIN
                THONGTINADMIN ttAdmin = new THONGTINADMIN();
                ttAdmin.Show();
            };

            int x = screenPoint.X - popupMenu.Width + btnAvt.Width;
            int y = screenPoint.Y + btnAvt.Height + 5;
            popupMenu.Location = new Point(x, y);

            popupMenu.Show();
            popupMenu.Activate();
        }

        private void TRANGCHUADMIN_Load(object sender, EventArgs e)
        {
            LoadCalendar();

        }
        //Mở trang chủ mặc định vào QLGV


        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Hide();
            QLGIAOVIEN signInForm = new QLGIAOVIEN();
            signInForm.Show();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            THONGTINADMIN chinhSuaTT = new THONGTINADMIN();
            chinhSuaTT.Show();
        }
    }
}
