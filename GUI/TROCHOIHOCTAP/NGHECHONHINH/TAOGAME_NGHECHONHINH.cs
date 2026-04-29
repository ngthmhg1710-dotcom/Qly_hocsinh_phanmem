// File: TAOGAME_NGHECHONHINH.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms; // Thư viện giao diện
using WMPLib;              // Thư viện âm thanh Windows Media Player
using DTO;                 // Tham chiếu Project DTO
using GUI.Properties;

namespace GAME1_
{
    public partial class TAOGAME_NGHECHONHINH : Form
    {
        // --- KHAI BÁO BIẾN ---

        // Sử dụng DTO thay vì class cũ
        private List<CauHoiHinhAnhDTO> danhSachCauHoi;

        private int cauHoiHienTaiIndex = 0;
        private int diemSo = 0;

        // Các trình phát âm thanh
        private WindowsMediaPlayer amThanhPlayer;      // Phát câu hỏi
        private WindowsMediaPlayer amThanhDungPlayer;  // Phát hiệu ứng đúng
        private WindowsMediaPlayer amThanhSaiPlayer;   // Phát hiệu ứng sai
        private WindowsMediaPlayer amThanhNenPlayer;   // Phát nhạc nền

        // Các control giao diện
        private Guna2PictureBox[] picLuaChonBoxes;
        private Guna2Panel[] panelLuaChonContainers;

        private bool isGameOver = false;
        private List<string> tempAudioFiles = new List<string>(); // Quản lý file tạm để xóa sau khi tắt
        private int initialBackgroundVolume = 50;

        // --- CONSTRUCTOR ---

        // Cập nhật tham số đầu vào là List<CauHoiDTO>
        public TAOGAME_NGHECHONHINH(List<CauHoiHinhAnhDTO> dsCauHoi)
        {
            InitializeComponent();
            this.danhSachCauHoi = dsCauHoi;

            // Đăng ký sự kiện đóng form
            this.FormClosing += TAOGAME_NGHECHONHINH_FormClosing;

            // Khởi tạo các Media Player
            amThanhPlayer = new WindowsMediaPlayer { settings = { autoStart = false } };
            amThanhDungPlayer = new WindowsMediaPlayer { settings = { autoStart = false } };
            amThanhSaiPlayer = new WindowsMediaPlayer { settings = { autoStart = false } };
            amThanhNenPlayer = new WindowsMediaPlayer();

            // Sự kiện để lặp lại nhạc nền nếu cần (xử lý thủ công nếu setMode loop không nhạy)
            amThanhPlayer.PlayStateChange += AmThanhPlayer_PlayStateChange;

            // Map các control vào mảng để dễ xử lý vòng lặp
            picLuaChonBoxes = new Guna2PictureBox[] { picLuaChon1, picLuaChon2, picLuaChon3 };
            panelLuaChonContainers = new Guna2Panel[] { panelLuaChon1, panelLuaChon2, panelLuaChon3 };
        }

        // --- SỰ KIỆN FORM LOAD ---

        private void Form2_Load(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (danhSachCauHoi == null || danhSachCauHoi.Count == 0)
            {
                MessageBox.Show("Không có câu hỏi nào để bắt đầu game.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            KhoiTaoVaPhatNhacNen();
            HienThiCauHoi(cauHoiHienTaiIndex);
        }

        // --- CÁC HÀM XỬ LÝ ÂM THANH ---

        private void PlaySoundFromResource(WindowsMediaPlayer player, Stream audioStream, string extension = ".mp3")
        {
            if (audioStream == null)
            {
                // Nếu không tìm thấy resource, bỏ qua để tránh crash
                return;
            }

            try
            {
                // Chuyển Stream từ Resource thành file tạm trên ổ cứng để WMP có thể đọc
                byte[] audioBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    audioStream.CopyTo(ms);
                    audioBytes = ms.ToArray();
                }

                string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + extension);
                File.WriteAllBytes(tempFile, audioBytes);

                // Lưu đường dẫn để xóa sau này
                tempAudioFiles.Add(tempFile);

                player.URL = tempFile;
                player.controls.play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi phát âm thanh từ resource: " + ex.Message);
            }
        }

        private void KhoiTaoVaPhatNhacNen()
        {
            try
            {
                Stream audioStream = GUI.Properties.Resources.background_music;

                if (audioStream == null) return;

                byte[] audioBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    audioStream.CopyTo(ms);
                    audioBytes = ms.ToArray();
                }

                string tempFile = Path.Combine(Path.GetTempPath(), "background_music_temp.mp3");
                File.WriteAllBytes(tempFile, audioBytes);
                tempAudioFiles.Add(tempFile);

                amThanhNenPlayer.URL = tempFile;
                amThanhNenPlayer.settings.setMode("loop", true);
                amThanhNenPlayer.settings.volume = initialBackgroundVolume;
                amThanhNenPlayer.controls.play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo nhạc nền: " + ex.Message);
            }
        }

        // Xử lý logic ưu tiên âm thanh: Khi tiếng câu hỏi kết thúc, nhạc nền tiếp tục
        private void AmThanhPlayer_PlayStateChange(int NewState)
        {
            if ((WMPPlayState)NewState == WMPPlayState.wmppsMediaEnded || (WMPPlayState)NewState == WMPPlayState.wmppsStopped)
            {
                if (amThanhNenPlayer.playState == WMPPlayState.wmppsPaused)
                {
                    amThanhNenPlayer.controls.play();
                }
            }
        }

        // --- LOGIC GAME ---

