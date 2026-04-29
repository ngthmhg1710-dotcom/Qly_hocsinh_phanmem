using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.DIEMDANH
{
    public partial class MENUDIEMDANH : Form
    {
        public MENUDIEMDANH()
        {
            InitializeComponent();
        }

        private void MENUCONGCU_DIEMDANH_Load(object sender, EventArgs e)
        {

        }

        private void btnCameraDiemDanh_Click(object sender, EventArgs e)
        {
            CAMERA_DIEMDANH camDiemDanh = new CAMERA_DIEMDANH();
            camDiemDanh.Show();
            this.Hide();
        }

        private void btnTaoQR_Click(object sender, EventArgs e)
        {
           TAOQR newQR = new TAOQR();
            newQR.Show();
            this.Hide();
        }
    }
}
