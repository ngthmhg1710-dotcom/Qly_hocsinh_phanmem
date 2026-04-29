using BUS;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class FRM_QUANLY_NOTE : Form
    {
        private LopHocBUS bus = new LopHocBUS();
        private int _idGV;

        // Các biến lưu trạng thái đang chọn
        private int _selectedMaLop = 0;
        private int _selectedMaMon = 0;
        private int _selectedSTT = 0;

        public FRM_QUANLY_NOTE()
        {
            InitializeComponent();
            _idGV = AuthSession.CurrentIdGV; // Lấy ID GV đang đăng nhập
        }

        private void FRM_QUANLY_NOTE_Load(object sender, EventArgs e)
        {
            LoadComboBoxLop();
        }

        // 1. Load danh sách lớp vào ComboBox
        private void LoadComboBoxLop()
        {
            try
            {
                DataTable dt = bus.GetListLopCuaGV(_idGV);
                cboLop.DataSource = dt;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";

                // Gán sự kiện sau khi load để tránh trigger nhầm
                cboLop.SelectedIndexChanged += CboLop_SelectedIndexChanged;

                // Kích hoạt chọn lớp đầu tiên nếu có
                if (cboLop.Items.Count > 0) CboLop_SelectedIndexChanged(null, null);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load lớp: " + ex.Message); }
        }

        // 2. Khi chọn Lớp -> Load Môn và Danh sách học sinh
        private void CboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLop.SelectedValue != null && int.TryParse(cboLop.SelectedValue.ToString(), out int maLop))
            {
                _selectedMaLop = maLop;
                LoadComboBoxMon(maLop);
                LoadDanhSachHocSinh(maLop);
            }
        }

        private void LoadComboBoxMon(int maLop)
        {
            DataTable dt = bus.GetListMonCuaGVTrongLop(_idGV, maLop);
            cboMon.DataSource = dt;
            cboMon.DisplayMember = "TenMon";
            cboMon.ValueMember = "MaMon";

            cboMon.SelectedIndexChanged -= CboMon_SelectedIndexChanged;
            cboMon.SelectedIndexChanged += CboMon_SelectedIndexChanged;

            if (cboMon.Items.Count > 0)
            {
                cboMon.SelectedIndex = 0;
                _selectedMaMon = Convert.ToInt32(cboMon.SelectedValue);
            }
        }

        private void LoadDanhSachHocSinh(int maLop)
        {
            // Tái sử dụng hàm lấy danh sách học sinh (nhưng chỉ cần tên và STT)
            // Hoặc dùng hàm GetChiTietHocSinhByLop cũng được
            DataTable dt = bus.GetHocSinhByLop(maLop);

            lstHocSinh.DataSource = dt;
            lstHocSinh.DisplayMember = "HoTen";
            lstHocSinh.ValueMember = "STT";

            lstHocSinh.SelectedIndexChanged -= LstHocSinh_SelectedIndexChanged;
            lstHocSinh.SelectedIndexChanged += LstHocSinh_SelectedIndexChanged;
        }

        // 3. Khi chọn Môn -> Cập nhật lại biến MaMon (và load lại note nếu đang chọn HS)
        private void CboMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMon.SelectedValue != null)
            {
                _selectedMaMon = Convert.ToInt32(cboMon.SelectedValue);
                LoadNoteCuaHocSinh(); // Tải lại note ứng với môn mới
            }
        }

        // 4. Khi chọn Học Sinh -> Load nội dung ghi chú lên
        private void LstHocSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHocSinh.SelectedValue != null && int.TryParse(lstHocSinh.SelectedValue.ToString(), out int stt))
            {
                _selectedSTT = stt;
                LoadNoteCuaHocSinh();
            }
        }

        private void LoadNoteCuaHocSinh()
        {
            if (_selectedSTT > 0 && _selectedMaMon > 0)
            {
                // Lấy note từ DB lên
                txtNhanXet.Text = bus.GetNoteHocSinh(_selectedSTT, _selectedMaMon);
            }
        }

        // 5. Nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_selectedSTT <= 0 || _selectedMaMon <= 0)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Lớp, Môn và Học sinh!");
                return;
            }

            if (bus.SaveNote(_idGV, _selectedSTT, _selectedMaMon, txtNhanXet.Text))
            {
                MessageBox.Show("Đã lưu ghi chú thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Lưu thất bại.");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}