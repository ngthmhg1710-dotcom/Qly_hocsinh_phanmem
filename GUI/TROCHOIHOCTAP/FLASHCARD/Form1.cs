using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks; // Để xử lý xóa file ngầm
using System.Windows.Forms;
using WMPLib; // Thư viện Windows Media Player
using DTO; // <--- QUAN TRỌNG: Gọi lớp DTO để nhận diện Flashcard
using GUI.Properties;

namespace FlashcardFlipGame
{
    public partial class Form1 : Form
    {
        private List<Flashcard> cards;
        private int currentIndex = 0;

        // --- Các biến quản lý âm thanh ---
        private WindowsMediaPlayer backgroundMusicPlayer;
        private WindowsMediaPlayer flipSoundPlayer;

        // Lưu đường dẫn file để dùng đi dùng lại, không tạo mới liên tục
        private string bgMusicPath = null;
        private string flipSoundPath = null;

        private bool isMusicMuted = false;
        private bool isSfxMuted = false;
        // ------------------------------------

        // Constructor nhận vào danh sách thẻ từ DTO
        public Form1(List<Flashcard> deck)
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.White;

            // Kiểm tra dữ liệu đầu vào
            if (deck == null || deck.Count == 0)
            {
                MessageBox.Show("Không có thẻ nào để học.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Đóng form an toàn khi chưa load xong
                this.Load += (s, e) => Close();
                return;
            }

            this.cards = deck;

            // Khởi tạo âm thanh và đăng ký sự kiện
            InitializeAudio();

            // Đăng ký sự kiện lật thẻ từ UserControl
            flashcardControl1.CardFlipped += FlashcardControl1_CardFlipped;

            // Đăng ký sự kiện đóng form để dọn dẹp
            this.FormClosing += Form1_FormClosing;

            // Hiển thị thẻ đầu tiên
            UpdateCardDisplay();
        }

        #region Audio Management (Quản lý âm thanh)

        private void InitializeAudio()
        {
            try
            {
                // 1. Giải nén file ra ổ cứng MỘT LẦN DUY NHẤT tại đây
                // Lưu ý: Đảm bảo bạn đã thêm file vào Resources của project
                bgMusicPath = ExtractResourceToTempFile(GUI.Properties.Resources.background_music2);
                flipSoundPath = ExtractResourceToTempFile(GUI.Properties.Resources.flip_sound);

                // 2. Cấu hình Nhạc nền
                backgroundMusicPlayer = new WindowsMediaPlayer();
                backgroundMusicPlayer.settings.autoStart = false;
                backgroundMusicPlayer.URL = bgMusicPath;
                backgroundMusicPlayer.settings.setMode("loop", true);
                backgroundMusicPlayer.settings.volume = 50;
                backgroundMusicPlayer.controls.play();

                // 3. Cấu hình Âm thanh lật (nhưng chưa phát)
                flipSoundPlayer = new WindowsMediaPlayer();
                flipSoundPlayer.settings.autoStart = false;
                // Load sẵn URL để đỡ bị delay khi lật lần đầu
                flipSoundPlayer.URL = flipSoundPath;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi ra Console thay vì hiện Popup để tránh làm phiền người dùng
                Console.WriteLine("Lỗi khởi tạo âm thanh: " + ex.Message);
            }
        }

        private void FlashcardControl1_CardFlipped(object sender, EventArgs e)
        {
            if (!isSfxMuted && flipSoundPlayer != null && !string.IsNullOrEmpty(flipSoundPath))
            {
                try
                {
                    // Stop trước để rewind về đầu, sau đó Play ngay
                    flipSoundPlayer.controls.stop();
                    flipSoundPlayer.controls.play();
                }
                catch { }
            }
        }

        // Hàm trích xuất file từ Resources ra thư mục Temp
        private string ExtractResourceToTempFile(System.IO.Stream audioStream, string extension = ".mp3")
        {
            if (audioStream == null) return null;

            try
            {
                byte[] audioBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    audioStream.CopyTo(ms);
                    audioBytes = ms.ToArray();
                }

                // Tạo tên file ngẫu nhiên để tránh trùng lặp
                string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + extension);
                File.WriteAllBytes(tempFile, audioBytes);
                return tempFile;
            }
            catch
            {
                return null;
            }
        }

        private void picMusicToggle_Click(object sender, EventArgs e)
        {
            isMusicMuted = !isMusicMuted;
            if (backgroundMusicPlayer != null)
            {
                backgroundMusicPlayer.settings.mute = isMusicMuted;
            }
            // Đổi icon nút bấm (Cần có ảnh trong Resources)
            picMusicToggle.Image = isMusicMuted ? GUI.Properties.Resources.music_off : GUI.Properties.Resources.music_on;
        }

        private void picSfxToggle_Click(object sender, EventArgs e)
        {
            isSfxMuted = !isSfxMuted;
            // Chỉ cần bật/tắt cờ hiệu, không cần mute player
            picSfxToggle.Image = isSfxMuted ? GUI.Properties.Resources.sfx_off : GUI.Properties.Resources.sfx_on;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. Dừng nhạc ngay lập tức
            if (backgroundMusicPlayer != null)
            {
                try { backgroundMusicPlayer.controls.stop(); backgroundMusicPlayer.close(); } catch { }
            }
            if (flipSoundPlayer != null)
            {
                try { flipSoundPlayer.controls.stop(); flipSoundPlayer.close(); } catch { }
            }

            // 2. Chuyển việc xóa file sang luồng phụ (Background Task)
            string file1 = bgMusicPath;
            string file2 = flipSoundPath;

            Task.Run(() =>
            {
                // Chờ nhẹ 1 chút để WMP nhả file hoàn toàn
                System.Threading.Thread.Sleep(100);

                if (!string.IsNullOrEmpty(file1)) DeleteFileSafe(file1);
                if (!string.IsNullOrEmpty(file2)) DeleteFileSafe(file2);
            });
        }

        private void DeleteFileSafe(string filePath)
        {
            try
            {
                if (File.Exists(filePath)) File.Delete(filePath);
            }
            catch
            {
                // Bỏ qua lỗi nếu file đang bị khóa
            }
        }

        #endregion

        #region Game Logic (Next/Prev/Display)

        private void UpdateCardDisplay()
        {
            if (currentIndex >= 0 && currentIndex < cards.Count)
            {
                // Gọi hàm SetCard của UserControl để hiển thị
                flashcardControl1.SetCard(cards[currentIndex]);

                // Cập nhật số trang
                lblCardCount.Text = $"{currentIndex + 1} / {cards.Count}";

                // Ẩn/Hiện nút Previous
                btnPrevious.Enabled = (currentIndex > 0);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex >= cards.Count - 1)
            {
                // Đã học hết thẻ
                MessageBox.Show("Tuyệt vời! Bạn đã hoàn thành tất cả các thẻ!", "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                currentIndex++;
                UpdateCardDisplay();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateCardDisplay();
            }
        }

        private void flashcardControl1_Click(object sender, EventArgs e)
        {
            // Sự kiện click đã được xử lý bên trong FlashcardControl (hàm Flip)
            // Có thể để trống hoặc thêm logic phụ nếu cần
        }

        #endregion
    }
}