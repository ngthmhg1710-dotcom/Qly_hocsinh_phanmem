using BUS;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace GUI
{
    public partial class CAMERA_DIEMDANH : Form
    {
        // --- 1. KHAI BÁO BIẾN ---
        private LopHocBUS busLop = new LopHocBUS(); // Đổi tên biến cho rõ
        private int _idGV;

        // Biến lưu trạng thái đang chọn
        private int _selectedMaLop = 0;
        private int _selectedMaMon = 0;

        // Biến Camera
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice;
        private Timer timer;

        public CAMERA_DIEMDANH()
        {
            InitializeComponent();
            _idGV = AuthSession.CurrentIdGV;
        }

        private void CAMERA_DIEMDANH_Load(object sender, EventArgs e)
        {
            LoadCameraList();
            SetupDataGridView();
            LoadComboBoxLop();
        }

        private void LoadCameraList()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                cbbCamera.Items.Add(device.Name);

            if (cbbCamera.Items.Count > 0)
                cbbCamera.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvDiemDanh.Columns.Clear();
            dgvDiemDanh.Columns.Add("STT", "STT");
            dgvDiemDanh.Columns.Add("HoTen", "Họ Tên");
            // Các cột khác nếu cần, nhưng chủ yếu ta sẽ dùng DataSource
            dgvDiemDanh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // --- 2. LOGIC LOAD DATA ---

        private void LoadComboBoxLop()
        {
            try
            {
                DataTable dt = busLop.GetListLopCuaGV(_idGV);
                cboLop.DataSource = dt;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";

                // Đăng ký sự kiện
                cboLop.SelectedIndexChanged -= CboLop_SelectedIndexChanged;
                cboLop.SelectedIndexChanged += CboLop_SelectedIndexChanged;

                if (cboLop.Items.Count > 0)
                {
                    cboLop.SelectedIndex = 0;
                    // Kích hoạt sự kiện thủ công lần đầu
                    CboLop_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load lớp: " + ex.Message); }
        }

        private void CboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLop.SelectedValue != null && int.TryParse(cboLop.SelectedValue.ToString(), out int maLop))
            {
                _selectedMaLop = maLop;
                LoadComboBoxMon(maLop);
            }
        }

        private void LoadComboBoxMon(int maLop)
        {
            try
            {
                DataTable dt = busLop.GetListMonCuaGVTrongLop(_idGV, maLop);
                cboMon.DataSource = dt;
                cboMon.DisplayMember = "TenMon";
                cboMon.ValueMember = "MaMon";

                cboMon.SelectedIndexChanged -= CboMon_SelectedIndexChanged;
                cboMon.SelectedIndexChanged += CboMon_SelectedIndexChanged;

                if (cboMon.Items.Count > 0)
                {
                    cboMon.SelectedIndex = 0;
                    if (int.TryParse(cboMon.SelectedValue.ToString(), out int maMon))
                    {
                        _selectedMaMon = maMon;
                        // Khi đã có đủ Lớp và Môn -> Load danh sách đã điểm danh lên lưới
                        LoadDataLenGrid();
                    }
                }
            }
            catch (Exception ex) { /* Xử lý lỗi */ }
        }

        private void CboMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMon.SelectedValue != null && int.TryParse(cboMon.SelectedValue.ToString(), out int maMon))
            {
                _selectedMaMon = maMon;
                LoadDataLenGrid(); // Load lại lưới khi đổi môn
            }
        }

        // Hàm helper để load dữ liệu lên Grid
        private void LoadDataLenGrid()
        {
            if (_selectedMaLop > 0 && _selectedMaMon > 0)
            {
                try
                {
                    // Gọi qua BUS -> DAL -> SQL
                    DataTable dt = DiemDanhBUS.LayDanhSachTheoLopMon(_selectedMaLop, _selectedMaMon);
                    dgvDiemDanh.DataSource = dt;
                }
                catch { }
            }
        }

        // --- 3. XỬ LÝ CAMERA ---

        private void btnBatDau_Click(object sender, EventArgs e)
        {
            if (_selectedMaLop <= 0 || _selectedMaMon <= 0)
            {
                MessageBox.Show("Vui lòng chọn Lớp và Môn học trước!", "Cảnh báo");
                return;
            }

            if (filterInfoCollection.Count == 0)
            {
                MessageBox.Show("Không tìm thấy camera!");
                return;
            }

            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cbbCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();

            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();

            lblStatus.Text = "📷 Đang quét...";
            lblStatus.ForeColor = Color.Green;
        }

        private void btnDung_Click(object sender, EventArgs e)
        {
            StopCamera();
            lblStatus.Text = "Đã dừng camera.";
            lblStatus.ForeColor = Color.Red;
        }

        private void StopCamera()
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
                videoCaptureDevice.SignalToStop();
            if (timer != null)
                timer.Stop();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            picQR.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        // --- 4. XỬ LÝ QUÉT QR ---

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (picQR.Image == null) return;

            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode((Bitmap)picQR.Image);

            if (result != null)
            {
                XuLyDuLieuQR(result.Text);
            }
        }

        private void XuLyDuLieuQR(string qrText)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                try
                {
                    timer.Stop(); // Dừng quét để xử lý

                    // 1. Tách chuỗi
                    string[] parts = qrText.Split('|');
                    if (parts.Length < 3)
                    {
                        timer.Start();
                        return;
                    }

                    int stt = int.Parse(parts[0]);
                    string hoTen = parts[1];
                    int maLopInQR = int.Parse(parts[2]);

                    // 2. CHECK LOGIC: SAI LỚP
                    if (maLopInQR != _selectedMaLop)
                    {
                        System.Media.SystemSounds.Hand.Play();
                        MessageBox.Show(
                            $"⚠️ SAI LỚP!\n\nHS: {hoTen}\nLớp ID: {maLopInQR}\nHiện tại: {_selectedMaLop}",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        timer.Start();
                        return;
                    }

                    // 3. GỌI BUS ĐỂ LƯU (Không viết SQL ở đây nữa)
                    bool isSuccess = DiemDanhBUS.ThemDiemDanh(stt, maLopInQR, _selectedMaMon);

                    if (isSuccess)
                    {
                        System.Media.SystemSounds.Beep.Play();
                        lblStatus.Text = $"✅ Đã điểm danh: {hoTen}";

                        // Load lại lưới để thấy dòng mới
                        LoadDataLenGrid();
                    }
                    else
                    {
                        // BUS trả về false nghĩa là đã trùng (đã điểm danh rồi)
                        lblStatus.Text = $"⚠️ {hoTen} đã được điểm danh môn này rồi.";
                    }

                    // Xử lý xong, cho quét tiếp
                    timer.Start();
                }
                catch (Exception ex)
                {
                    timer.Stop();
                    // MessageBox.Show("Lỗi: " + ex.Message); // Có thể bật lên để debug
                    lblStatus.Text = "Lỗi xử lý QR.";
                    timer.Start();
                }
            }));
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            StopCamera();
        }
    }
}