        private void HienThiCauHoi(int index)
        {
            // Nếu hết câu hỏi -> Kết thúc
            if (index >= danhSachCauHoi.Count)
            {
                KetThucGame();
                return;
            }

            // Tiếp tục phát nhạc nền nếu đang tạm dừng
            if (amThanhNenPlayer.playState == WMPPlayState.wmppsPaused)
            {
                amThanhNenPlayer.controls.play();
            }

            // Lấy dữ liệu câu hỏi từ DTO
            CauHoiHinhAnhDTO cauHoi = danhSachCauHoi[index];

            // Reset trạng thái giao diện (viền, enable)
            for (int i = 0; i < panelLuaChonContainers.Length; i++)
            {
                panelLuaChonContainers[i].BorderThickness = 0;
                picLuaChonBoxes[i].Enabled = true;
            }

            try
            {
                // Load hình ảnh từ đường dẫn trong DTO
                picLuaChon1.Image = Image.FromFile(cauHoi.Anh[0]);
                picLuaChon2.Image = Image.FromFile(cauHoi.Anh[1]);
                picLuaChon3.Image = Image.FromFile(cauHoi.Anh[2]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hình ảnh (File có thể đã bị xóa hoặc di chuyển): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblDiem.Text = diemSo.ToString();
            lblCauHoi.Text = $"{index + 1}/{danhSachCauHoi.Count}";

            // Phát âm thanh câu hỏi
            amThanhPlayer.URL = cauHoi.AmThanh;
            amThanhPlayer.controls.play();
        }

        private async void picLuaChon_Click(object sender, EventArgs e)
        {
            if (isGameOver) return;

            // Khóa các lựa chọn để tránh spam click
            foreach (var pic in picLuaChonBoxes)
            {
                pic.Enabled = false;
            }

            // Xác định lựa chọn
            Guna2PictureBox picDaChon = sender as Guna2PictureBox;
            Guna2Panel panelDaChon = picDaChon.Parent as Guna2Panel;

            int luaChonCuaNguoiDung = Convert.ToInt32(picDaChon.Tag); // Tag được set 0, 1, 2 trong Designer
            int dapAnDung = danhSachCauHoi[cauHoiHienTaiIndex].DapAn;

            Guna2Panel panelDapAnDung = panelLuaChonContainers[dapAnDung];
            int borderSize = 6;

            // Kiểm tra đúng sai
            if (luaChonCuaNguoiDung == dapAnDung)
            {
                PlaySoundFromResource(amThanhDungPlayer, GUI.Properties.Resources.correct_sound);
                diemSo += 10;
                lblDiem.Text = diemSo.ToString();

                // Hiệu ứng đúng: Viền xanh
                panelDaChon.BorderThickness = borderSize;
                panelDaChon.BorderColor = Color.LawnGreen;
            }
            else
            {
                PlaySoundFromResource(amThanhSaiPlayer, GUI.Properties.Resources.wrong_sound);

                // Hiệu ứng sai: Viền đỏ cho cái chọn sai, Viền xanh cho cái đúng
                panelDaChon.BorderThickness = borderSize;
                panelDaChon.BorderColor = Color.Tomato;

                panelDapAnDung.BorderThickness = borderSize;
                panelDapAnDung.BorderColor = Color.LawnGreen;
            }

            // Chờ 1.5 giây trước khi qua câu tiếp theo
            await Task.Delay(1500);

            cauHoiHienTaiIndex++;
            HienThiCauHoi(cauHoiHienTaiIndex);
        }

        private void KetThucGame()
        {
            isGameOver = true;
            try { amThanhNenPlayer.controls.stop(); } catch { }

            MessageBox.Show($"Trò chơi kết thúc!\nTổng điểm: {diemSo}", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close(); // Đóng Form -> Kích hoạt sự kiện FormClosed -> Form Tạo sẽ hiện lại
        }

        // --- DỌN DẸP TÀI NGUYÊN ---

        private async void TAOGAME_NGHECHONHINH_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. Dừng và giải phóng Media Player NGAY LẬP TỨC
            try
            {
                if (amThanhPlayer != null) amThanhPlayer.PlayStateChange -= AmThanhPlayer_PlayStateChange;

                amThanhNenPlayer?.controls.stop();
                amThanhPlayer?.controls.stop();
                amThanhDungPlayer?.controls.stop();
                amThanhSaiPlayer?.controls.stop();

                amThanhNenPlayer?.close();
                amThanhPlayer?.close();
                amThanhDungPlayer?.close();
                amThanhSaiPlayer?.close();
            }
            catch { }

            // 2. Xóa file tạm trong Background Task (để không làm đơ Form khi đóng)
            // Copy danh sách file để tránh xung đột
            var filesToDelete = new List<string>(tempAudioFiles);

            // Chạy task ngầm để xóa file sau 1 khoảng thời gian ngắn (đảm bảo WMP đã nhả file ra)
            await Task.Run(async () =>
            {
                await Task.Delay(1000); // Đợi 1s cho chắc ăn
                foreach (var filePath in filesToDelete)
                {
                    try
                    {
                        if (File.Exists(filePath)) File.Delete(filePath);
                    }
                    catch { }
                }
            });
        }

        // Nút loa để nghe lại câu hỏi
        private void picSound_Click(object sender, EventArgs e)
        {
            if (isGameOver) return;

            // Dừng âm thanh đang phát để ưu tiên tiếng câu hỏi
            if (amThanhPlayer.playState == WMPPlayState.wmppsPlaying)
            {
                amThanhPlayer.controls.stop();
            }

            if (amThanhNenPlayer.playState == WMPPlayState.wmppsPlaying)
            {
                amThanhNenPlayer.controls.pause();
            }

            amThanhPlayer.controls.play();
        }

        private void lblCauHoi_Click(object sender, EventArgs e)
        {
            // Placeholder
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}