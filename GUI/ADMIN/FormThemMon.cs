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
    public partial class formThemMon : Form
    {
        public formThemMon()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Trong sự kiện Click nút Lưu/Thêm
        // FILE: GUI/formThemMon.cs -> Sự kiện nút Lưu/Thêm
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon)) return;

            try
            {
                // InsertMonHoc giờ trả về false nếu trùng
                if (MonHocBUS.InsertMonHoc(tenMon))
                {
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show($"Môn '{tenMon}' đã tồn tại!", "Trùng tên", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenMon.SelectAll();
                    txtTenMon.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
