using System;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    public partial class FRM_NHAPDIEM : Form
    {
        private int _stt;
        private int _maMon;
        private LopHocBUS bus = new LopHocBUS();

        public FRM_NHAPDIEM(int stt, int maMon, string hoTen, string gk1, string hk1, string gk2, string hk2)
        {
            InitializeComponent();
            _stt = stt;
            _maMon = maMon;

            // --- PHẦN HIỂN THỊ THÔNG TIN HỌC SINH ---
            this.Text = "Nhập điểm cho học sinh"; // Tiêu đề Form

            // Gán thông tin vào Label (Bạn cần tạo 2 label này bên Design trước)
            // Kiểm tra null để tránh lỗi nếu quên chưa tạo Label
            if (lblShowSTT != null) lblShowSTT.Text = _stt.ToString();
            if (lblShowTen != null) lblShowTen.Text = hoTen;
            // -----------------------------------------

            txtGK1.Text = gk1;
            txtHK1.Text = hk1;
            txtGK2.Text = gk2;
            txtHK2.Text = hk2;

            // Gán sự kiện cho nút hủy
            if (btnHuy != null) btnHuy.Click += (s, e) => this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Chuyển đổi dữ liệu
                float fGK1 = string.IsNullOrEmpty(txtGK1.Text) ? 0 : float.Parse(txtGK1.Text);
                float fHK1 = string.IsNullOrEmpty(txtHK1.Text) ? 0 : float.Parse(txtHK1.Text);
                float fGK2 = string.IsNullOrEmpty(txtGK2.Text) ? 0 : float.Parse(txtGK2.Text);
                float fHK2 = string.IsNullOrEmpty(txtHK2.Text) ? 0 : float.Parse(txtHK2.Text);

                // 2. KIỂM TRA ĐIỀU KIỆN
                if (fGK1 < 0 || fGK1 > 10 || fHK1 < 0 || fHK1 > 10 ||
                    fGK2 < 0 || fGK2 > 10 || fHK2 < 0 || fHK2 > 10)
                {
                    MessageBox.Show("Điểm số không hợp lệ! Vui lòng nhập điểm từ 0 đến 10.",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Lưu dữ liệu
                if (bus.UpdateDiemHocSinh(_stt, _maMon, fGK1, fHK1, fGK2, fHK2))
                {
                    MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số (Ví dụ: 8.5)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}