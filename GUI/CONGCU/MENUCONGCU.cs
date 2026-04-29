using Calendar;
using GUI.CONGCU.BANGTRANG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO; // <--- Thêm dòng này để dùng được UserSession

namespace GUI
{
    public partial class MENUCONGCU : Form
    {
        public MENUCONGCU()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            GOITENNGAUNHIEN GTNN = new GOITENNGAUNHIEN();
            GTNN.Show();
            this.Hide();
        }

        private void MENUCONGCU_Load(object sender, EventArgs e)
        {

        }

        private void btn_DONGHODEMNGUOC_Click(object sender, EventArgs e)
        {
         

            Calendar.MainForm mainForm = new Calendar.MainForm();
            mainForm.Show();
            this.Hide();
        }

        private void btnBangDienTu_Click(object sender, EventArgs e)
        {
            BANGTRANG bangTrang = new BANGTRANG();
            bangTrang.Show();
            this.Hide();
        }
    }
}
