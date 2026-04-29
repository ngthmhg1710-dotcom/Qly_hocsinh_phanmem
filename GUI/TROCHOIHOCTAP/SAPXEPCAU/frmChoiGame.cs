using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS;
using System.IO; // Thêm thư viện này để xử lý file
using GUI.Properties;
namespace SapXepCau
{
    public partial class frmChoiGame : Form
    {
        // Các biến lưu trữ thông tin game và người dùng
        private int _idGame;
        private string _tenGame;
        private int _idGV;
        private GameSapXepCauBUS bus = new GameSapXepCauBUS();

        // Các biến xử lý logic trò chơi
        private List<string> _danhSachCauHoi;
        private int _cauHienTaiIndex = 0;
        private string _cauDungHienTai = "";

        // --- KHAI BÁO BIẾN ÂM THANH ---
        // Sử dụng WMPLib (Windows Media Player)
        private WMPLib.WindowsMediaPlayer _nhacNenPlayer = new WMPLib.WindowsMediaPlayer();
        private WMPLib.WindowsMediaPlayer _sfxPlayer = new WMPLib.WindowsMediaPlayer(); // Dùng cho hiệu ứng (click, đúng, sai)
        // -------------------------------

        public frmChoiGame(int idGame, string tenGame, int idGV)
        {
            InitializeComponent();
            _idGame = idGame;
            _tenGame = tenGame;
            _idGV = idGV;
            lblTenGame.Text = _tenGame.ToUpper();
        }

        private void frmChoiGame_Load(object sender, EventArgs e)
        {
            // ============================================================
            // --- CẤU HÌNH THANH KÉO (SCROLLBAR) ---
            // ============================================================

            // Cấu hình cho khung chứa câu trả lời (Phần màu trắng ở trên)
            flpTarget.AutoScroll = true;       // Tự động hiện thanh kéo khi nội dung tràn
            flpTarget.WrapContents = true;     // Tự động xuống dòng khi hết chiều ngang

            // Cấu hình cho khung chứa các từ (Phần bàn phím ở dưới)
            flpSource.AutoScroll = true;       // Tự động hiện thanh kéo khi nội dung tràn
            flpSource.WrapContents = true;     // Tự động xuống dòng
            // ============================================================

            // --- BẬT NHẠC NỀN ---
            ChoiNhacNen();
            // --------------------

            LoadDuLieuGame();
        }

        // --- CÁC HÀM XỬ LÝ ÂM THANH ---
        private void ChoiNhacNen()
        {
            try
            {
                // Lấy đường dẫn file tạm cho nhạc nền
                string path = GetTempFilePath(GUI.Properties.Resources.nhacnen, "nhacnen.mp3");
                _nhacNenPlayer.URL = path;
                _nhacNenPlayer.settings.setMode("loop", true); // Lặp lại nhạc nền
                _nhacNenPlayer.settings.volume = 80; // Âm lượng vừa phải (0-100)
                _nhacNenPlayer.controls.play();
            }
            catch { /* Bỏ qua lỗi nếu không load được nhạc */ }
        }

        private void ChoiAmThanh(UnmanagedMemoryStream resource, string fileName)
        {
            try
            {
                string path = GetTempFilePath(resource, fileName);
                _sfxPlayer.URL = path;
                _sfxPlayer.settings.volume = 70; // Âm lượng tối đa cho hiệu ứng
                _sfxPlayer.controls.play();
            }
            catch { }
        }

