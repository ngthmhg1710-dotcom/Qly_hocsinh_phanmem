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
    public partial class UC_CONGCU_DONGHODEMNGUOC : UserControl
    {
        public UC_CONGCU_DONGHODEMNGUOC()
        {
            InitializeComponent();
        }

        private void UC_CONGCU_DONGHODEMNGUOC_Load(object sender, EventArgs e)
        {

        }

        private void txtDSGiaoVien_Click(object sender, EventArgs e)
        {

        }
        private bool isMenuHGVisible = false;
        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {
            // Đảo trạng thái hiển thị
            isMenuHGVisible = !isMenuHGVisible;

            // Gán lại cho nút
            btnhengio.Visible = isMenuHGVisible;
        }

        private void txtEmailGV_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
