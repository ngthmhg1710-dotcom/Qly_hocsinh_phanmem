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
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelServer_Click(object sender, EventArgs e)
        {

        }

        private void labelDataBase_Click(object sender, EventArgs e)
        {

        }

        private void labelUID_Click(object sender, EventArgs e)
        {

        }

        private void labelPassWord_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxWinAuthen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Config_Load(object sender, EventArgs e)
        {
            // Thiết lập font cho các điều khiển
            labelKetNoiDatabase.Font = FontManager.GetFontByName("Montserrat", 11);
            labelServer.Font = FontManager.GetFontByName("Montserrat", 10);
            labelDataBase.Font = FontManager.GetFontByName("Montserrat", 10);
            labelUID.Font = FontManager.GetFontByName("Montserrat", 10);
            labelPassWord.Font = FontManager.GetFontByName("Montserrat", 10);
            checkBoxWinAuthen.Font = FontManager.GetFontByName("Montserrat", 9);
            btnLuu.Font = FontManager.GetFontByName("Montserrat", 9);
            btnHuy.Font = FontManager.GetFontByName("Montserrat", 9);
        }
    }
}
