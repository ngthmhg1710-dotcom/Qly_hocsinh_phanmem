using BUS;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmPhanCongGiangDay : Form
    {
        private int _idGV;
        private string _tenGV;

        // Bảng tạm để lưu dữ liệu trên lưới trước khi bấm Lưu DB
        private DataTable _dtPhanCong;

        public frmPhanCongGiangDay(int idGV, string tenGV)
        {
            InitializeComponent();
            _idGV = idGV;
            _tenGV = tenGV;
        }

        private void frmPhanCongGiangDay_Load(object sender, EventArgs e)
        {
            // --- SỬA DÒNG NÀY ĐỂ HIỂN THỊ TÊN GV ---
            lblTieuDe.Text = "PHÂN CÔNG GIẢNG DẠY CHI TIẾT: " + _tenGV.ToUpper();
            // ----------------------------------------

            // Cập nhật tiêu đề cửa sổ (Title Bar)
            this.Text = "Phân công - Giáo viên: " + _tenGV;

            LoadComboBoxes();
            InitDataTable();
            LoadCurrentData();
        }

        // 1. Load danh sách vào 2 ComboBox
        private void LoadComboBoxes()
        {
            // Load Lớp
            cboLop.DataSource = GiaoVienBUS.LayToanBoLopHoc();
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";

            // Load Môn
            cboMon.DataSource = GiaoVienBUS.LayToanBoMonHoc();
            cboMon.DisplayMember = "TenMon";
            cboMon.ValueMember = "MaMon";
        }

        // 2. Tạo cấu trúc cho bảng dữ liệu hiển thị trên Grid
        private void InitDataTable()
        {
            _dtPhanCong = new DataTable();
            _dtPhanCong.Columns.Add("MaLop", typeof(int));
            _dtPhanCong.Columns.Add("TenLop", typeof(string));
            _dtPhanCong.Columns.Add("MaMon", typeof(int));
            _dtPhanCong.Columns.Add("TenMon", typeof(string));

            // Gán vào Grid
            dgvPhanCong.DataSource = _dtPhanCong;

            // Ẩn cột ID cho đẹp
            dgvPhanCong.Columns["MaLop"].Visible = false;
            dgvPhanCong.Columns["MaMon"].Visible = false;
            dgvPhanCong.Columns["TenLop"].HeaderText = "Lớp";
            dgvPhanCong.Columns["TenMon"].HeaderText = "Môn Giảng Dạy";
            dgvPhanCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // 3. Lấy dữ liệu cũ GV đang dạy để hiện lên
        private void LoadCurrentData()
        {
            DataTable dtCu = GiaoVienBUS.LayChiTietPhanCong(_idGV);

            // Copy dữ liệu từ DB vào bảng tạm của Grid
            foreach (DataRow dr in dtCu.Rows)
            {
                _dtPhanCong.Rows.Add(
                    dr["MaLop"],
                    dr["TenLop"],
                    dr["MaMon"],
                    dr["TenMon"]
                );
            }
        }

        // 4. Nút THÊM: Chọn Lớp + Môn -> Đẩy vào lưới
        private void btnThem_Click(object sender, EventArgs e)
        {
            int maLop = Convert.ToInt32(cboLop.SelectedValue);
            string tenLop = cboLop.Text;
            int maMon = Convert.ToInt32(cboMon.SelectedValue);
            string tenMon = cboMon.Text;

            // Kiểm tra trùng: Nếu trong lưới đã có cặp Lớp-Môn này rồi thì thôi
            foreach (DataRow row in _dtPhanCong.Rows)
            {
                if (Convert.ToInt32(row["MaLop"]) == maLop && Convert.ToInt32(row["MaMon"]) == maMon)
                {
                    MessageBox.Show("Đã tồn tại phân công này trong danh sách!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Thêm dòng mới
            _dtPhanCong.Rows.Add(maLop, tenLop, maMon, tenMon);
        }

        // 5. Nút XÓA: Xóa dòng đang chọn khỏi lưới
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhanCong.CurrentRow != null)
            {
                // Xóa khỏi DataTable (Grid sẽ tự cập nhật)
                int index = dgvPhanCong.CurrentRow.Index;
                _dtPhanCong.Rows[index].Delete();
            }
        }

        // 6. Nút LƯU: Gửi toàn bộ bảng xuống DB
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi BUS, truyền nguyên cái bảng _dtPhanCong xuống
                // BUS/DAL sẽ xóa cái cũ và insert lại y hệt những gì đang có trên lưới
                GiaoVienBUS.LuuPhanCongChinhXac(_idGV, _dtPhanCong);

                MessageBox.Show("Lưu phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}