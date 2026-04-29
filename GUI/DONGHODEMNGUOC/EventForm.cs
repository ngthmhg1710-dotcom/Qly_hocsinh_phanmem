using System;
using System.Drawing;
using System.Windows.Forms;
using DTO;

namespace Calendar
{
    public partial class EventForm : Form
    {
        public Event NewEvent { get; private set; }
        private DateTime _eventDate; // Biến để lưu ngày của sự kiện

        public EventForm(DateTime startTime, DateTime endTime)
        {
            InitializeComponent();

            // Lưu lại ngày, chỉ lấy phần Date, bỏ qua giờ
            _eventDate = startTime.Date;

            // Thiết lập giá trị ban đầu cho các control chọn giờ
            dtpStartTime.Value = startTime;
            dtpEndTime.Value = endTime;

            // Hiển thị ngày được chọn trên label
            lblSelectedDate.Text = $"Sự kiện cho ngày: {_eventDate:dd/MM/yyyy}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Kết hợp lại ngày đã lưu với giờ được chọn từ control
            DateTime finalStartTime = _eventDate.Date + dtpStartTime.Value.TimeOfDay;
            DateTime finalEndTime = _eventDate.Date + dtpEndTime.Value.TimeOfDay;

            if (finalEndTime <= finalStartTime)
            {
                MessageBox.Show("Thời gian kết thúc phải sau thời gian bắt đầu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NewEvent = new Event
            {
                Title = string.IsNullOrWhiteSpace(txtTitle.Text) ? "(Không có tiêu đề)" : txtTitle.Text,
                Description = txtDescription.Text,
                StartTime = finalStartTime,
                EndTime = finalEndTime,
                // Chọn một màu ngẫu nhiên cho sự kiện
                EventColor = Color.FromArgb(0, 126, 249),
                TextColor = Color.White
            };
            if (chkRemindBefore.Checked)
            {
                // Nếu được tick, đặt thời gian nhắc nhở là TRƯỚC 5 phút so với giờ kết thúc
                NewEvent.ReminderTime = finalEndTime.AddMinutes(-5);
            }
            else
            {
                // Nếu không tick, đặt thời gian nhắc nhở là NGAY TẠI giờ kết thúc
                NewEvent.ReminderTime = finalEndTime;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Hàm tiện ích để chọn một màu ngẫu nhiên từ danh sách
        private Color GetRandomColor()
        {
            Random rand = new Random();
            Color[] colorPalette = new Color[]
            {
                Color.FromArgb(26, 188, 156), // Turquoise
                Color.FromArgb(46, 204, 113), // Emerald
                Color.FromArgb(52, 152, 219), // Peter River
                Color.FromArgb(155, 89, 182),  // Amethyst
                Color.FromArgb(241, 196, 15),  // Sun Flower
                Color.FromArgb(230, 126, 34),  // Carrot
                Color.FromArgb(231, 76, 60),   // Alizarin
                Color.FromArgb(52, 73, 94)    // Wet Asphalt
            };
            return colorPalette[rand.Next(colorPalette.Length)];
        }

        private void chkRemindBefore_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}