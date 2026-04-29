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
    public partial class MENU_CHINHSUA : Form
    {
        public event EventHandler InforButtonClicked;
        public MENU_CHINHSUA()
        {
            InitializeComponent();
        }

        //private void btnChinhSuaTT_Click(object sender, EventArgs e)
        //{
        //    // Báo cho form cha biết đã bấm nút "Chỉnh sửa thông tin"
        //    InforButtonClicked?.Invoke(this, EventArgs.Empty);
        //    this.Close(); // Đóng menu popup
        //}

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // Đóng popup menu
            this.Close();

            // Đóng tất cả form khác (trừ form đăng nhập)
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (!(form is DANGNHAP))
                    form.Close();
            }

            // Mở lại form đăng nhập
            DANGNHAP signInForm = new DANGNHAP();
            signInForm.Show();
        }



        private void MENU_CHINHSUA_Load(object sender, EventArgs e)
        {

        }

        private void btnInfor_Click(object sender, EventArgs e)
        {

        }
    }
}
