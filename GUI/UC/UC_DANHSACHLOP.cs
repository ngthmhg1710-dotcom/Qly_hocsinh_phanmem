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
    public partial class UC_DANHSACHLOP : UserControl
    {
        public UC_DANHSACHLOP()
        {
            InitializeComponent();
        }

        private void UC_DANHSACHLOP_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                UC_MON_GV uc = new UC_MON_GV();
                uc.Margin = new Padding(20);   // khoảng cách giữa các thẻ
                flpDanhSachLop.Controls.Add(uc);
            }
        }
    }
}