        // Hàm hỗ trợ ghi file từ Resource ra thư mục Temp để WMP chơi được
        private string GetTempFilePath(UnmanagedMemoryStream resource, string fileName)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), fileName);

            // Xử lý cập nhật file: Nếu file đã tồn tại, thử xóa nó đi để ghi file mới
            if (File.Exists(tempPath))
            {
                try
                {
                    File.Delete(tempPath);
                }
                catch
                {
                    return tempPath;
                }
            }

            // Ghi file mới từ Resource ra
            using (Stream file = File.Create(tempPath))
            {
                resource.CopyTo(file);
            }

            return tempPath;
        }
        // -------------------------------

        private void LoadDuLieuGame()
        {
            try
            {
                _danhSachCauHoi = bus.LayChiTietGame(_idGame);
                if (_danhSachCauHoi != null && _danhSachCauHoi.Count > 0)
                {
                    LoadCauHoi(0);
                }
                else
                {
                    MessageBox.Show("Bài tập này chưa có câu hỏi nào!");
                    TroVeTrangTaoGame();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                TroVeTrangTaoGame();
            }
        }

        private void LoadCauHoi(int index)
        {
            // SuspendLayout giúp giao diện không bị nháy khi load lại nút
            flpTarget.SuspendLayout();
            flpSource.SuspendLayout();

            flpTarget.Controls.Clear();
            flpSource.Controls.Clear();
            lblThongBao.Text = "Hãy sắp xếp các từ bên dưới thành câu hoàn chỉnh";
            lblThongBao.ForeColor = Color.Gray;
            btnTiepTuc.Visible = false;
            btnKiemTra.Enabled = true;

            if (index >= _danhSachCauHoi.Count)
            {
                MessageBox.Show("Chúc mừng! Bạn đã hoàn thành xuất sắc bài tập này.", "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TroVeTrangTaoGame();
                return;
            }

            _cauHienTaiIndex = index;
            _cauDungHienTai = _danhSachCauHoi[index].Trim();
            lblCauSo.Text = $"Câu {index + 1} / {_danhSachCauHoi.Count}";

            string[] words = _cauDungHienTai.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Random rnd = new Random();
            string[] shuffledWords = words.OrderBy(x => rnd.Next()).ToArray();

            foreach (string w in shuffledWords)
            {
                Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
                btn.Text = w;
                btn.BorderRadius = 15;
                btn.BorderThickness = 1;
                btn.BorderColor = Color.FromArgb(22, 30, 92);
                btn.FillColor = Color.White;
                btn.ForeColor = Color.FromArgb(22, 30, 92);
                btn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                btn.AutoSize = true;
                btn.Padding = new Padding(5);
                btn.Cursor = Cursors.Hand;
                btn.HoverState.FillColor = Color.FromArgb(22, 30, 92);
                btn.HoverState.ForeColor = Color.White;

                btn.Click += BtnWord_Click;
                flpSource.Controls.Add(btn);
            }

            // ResumeLayout để vẽ lại giao diện sau khi thêm xong nút
            flpTarget.ResumeLayout();
            flpSource.ResumeLayout();
        }

        private void BtnWord_Click(object sender, EventArgs e)
        {
            // --- CHƠI TIẾNG CLICK ---
            ChoiAmThanh(GUI.Properties.Resources.click, "click.mp3");
            // ------------------------

            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;

            if (btn.Parent == flpSource)
            {
                flpSource.Controls.Remove(btn);
                flpTarget.Controls.Add(btn);
                btn.FillColor = Color.FromArgb(225, 230, 255);
                btn.BorderColor = Color.FromArgb(22, 30, 92);

                // Tự động cuộn xuống cuối khi thêm từ mới vào target
                flpTarget.ScrollControlIntoView(btn);
            }
            else
            {
                flpTarget.Controls.Remove(btn);
                flpSource.Controls.Add(btn);
                btn.FillColor = Color.White;
            }
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            string cauTraLoi = "";
            foreach (Control c in flpTarget.Controls)
            {
                cauTraLoi += c.Text + " ";
            }
            cauTraLoi = cauTraLoi.Trim();

            if (cauTraLoi.Equals(_cauDungHienTai, StringComparison.OrdinalIgnoreCase))
            {
                // --- CHƠI TIẾNG ĐÚNG ---
                ChoiAmThanh(GUI.Properties.Resources.dung, "dung.mp3");
                // -----------------------

                lblThongBao.Text = "CHÍNH XÁC!";
                lblThongBao.ForeColor = Color.FromArgb(0, 192, 0);

                btnTiepTuc.Visible = true;
                btnKiemTra.Enabled = false;

                foreach (Guna.UI2.WinForms.Guna2Button btn in flpTarget.Controls)
                {
                    btn.FillColor = Color.FromArgb(0, 192, 0);
                    btn.ForeColor = Color.White;
                    btn.BorderColor = Color.FromArgb(0, 192, 0);
                }
            }
            else
            {
                // --- CHƠI TIẾNG SAI ---
                ChoiAmThanh(GUI.Properties.Resources.sai, "sai.mp3");
                // ----------------------

                lblThongBao.Text = "SAI RỒI!!!!";
                lblThongBao.ForeColor = Color.Red;

                foreach (Guna.UI2.WinForms.Guna2Button btn in flpTarget.Controls)
                {
                    btn.BorderColor = Color.Red;
                }
            }
        }

        private void btnTiepTuc_Click(object sender, EventArgs e)
        {
            LoadCauHoi(_cauHienTaiIndex + 1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            TroVeTrangTaoGame();
        }

        private void TroVeTrangTaoGame()
        {
            // --- TẮT NHẠC NỀN KHI THOÁT ---
            if (_nhacNenPlayer != null) _nhacNenPlayer.controls.stop();
            // ------------------------------

            frmTaoGame f = new frmTaoGame(_idGV);
            f.Show();
            this.Close();
        }
    }
}