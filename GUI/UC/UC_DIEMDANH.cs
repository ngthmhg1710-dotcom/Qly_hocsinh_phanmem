using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS; // Gọi tầng BUS
using DTO; // Gọi tầng DTO

namespace GUI
{
    public partial class UC_DIEMDANH : UserControl
    {
        // Khởi tạo BUS
        private LopHocBUS bus = new LopHocBUS();
        private int _idGV; // ID Giáo viên đang đăng nhập

        public UC_DIEMDANH()
        {
            InitializeComponent();

            // Lấy ID GV từ Session đăng nhập
            _idGV = AuthSession.CurrentIdGV;

            // Đăng ký sự kiện Load
            this.Load += UC_DIEMDANH_Load;
        }

        private void UC_DIEMDANH_Load(object sender, EventArgs e)
        {
            // Load danh sách lớp ngay khi mở UC
            LoadComboBoxLop();

            // Trang trí giao diện bảng cho đẹp (nếu không dùng Guna2)
            StyleGrid(dgvHocSinh);
            StyleGrid(dgvLichSu);
        }

        // =================================================================================
        // PHẦN 1: LOAD DỮ LIỆU LỚP
        // =================================================================================
        private void LoadComboBoxLop()
        {
            try
            {
                // Gọi BUS lấy danh sách lớp của GV
                DataTable dt = bus.GetListLopCuaGV(_idGV);

                cboLop.DataSource = dt;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";

                // Gán sự kiện chọn lớp
                cboLop.SelectedIndexChanged -= CboLop_SelectedIndexChanged;
                cboLop.SelectedIndexChanged += CboLop_SelectedIndexChanged;

                // Tự động chọn lớp đầu tiên nếu có
                if (cboLop.Items.Count > 0)
                {
                    cboLop.SelectedIndex = 0;
                    // Gọi thủ công để load học sinh lớp đầu tiên
                    CboLop_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lớp: " + ex.Message);
            }
        }

        // Sự kiện khi GV chọn lớp khác
        private void CboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLop.SelectedValue != null && int.TryParse(cboLop.SelectedValue.ToString(), out int maLop))
            {
                LoadDanhSachHocSinh(maLop);
            }
        }

        // =================================================================================
        // PHẦN 2: LOAD DANH SÁCH HỌC SINH (BÊN TRÁI)
        // =================================================================================
        private void LoadDanhSachHocSinh(int maLop)
        {
            try
            {
                DataTable dt = bus.GetHocSinhByLop(maLop);
                dgvHocSinh.DataSource = dt;

                // Ẩn các cột không cần thiết (chỉ hiện Tên)
                if (dgvHocSinh.Columns.Contains("STT")) dgvHocSinh.Columns["STT"].Visible = false;
                if (dgvHocSinh.Columns.Contains("MaLop")) dgvHocSinh.Columns["MaLop"].Visible = false;

                // Đổi tên cột hiển thị
                if (dgvHocSinh.Columns.Contains("HoTen"))
                    dgvHocSinh.Columns["HoTen"].HeaderText = "Danh sách Học sinh";

                // Đăng ký sự kiện Click vào học sinh
                dgvHocSinh.CellClick -= DgvHocSinh_CellClick;
                dgvHocSinh.CellClick += DgvHocSinh_CellClick;

                // Reset bảng bên phải
                dgvLichSu.DataSource = null;
                if (lblTenHocSinh != null) lblTenHocSinh.Text = "Vui lòng chọn học sinh...";
            }
            catch (Exception ex)
            {
                // Có thể log lỗi
            }
        }

        // Sự kiện khi Click vào 1 học sinh
        private void DgvHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHocSinh.Rows[e.RowIndex];

                // Lấy STT để truy vấn lịch sử
                if (row.Cells["STT"].Value != null)
                {
                    int stt = Convert.ToInt32(row.Cells["STT"].Value);
                    string tenHS = row.Cells["HoTen"].Value.ToString();

                    if (lblTenHocSinh != null)
                        lblTenHocSinh.Text = $"Lịch sử điểm danh: {tenHS}";

                    // Load lịch sử của em này
                    LoadLichSu(stt);
                }
            }
        }

        // =================================================================================
        // PHẦN 3: LOAD LỊCH SỬ ĐIỂM DANH (BÊN PHẢI)
        // =================================================================================
        private void LoadLichSu(int stt)
        {
            try
            {
                // Gọi hàm BUS (Hàm này gọi SP sp_GetLichSuDiemDanhByHocSinh)
                DataTable dtLichSu = bus.GetLichSuDiemDanh(stt);
                dgvLichSu.DataSource = dtLichSu;

                if (dtLichSu.Rows.Count > 0)
                {
                    // Đặt tên cột tiếng Việt
                    dgvLichSu.Columns["Ngay"].HeaderText = "Ngày học";
                    dgvLichSu.Columns["ThoiGian"].HeaderText = "Giờ vào";
                    dgvLichSu.Columns["TenMon"].HeaderText = "Môn học";
                    dgvLichSu.Columns["TinhTrang"].HeaderText = "Trạng thái";
                    dgvLichSu.Columns["Buoi"].HeaderText = "Buổi";

                    // Định dạng hiển thị
                    dgvLichSu.Columns["Ngay"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
        }

        // =================================================================================
        // HÀM PHỤ TRỢ: TRANG TRÍ DATA GRID VIEW
        // =================================================================================
        private void StyleGrid(DataGridView dgv)
        {
            // Chỉ áp dụng nếu dgv khác null
            if (dgv == null) return;

            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false; // Ẩn cột đầu dòng

            // Màu sắc Header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh dương
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;

            // Màu sắc Rows
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219); // Xanh nhạt khi chọn
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.RowTemplate.Height = 30;
        }
    }
}