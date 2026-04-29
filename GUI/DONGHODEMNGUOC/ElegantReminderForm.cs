using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Threading; // Thêm thư viện để dùng Thread
using System.Windows.Forms;
using DTO;

namespace Calendar
{
    public partial class ElegantReminderForm : Form
    {
        private static List<ElegantReminderForm> openForms = new List<ElegantReminderForm>();
        private SoundPlayer _loopingPlayer; // Biến để điều khiển âm thanh

        public ElegantReminderForm(Event eventToRemind)
        {
            InitializeComponent();

            lblTitle.Text = eventToRemind.Title;

            // Logic để quyết định nội dung thông báo
            // HasValue là để chắc chắn ReminderTime không bị null
            if (eventToRemind.ReminderTime.HasValue && eventToRemind.ReminderTime < eventToRemind.EndTime)
            {
                // Trường hợp 1: Đây là thông báo "nhắc trước"
                // (Vì thời gian nhắc nhở nhỏ hơn thời gian kết thúc)
                lblDetails.Text = $"Sắp kết thúc lúc: {eventToRemind.EndTime:HH:mm}";

                // BONUS: Đổi màu nền để dễ nhận biết
                this.BackColor = Color.FromArgb(255, 152, 0); // Màu vàng cam để cảnh báo
            }
            else
            {
                // Trường hợp 2: Đây là thông báo "ngay lúc kết thúc"
                // (Bao gồm cả hẹn giờ nhanh và các sự kiện không tick "nhắc trước")
                lblDetails.Text = $"Vừa kết thúc lúc: {eventToRemind.EndTime:HH:mm} ngày {eventToRemind.EndTime:dd/MM/yyyy}";

                // Giữ màu nền mặc định (bạn có thể đổi màu ở đây nếu muốn)
                // this.BackColor = Color.FromArgb(70, 70, 82); 
            }
        }

        private void ElegantReminderForm_Load(object sender, EventArgs e)
        {
            PositionForm();
            openForms.Add(this);
            StartLoopingSound(); // Gọi hàm bắt đầu phát âm thanh lặp lại
        }

        private void StartLoopingSound()
        {
            try
            {
                // --- SỬA ĐỔI QUAN TRỌNG ---
                // Sử dụng lại cách lấy resource đúng mà bạn đã dùng (Properties.Resources)
                _loopingPlayer = new SoundPlayer(Application.StartupPath + @"\Sounds\notify.wav");

                // Tạo một luồng mới để phát âm thanh, tránh làm đơ giao diện
                Thread soundThread = new Thread(() =>
                {
                    _loopingPlayer.PlayLooping();
                });

                soundThread.IsBackground = true; 
                soundThread.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi phát âm thanh: " + ex.Message);
            }
        }

        private void PositionForm()
        {
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Left = workingArea.Right - this.Width - 15;
            int topPosition = workingArea.Bottom - this.Height - 15;
            foreach (var form in openForms.Where(f => f != this))
            {
                topPosition -= (form.Height + 10);
            }
            this.Top = topPosition;
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            // Dừng âm thanh trước khi đóng form
            _loopingPlayer?.Stop(); 
            this.Close();
        }

        private void ElegantReminderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Đảm bảo dừng và giải phóng tài nguyên âm thanh khi form đóng
            _loopingPlayer?.Stop();
            _loopingPlayer?.Dispose();

            openForms.Remove(this);
            RepositionOpenForms();
        }

        private static void RepositionOpenForms()
        {
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            int topPosition = workingArea.Bottom - 15;
            foreach (var form in openForms.AsEnumerable().Reverse())
            {
                topPosition -= (form.Height + 10);
                form.Top = topPosition;
            }
        }
    }
}