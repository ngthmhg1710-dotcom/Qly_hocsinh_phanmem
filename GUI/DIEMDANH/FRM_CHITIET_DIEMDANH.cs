using BUS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.DIEMDANH
{
    public partial class FRM_CHITIET_DIEMDANH : Form
    {
        private LopHocBUS bus = new LopHocBUS();
        private int _stt;
        private string _tenHS;

        // Constructor: Chỉ nhận tham số, không chỉnh sửa giao diện Form
        public FRM_CHITIET_DIEMDANH(int stt, string tenHS)
        {
            InitializeComponent(); // Hàm này sẽ load giao diện bạn đã thiết kế
            _stt = stt;
            _tenHS = tenHS;
        }

        // Sự kiện Load: Chỉ nạp dữ liệu

        private void FRM_CHITIET_DIEMDANH_Load_1(object sender, EventArgs e)
        {
            // 1. Gán tên học sinh vào Label (nếu bạn có vẽ Label tên lblTieuDe)
            if (lblTieuDe != null)
            {
                lblTieuDe.Text = $"LỊCH SỬ ĐIỂM DANH: {_tenHS.ToUpper()}";
            }

            // 2. Load dữ liệu vào bảng
            LoadLichSu();

            // 3. Trang trí bảng (Nếu bạn đã chỉnh trong Properties rồi thì có thể bỏ dòng này)
            StyleGrid(dgvLichSu);
        }
        private void LoadLichSu()
        {
            try
            {
                var dt = bus.GetLichSuDiemDanh(_stt);
                dgvLichSu.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    // Đặt tên cột hiển thị tiếng Việt (Đảm bảo tên cột trong SQL/BUS trả về đúng như này)
                    if (dgvLichSu.Columns.Contains("Ngay")) dgvLichSu.Columns["Ngay"].HeaderText = "Ngày học";
                    if (dgvLichSu.Columns.Contains("ThoiGian")) dgvLichSu.Columns["ThoiGian"].HeaderText = "Giờ vào";
                    if (dgvLichSu.Columns.Contains("TenMon")) dgvLichSu.Columns["TenMon"].HeaderText = "Môn học";
                    if (dgvLichSu.Columns.Contains("TinhTrang")) dgvLichSu.Columns["TinhTrang"].HeaderText = "Trạng thái";
                    if (dgvLichSu.Columns.Contains("Buoi")) dgvLichSu.Columns["Buoi"].HeaderText = "Buổi";

                    // Định dạng cột ngày
                    if (dgvLichSu.Columns.Contains("Ngay"))
                        dgvLichSu.Columns["Ngay"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                else
                {
                    MessageBox.Show($"Học sinh {_tenHS} chưa có dữ liệu điểm danh nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // this.Close(); // Có muốn tự đóng form khi không có dữ liệu không? Tùy bạn chọn.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Hàm trang trí bảng (Giữ lại để bảng đẹp hơn, hoặc xóa nếu bạn đã chỉnh Properties)
        private void StyleGrid(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
        }

      
    }
}