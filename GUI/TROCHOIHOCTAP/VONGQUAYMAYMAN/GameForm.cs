// File: GameForm.cs
using DTO; // Thêm tham chiếu DTO
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using WMPLib;
using System.Threading.Tasks;
using DTO.Game; // <--- THÊM DÒNG NÀY QUAN TRỌNG
using GUI.Properties;
namespace RANDOMSO
{
    public partial class GameForm : Form
    {
        // Các biến quản lý trạng thái game (Sửa Question -> CauHoiDTO)
        private List<CauHoiRandomDTO> allQuestions;
        private List<CauHoiRandomDTO> activeQuestions;
        private CauHoiRandomDTO currentQuestion;
        private int score = 0;

        // Các biến cho vòng quay
        private Bitmap originalWheelImage;
        private float currentAngle = 0.0f;
        private float rotationSpeed = 0.0f;
        private float deceleration = 0.2f;
        private Timer spinTimer;
        private Random random = new Random();
        private bool isSpinning = false;

        // Các biến âm thanh và hiệu ứng
        private WindowsMediaPlayer backgroundMusicPlayer;
        private WindowsMediaPlayer wheelSpinPlayer;
        private WindowsMediaPlayer correctAnswerPlayer;
        private WindowsMediaPlayer incorrectAnswerPlayer;
        private WindowsMediaPlayer questionRevealPlayer;
        private WindowsMediaPlayer applausePlayer;
        private List<string> tempAudioFiles = new List<string>();
        private Timer applauseTimer;

        // Constructor nhận List<CauHoiDTO>
        public GameForm(List<CauHoiRandomDTO> questions)
        {
            InitializeComponent();
            allQuestions = questions;
            activeQuestions = new List<CauHoiRandomDTO>(allQuestions);

            InitializeAudioPlayers();
            this.FormClosing += GameForm_FormClosing;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            spinTimer = new Timer { Interval = 20 };
            spinTimer.Tick += SpinTimer_Tick;

            applauseTimer = new Timer();
            applauseTimer.Interval = 1500;
            applauseTimer.Tick += ApplauseTimer_Tick;

            pnlCurrentQuestion.Visible = false;

            lblPointer.BringToFront();
            UpdateScoreDisplay();
            DrawWheel();

            if (backgroundMusicPlayer.URL != null)
            {
                backgroundMusicPlayer.controls.play();
            }
        }

        private void ApplauseTimer_Tick(object sender, EventArgs e)
        {
            applausePlayer.controls.stop();
            applauseTimer.Stop();
        }

        private void InitializeAudioPlayers()
        {
            backgroundMusicPlayer = new WindowsMediaPlayer();
            wheelSpinPlayer = new WindowsMediaPlayer();
            correctAnswerPlayer = new WindowsMediaPlayer();
            incorrectAnswerPlayer = new WindowsMediaPlayer();
            questionRevealPlayer = new WindowsMediaPlayer();
            applausePlayer = new WindowsMediaPlayer();

            try
            {
                backgroundMusicPlayer.URL = GetSoundUrlFromResource(GUI.Properties.Resources.background_music);
                wheelSpinPlayer.settings.autoStart = false;
                correctAnswerPlayer.settings.autoStart = false;
                incorrectAnswerPlayer.settings.autoStart = false;
                questionRevealPlayer.settings.autoStart = false;
                applausePlayer.settings.autoStart = false;
                backgroundMusicPlayer.settings.setMode("loop", true);
                backgroundMusicPlayer.settings.volume = 80;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải file âm thanh từ Resources. Vui lòng kiểm tra lại tên file.\n" + ex.Message, "Lỗi Âm Thanh");
            }
        }

