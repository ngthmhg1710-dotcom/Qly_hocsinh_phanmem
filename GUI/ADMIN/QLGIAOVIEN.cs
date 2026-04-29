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
    public partial class QLGIAOVIEN : Form
    {
        public QLGIAOVIEN()
        {
            InitializeComponent();
        }

        private void btnQLLopHoc_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_QLGIAOVIEN());
        }
        private void ShowUserControl(UserControl control)
        {
            panelMain2.Controls.Clear(); // Xóa các control cũ trong panelMain
            control.Dock = DockStyle.Fill; // Cho UserControl chiếm hết panel
            panelMain2.Controls.Add(control); // Add UserControl mới vào panel
        }
    
        public void ShowDashboardOnSignIn()
        {
            btnQLGV.PerformClick();
        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            TRANGCHUADMIN signInForm = new TRANGCHUADMIN();
            signInForm.Show();
        }
       // private bool isInfoVisible = false;

        //private void guna2PictureBox1_Click(object sender, EventArgs e)
        //{

        //    // Đảo trạng thái hiển thị
        //    isInfoVisible = !isInfoVisible;

        //    // Gán lại cho nút
        //    btnInfor.Visible = isInfoVisible;
        //}

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            THONGTINADMIN ttAdmin = new THONGTINADMIN();
            ttAdmin.Show();
            this.Hide(); // Ẩn form đăng nhập
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            DANGNHAP signInForm = new DANGNHAP();
            signInForm.Show();
        }

        private void panelMain2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void QLGIAOVIEN_Load(object sender, EventArgs e)
        {
            //btnInfor.Visible = false; // ẩn khi chương trình khởi chạy
            //load font
            btnQLGV.Font = FontManager.GetFontByName("Montserrat", 12);
            btnQLMon.Font = FontManager.GetFontByName("Montserrat", 12);
            //btnThongKe.Font = FontManager.GetFontByName("Montserrat", 12);
            // 👉 Hiển thị mặc định "Quản lý Môn học" khi vừa mở form
            ShowUserControl(new UC_QLMONHOC());

        }

        private void btnQLMon_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_QLMONHOC());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }

        private void btnAvt_Click(object sender, EventArgs e)
        {
            MENU_CHINHSUA popupMenu = new MENU_CHINHSUA();
            Point screenPoint = btnAvt.PointToScreen(Point.Empty);


            popupMenu.InforButtonClicked += PopupMenu_InforButtonClicked;

            int x = screenPoint.X - popupMenu.Width + btnAvt.Width;
            int y = screenPoint.Y + btnAvt.Height + 5;

            popupMenu.Location = new Point(x, y);

            popupMenu.Show();
            popupMenu.Activate();
        }
        private void PopupMenu_InforButtonClicked(object sender, EventArgs e)
        {
            THONGTINADMIN ttAdmin = new THONGTINADMIN();
            ttAdmin.FormClosed += (s, ev) => this.Show();
            ttAdmin.Show();
            this.Hide();
        }

       
    }
}
