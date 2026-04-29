using BUS;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using System.IO;

namespace GUI
{
    public partial class TAOQR : Form
    {
        // 1. Khởi tạo BUS và các biến lưu trạng thái
        private LopHocBUS bus = new LopHocBUS();
        private int _idGV;

        private int _selectedMaLop = 0;
        private int _selectedMaMon = 0;

        private BarcodeWriter qrWriter;

        public TAOQR()
        {
            InitializeComponent();
            _idGV = AuthSession.CurrentIdGV; // Lấy ID GV đang đăng nhập

            // Cấu hình QR Code (Giữ nguyên)
            qrWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 200,
                    Width = 200,
                    Margin = 1,
                    CharacterSet = "UTF-8"
                }
            };
        }

        private void TAOQR_Load(object sender, EventArgs e)
        {
            LoadComboBoxLop();
        }

        // --- 2. LOGIC LOAD DỮ LIỆU (Giống hệt FRM_QUANLY_NOTE) ---

        // Bước 1: Load Lớp của GV
        private void LoadComboBoxLop()
        {
            try
            {
                DataTable dt = bus.GetListLopCuaGV(_idGV);
                cboLop.DataSource = dt;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";

                // Gán sự kiện
                cboLop.SelectedIndexChanged -= CboLop_SelectedIndexChanged;
                cboLop.SelectedIndexChanged += CboLop_SelectedIndexChanged;

                // Chọn dòng đầu tiên nếu có
                if (cboLop.Items.Count > 0)
                {
                    cboLop.SelectedIndex = 0;
                    CboLop_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load lớp: " + ex.Message); }
        }

        // Bước 2: Khi chọn Lớp -> Load Môn & Học sinh
        private void CboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLop.SelectedValue != null && int.TryParse(cboLop.SelectedValue.ToString(), out int maLop))
            {
                _selectedMaLop = maLop;

                // Gọi 2 hàm con
                LoadComboBoxMon(maLop);
                LoadDanhSachHocSinh(maLop);
            }
        }

        // Load Môn học theo Lớp và GV
        private void LoadComboBoxMon(int maLop)
        {
            try
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
            catch (Exception ex) { /* Xử lý lỗi nếu cần */ }
        }

        // Sự kiện khi chọn Môn
        private void CboMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMon.SelectedValue != null)
            {
                _selectedMaMon = Convert.ToInt32(cboMon.SelectedValue);
            }
        }

        // Load Danh sách học sinh vào DataGridView
        private void LoadDanhSachHocSinh(int maLop)
        {
            try
            {
                DataTable dt = bus.GetHocSinhByLop(maLop);
                dgvHocSinh.DataSource = dt;

                // Format cột nếu cần
                // dgvHocSinh.Columns["HoTen"].HeaderText = "Họ và tên";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load học sinh: " + ex.Message); }
        }

        // --- 3. TẠO MÃ QR ---

        

        // Hàm vẽ chữ (Giữ nguyên, chỉ chỉnh lại vị trí nếu muốn)
        private Bitmap ThemChuVaoQR(Bitmap qrImage, string dong1, string dong2)
        {
            int extraHeight = 60;
            Bitmap result = new Bitmap(qrImage.Width, qrImage.Height + extraHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.White);
                g.DrawImage(qrImage, 0, 0);

                using (Font fontTen = new Font("Segoe UI", 11, FontStyle.Bold))
                using (Font fontLop = new Font("Segoe UI", 9, FontStyle.Regular)) // Font nhỏ hơn chút cho dòng 2
                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center })
                {
                    // Tên học sinh
                    g.DrawString(dong1, fontTen, Brushes.Black,
                        new RectangleF(0, qrImage.Height, qrImage.Width, 30), sf);

                    // Lớp - Môn
                    g.DrawString(dong2, fontLop, Brushes.DarkSlateGray,
                        new RectangleF(0, qrImage.Height + 25, qrImage.Width, 30), sf);
                }
            }
            return result;
        }

        private void btnTaoTatCa_Click_1(object sender, EventArgs e)
        {
            if (_selectedMaLop <= 0 || dgvHocSinh.Rows.Count == 0) return;

            // Lấy tên lớp và tên môn để in lên ảnh QR cho đẹp
            string tenLop = cboLop.Text;
            string tenMon = cboMon.Text;
            string tieuDe = $"Lớp: {tenLop} - {tenMon}";

            Directory.CreateDirectory("QR_HocSinh");

            foreach (DataGridViewRow row in dgvHocSinh.Rows)
            {
                // Bỏ qua dòng trống hoặc dòng mới (new row) của DataGridView
                if (row.IsNewRow || row.Cells["STT"].Value == null) continue;

                string stt = row.Cells["STT"].Value.ToString();
                string ten = row.Cells["HoTen"].Value.ToString();

                // NỘI DUNG QR: Thường chỉ cần STT|Ten|MaLop
                // (Môn học dùng để hiển thị trên ảnh, không nhất thiết phải mã hóa vào QR 
                // trừ khi bạn muốn QR này chỉ dùng được cho môn đó)
                string data = $"{stt}|{ten}|{_selectedMaLop}";

                var qrImgRaw = qrWriter.Write(data);

                // Vẽ thêm chữ (Tên HS + Lớp + Môn)
                var qrFinal = ThemChuVaoQR(qrImgRaw, ten, tieuDe);

                qrFinal.Save($"QR_HocSinh/{tenLop}_{tenMon}_HS_{stt}.png");
            }
            MessageBox.Show($"✅ Đã tạo QR cho lớp {tenLop} - Môn {tenMon}!");
        }

        private void btnTaoMot_Click_1(object sender, EventArgs e)
        {
            if (dgvHocSinh.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn học sinh!");
                return;
            }

            var row = dgvHocSinh.SelectedRows[0];
            if (row.Cells["STT"].Value == null) return;

            string stt = row.Cells["STT"].Value.ToString();
            string hoTen = row.Cells["HoTen"].Value.ToString();
            string tenLop = cboLop.Text;
            string tenMon = cboMon.Text;

            string qrData = $"{stt}|{hoTen}|{_selectedMaLop}";

            var qrImgRaw = qrWriter.Write(qrData);

            // Hiển thị đầy đủ thông tin lên ảnh
            var qrFinal = ThemChuVaoQR(qrImgRaw, hoTen, $"{tenLop} - {tenMon}");

            picQR.Image = qrFinal;
            picQR.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã có ảnh QR trên PictureBox chưa
            if (picQR.Image == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh và tạo QR trước khi lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lấy thông tin để đặt tên file cho đẹp
            string tenFile = "QR_Code";
            if (dgvHocSinh.SelectedRows.Count > 0)
            {
                var row = dgvHocSinh.SelectedRows[0];
                string stt = row.Cells["STT"].Value.ToString();
                string ten = row.Cells["HoTen"].Value.ToString();
                string tenLop = cboLop.Text;
                // Tên file ví dụ: 10A1_NguyenVanA_01.png
                tenFile = $"{tenLop}_{ten}_{stt}";
            }

            // 3. Mở hộp thoại lưu file
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Lưu mã QR Học sinh";
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                sfd.FileName = tenFile;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Lưu ảnh
                        picQR.Image.Save(sfd.FileName);
                        MessageBox.Show("Đã lưu mã QR thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}