        private string GetSoundUrlFromResource(Stream audioStream, string extension = ".mp3")
        {
            if (audioStream == null) throw new ArgumentNullException(nameof(audioStream), "Resource âm thanh không tồn tại hoặc tên bị sai.");

            byte[] audioBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                audioStream.CopyTo(ms);
                audioBytes = ms.ToArray();
            }

            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + extension);
            File.WriteAllBytes(tempFile, audioBytes);
            tempAudioFiles.Add(tempFile);
            return tempFile;
        }

        private void PlayEffectSound(WindowsMediaPlayer player, Stream audioStream)
        {
            try
            {
                if (player.playState == WMPPlayState.wmppsPlaying)
                {
                    player.controls.stop();
                }
                player.URL = GetSoundUrlFromResource(audioStream);
                player.controls.play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi phát âm thanh hiệu ứng: " + ex.Message);
            }
        }

        private void UpdateScoreDisplay()
        {
            lblScore.Text = $"Điểm: {score}";
        }

        private void btnSpin_Click(object sender, EventArgs e)
        {
            if (activeQuestions.Count == 1)
            {
                currentQuestion = activeQuestions[0];
                DisplayQuestion(currentQuestion);
                btnSpin.Enabled = false;
            }
            else if (!isSpinning && activeQuestions.Any())
            {
                backgroundMusicPlayer.settings.volume = 70;

                try
                {
                    wheelSpinPlayer.URL = GetSoundUrlFromResource(GUI.Properties.Resources.wheel_spin);
                    wheelSpinPlayer.settings.setMode("loop", true);
                    wheelSpinPlayer.settings.volume = 20;
                    wheelSpinPlayer.controls.play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể phát âm thanh vòng quay: " + ex.Message);
                }

                rotationSpeed = random.Next(45, 65);
                isSpinning = true;
                btnSpin.Enabled = false;
                spinTimer.Start();
            }
        }

        private void SpinTimer_Tick(object sender, EventArgs e)
        {
            currentAngle += rotationSpeed;
            rotationSpeed -= deceleration;

            if (rotationSpeed <= 0)
            {
                rotationSpeed = 0;
                spinTimer.Stop();
                isSpinning = false;
                wheelSpinPlayer.controls.stop();
                wheelSpinPlayer.close();
                backgroundMusicPlayer.settings.volume = 70;
                DetermineWinner();
            }
            pbWheel.Image = RotateImage(originalWheelImage, currentAngle);
        }

        private void DetermineWinner()
        {
            float finalAngle = currentAngle % 360;
            int numSections = activeQuestions.Count;
            if (numSections == 0) return;

            float sectionAngle = 360.0f / numSections;
            float pointerAngle = 270;
            float winningAngle = (360 - finalAngle + pointerAngle) % 360;
            int winningIndex = (int)Math.Floor(winningAngle / sectionAngle);
            winningIndex = Math.Max(0, Math.Min(numSections - 1, winningIndex));

            currentQuestion = activeQuestions[winningIndex];
            DisplayQuestion(currentQuestion);
        }

        private void DisplayQuestion(CauHoiRandomDTO q)
        {
            // Hiển thị panel ngay lập tức, không có hiệu ứng
            pnlCurrentQuestion.Visible = true;

            PlayEffectSound(questionRevealPlayer, GUI.Properties.Resources.question_reveal);

            btnSpin.Enabled = false;
            lblCurrentQuestionTitle.Text = $"Câu hỏi số {q.QuestionNumber}";

            lblQuestionDisplay.Text = q.QuestionText;
            AdjustFontSize(lblQuestionDisplay);

            picCurrentQuestionImage.ImageLocation = q.ImagePath;

            bool hasText = !string.IsNullOrWhiteSpace(q.QuestionText);
            bool hasImage = !string.IsNullOrEmpty(q.ImagePath);

            if (hasText && hasImage)
            {
                tblQuestionLayout.RowStyles[0].Height = 50;
                tblQuestionLayout.RowStyles[1].Height = 50;
            }
            else if (hasText)
            {
                tblQuestionLayout.RowStyles[0].Height = 0;
                tblQuestionLayout.RowStyles[1].Height = 100;
            }
            else if (hasImage)
            {
                tblQuestionLayout.RowStyles[0].Height = 100;
                tblQuestionLayout.RowStyles[1].Height = 0;
            }
        }

        private void AdjustFontSize(Label label)
        {
            float maxFontSize = 28.2F;
            label.Font = new Font(label.Font.FontFamily, maxFontSize, FontStyle.Bold);

            Size textSize = TextRenderer.MeasureText(label.Text, label.Font, label.ClientSize, TextFormatFlags.WordBreak);

            while ((textSize.Width > label.ClientSize.Width || textSize.Height > label.ClientSize.Height) && label.Font.Size > 8)
            {
                label.Font = new Font(label.Font.FontFamily, label.Font.Size - 1, FontStyle.Bold);
                textSize = TextRenderer.MeasureText(label.Text, label.Font, label.ClientSize, TextFormatFlags.WordBreak);
            }
        }

        private void btnCorrect_Click(object sender, EventArgs e)
        {
            HandleAnswer(true);
        }

        private void btnIncorrect_Click(object sender, EventArgs e)
        {
            HandleAnswer(false);
        }

        private void HandleAnswer(bool isCorrect)
        {
            if (isCorrect)
            {
                score += 10;
                UpdateScoreDisplay();
                lblScore.Refresh();

                PlayEffectSound(correctAnswerPlayer, GUI.Properties.Resources.correct_answer);
                PlayEffectSound(applausePlayer, GUI.Properties.Resources.applause);
                applauseTimer.Start();

                msgDialogGameOver.Show("Chính xác! Bạn được cộng 10 điểm.", "Tuyệt vời!");
            }
            else
            {
                PlayEffectSound(incorrectAnswerPlayer, GUI.Properties.Resources.incorrect_answer);
                msgDialogGameOver.Show("Không chính xác! Rất tiếc.", "Sai rồi!");
            }

            pnlCurrentQuestion.Visible = false;
            lblQuestionDisplay.Text = "";
            picCurrentQuestionImage.Image = null;
            activeQuestions.Remove(currentQuestion);
            DrawWheel();
        }

        private void DrawWheel()
        {
            if (!activeQuestions.Any())
            {
                pbWheel.Image = null;
                btnSpin.Enabled = false;
                ShowGameOverScreen();
                return;
            }

            originalWheelImage?.Dispose();

            if (activeQuestions.Count == 1)
            {
                btnSpin.Text = "Câu hỏi cuối";
            }
            else
            {
                btnSpin.Text = "Quay";
            }

            originalWheelImage = GenerateWheelImage(activeQuestions, pbWheel.Width);
            pbWheel.Image = originalWheelImage;
            btnSpin.Enabled = true;
        }

        #region Graphics Helper Functions
        private Bitmap GenerateWheelImage(List<CauHoiRandomDTO> items, int size)
        {
            Bitmap wheelImage = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(wheelImage))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                int numItems = items.Count;
                if (numItems == 0) return wheelImage;

                float sweepAngle = 360f / numItems;
                Color[] colors = { Color.MidnightBlue, Color.Maroon };
                Font textFont = new Font("Segoe UI", 12F, FontStyle.Bold);

                for (int i = 0; i < numItems; i++)
                {
                    Brush segmentBrush = new SolidBrush(colors[i % colors.Length]);
                    float startAngle = i * sweepAngle;
                    g.FillPie(segmentBrush, 0, 0, size - 1, size - 1, startAngle, sweepAngle);
                }

                if (numItems > 1)
                {
                    Pen borderPen = new Pen(Color.White, 2);
                    for (int i = 0; i < numItems; i++)
                    {
                        g.DrawPie(borderPen, 0, 0, size - 1, size - 1, i * sweepAngle, sweepAngle);
                    }
                }

                for (int i = 0; i < numItems; i++)
                {
                    string text = $"Câu {items[i].QuestionNumber}";
                    float startAngle = i * sweepAngle;
                    float textAngle = startAngle + sweepAngle / 2;
                    float radius = (size / 2) * 0.7f;
                    double angleRad = textAngle * Math.PI / 180.0;
                    float x = (size / 2) + (float)(radius * Math.Cos(angleRad));
                    float y = (size / 2) + (float)(radius * Math.Sin(angleRad));

                    var state = g.Save();
                    g.TranslateTransform(x, y);
                    g.RotateTransform(textAngle > 90 && textAngle < 270 ? textAngle - 180 : textAngle);
                    StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(text, textFont, Brushes.White, 0, 0, format);
                    g.Restore(state);
                }
            }
            return wheelImage;
        }

        private Bitmap RotateImage(Image image, float angle)
        {
            if (image == null) return null;
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(rotatedBmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.TranslateTransform(image.Width / 2, image.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-image.Width / 2, -image.Height / 2);
                g.DrawImage(image, new Point(0, 0));
            }
            return rotatedBmp;
        }
        #endregion

        #region GameOver Logic
        private void ShowGameOverScreen()
        {
            msgDialogGameOver.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
            msgDialogGameOver.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
            msgDialogGameOver.Style = Guna.UI2.WinForms.MessageDialogStyle.Light;
            msgDialogGameOver.Caption = "Kết thúc game";
            string message = $"Chúc mừng! Bạn đã hoàn thành game với số điểm: {score}\n\nBạn có muốn chơi lại không?";

            DialogResult result = msgDialogGameOver.Show(message, msgDialogGameOver.Caption);

            if (result == DialogResult.Yes)
            {
                score = 0;
                activeQuestions = new List<CauHoiRandomDTO>(allQuestions);
                UpdateScoreDisplay();
                DrawWheel();
            }
            else
            {
                CleanupAndCloseAsync();
            }
        }

        private async void CleanupAndCloseAsync()
        {
            await Task.Run(() =>
            {
                applauseTimer?.Stop();
                backgroundMusicPlayer?.controls.stop();
                wheelSpinPlayer?.controls.stop();
                backgroundMusicPlayer?.close();
                wheelSpinPlayer?.close();
                correctAnswerPlayer?.close();
                incorrectAnswerPlayer?.close();
                questionRevealPlayer?.close();
                applausePlayer?.close();

                foreach (var filePath in tempAudioFiles)
                {
                    try
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                    catch { }
                }
            });

            this.Close();
        }

        #endregion

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 1. Dừng các bộ đếm thời gian
            applauseTimer?.Stop();
            spinTimer?.Stop();

            // 2. Dừng âm thanh ngay lập tức
            try
            {
                backgroundMusicPlayer?.controls.stop();
                wheelSpinPlayer?.controls.stop();
                correctAnswerPlayer?.controls.stop();
                incorrectAnswerPlayer?.controls.stop();
                questionRevealPlayer?.controls.stop();
                applausePlayer?.controls.stop();
            }
            catch { }

            // XÓA BỎ đoạn code tìm Form cha ở đây.
            // Việc hiện lại Form cha sẽ do Form cha tự xử lý.
        }


        private void lblScore_Click(object sender, EventArgs e) { }
        private void lblScore_Click_1(object sender, EventArgs e) { }
        private void lblScore_Click_2(object sender, EventArgs e) { }
    }
}