using BUS;
using System;
using System.Data;
using System.Windows.Forms;

namespace GUI
{
    public partial class formSuaHocSinh : Form
    {
        private int _stt; // ID học sinh cần sửa

        public formSuaHocSinh(int stt)
        {
            InitializeComponent();
            _stt = stt;

            // QUAN TRỌNG: Phải nạp item cho ComboBox trước khi gán dữ liệu
            cmbGioiTinh.Items.Clear();
            cmbGioiTinh.Items.Add("Nam");
            cmbGioiTinh.Items.Add("Nữ");
        }

        private void formSuaHocSinh_Load(object sender, EventArgs e)
        {
            LoadThongTinCu();
        }

        private void LoadThongTinCu()
        {
            try
            {
                // Kiểm tra xem ID nhận được có đúng không
                // MessageBox.Show("Đang tải thông tin cho STT: " + _stt); 

                DataRow dr = HocSinhBUS.GetHocSinhById(_stt);

                if (dr != null)
                {
                    // 1. Họ tên
                    if (dr.Table.Columns.Contains("HoTen"))
                        txtHoTen.Text = dr["HoTen"].ToString();

                    // 2. Ngày sinh
                    if (dr.Table.Columns.Contains("NgaySinh") && dr["NgaySinh"] != DBNull.Value)
                        dtpNgaySinh.Value = Convert.ToDateTime(dr["NgaySinh"]);

                    // 3. Giới tính (Quan trọng: Phải khớp chữ với Items trong ComboBox)
                    if (dr.Table.Columns.Contains("GioiTinh"))
                        cmbGioiTinh.Text = dr["GioiTinh"].ToString();

                    // 4. Dân tộc
                    if (dr.Table.Columns.Contains("DanToc"))
                        txtDanToc.Text = dr["DanToc"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu của học sinh có ID: " + _stt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu trống
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!");
                return;
            }

            try
            {
                if (HocSinhBUS.UpdateHocSinh(_stt, txtHoTen.Text, dtpNgaySinh.Value, cmbGioiTinh.Text, txtDanToc.Text))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Báo cho form cha biết là đã xong
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbGioiTinh_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}