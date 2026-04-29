using GAME1_;
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

namespace GUI
{
    public partial class UC_CONGCU : UserControl
    {
        public UC_CONGCU()
        {
            InitializeComponent();
        }

        private void btnGoiTenNgauNhien_Click(object sender, EventArgs e)
        {
            GOITENNGAUNHIEN GTNN = new GOITENNGAUNHIEN();
            GTNN.Show();
        }

        private void btnBangTrang_Click(object sender, EventArgs e)
        {
            BANGTRANG bangTrang = new BANGTRANG();
            bangTrang.Show();
        }

        private void btnDongHoDemNguoc_Click(object sender, EventArgs e)
        {
            Calendar.MainForm mainForm = new Calendar.MainForm();
            mainForm.Show();
        }
    }
}
