using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using DTO;


namespace Calendar
{
    public partial class EventDetailsForm : Form
    {
        private Event _currentEvent;
        private DateTime _eventDate;
        private readonly bool _isQuickAddMode;

        // --- Biến cho cuộn chuột mượt ---
        private Timer scrollTimer;
        private int targetScrollValue;
        private double currentScrollValue;
        private const double EasingFactor = 0.2;

        // --- THÊM MỚI: Định danh và thuộc tính để nhận biết Hẹn giờ nhanh ---
        private const string QuickAddIdentifier = "::QUICK_ADD_EVENT::";
        private bool IsQuickAddEvent => _currentEvent?.Description?.Contains(QuickAddIdentifier) ?? false;

        private enum FormState { View, Edit }

        public EventDetailsForm(Event evt, bool isQuickAddMode = false)
        {
            InitializeComponent();
            _currentEvent = evt;
            _eventDate = _currentEvent.StartTime.Date;
            _isQuickAddMode = isQuickAddMode;
            InitializeSmoothScrolling();
        }

        private void EventDetailsForm_Load(object sender, EventArgs e)
        {
            AttachMouseWheelHandler(this);

            // Logic cũ đã được chuyển vào trong SwitchState để xử lý linh hoạt hơn
            if (_isQuickAddMode)
            {
                SwitchState(FormState.Edit);
            }
            else
            {
                SwitchState(FormState.View);
            }
        }

        private void InitializeSmoothScrolling()
        {
            scrollTimer = new Timer();
            scrollTimer.Interval = 15;
            scrollTimer.Tick += ScrollTimer_Tick;
            targetScrollValue = 0;
            currentScrollValue = 0;
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            double distance = targetScrollValue - currentScrollValue;
            if (Math.Abs(distance) < 1)
            {
                currentScrollValue = targetScrollValue;
                flpViewContent.Top = -targetScrollValue;
                scrollTimer.Stop();
                return;
            }
            currentScrollValue += distance * EasingFactor;
            flpViewContent.Top = -(int)Math.Round(currentScrollValue);
        }

        private void AttachMouseWheelHandler(Control control)
        {
            control.MouseWheel += Global_MouseWheelHandler;
            foreach (Control child in control.Controls)
            {
                AttachMouseWheelHandler(child);
            }
        }

        private void Global_MouseWheelHandler(object sender, MouseEventArgs e)
        {
            if (!pnlView.Visible || !vsbView.Visible) return;
            int direction = e.Delta / 120;
            int scrollAmount = 40;
            int newValue = vsbView.Value - (direction * scrollAmount);
            vsbView.Value = Math.Max(vsbView.Minimum, Math.Min(vsbView.Maximum, newValue));
        }

        private void PopulateViewData()
        {
            lblTitle.Text = string.IsNullOrWhiteSpace(_currentEvent.Title) ? "(Không có tiêu đề)" : _currentEvent.Title;

            var vietnameseCulture = new CultureInfo("vi-VN");
            string dateString = _currentEvent.StartTime.ToString("dddd, d MMMM", vietnameseCulture);
            string timeString = $"{_currentEvent.StartTime:HH:mm} – {_currentEvent.EndTime:HH:mm}";
            lblDateTime.Text = $"{char.ToUpper(dateString[0])}{dateString.Substring(1)} • {timeString}";

            pnlColorIndicator.FillColor = _currentEvent.EventColor;

            // --- SỬA ĐỔI: Xóa chuỗi định danh để người dùng không thấy ---
            string displayDescription = _currentEvent.Description?.Replace(QuickAddIdentifier, "").Trim();
            lblDescription.Text = displayDescription;
            bool hasDescription = !string.IsNullOrWhiteSpace(displayDescription);
            lblDescription.Visible = hasDescription;
            lblDescriptionHeader.Visible = hasDescription;

            UpdateViewLayout();
        }

        private void UpdateViewLayout()
        {
            bool needsScrollbar = flpViewContent.Height > pnlView.Height;
            vsbView.Visible = needsScrollbar;
            if (needsScrollbar)
            {
                vsbView.Maximum = flpViewContent.Height - pnlView.Height;
            }
            vsbView.Value = 0;
            targetScrollValue = 0;
            currentScrollValue = 0;
            flpViewContent.Top = 0;
        }

        private void PopulateEditData()
        {
            txtTitleEdit.Text = _currentEvent.Title;
            dtpStartTime.Value = _currentEvent.StartTime;
            dtpEndTime.Value = _currentEvent.EndTime;
            btnColorPicker.FillColor = _currentEvent.EventColor;
            UpdateColorPickerTextContrast(btnColorPicker, _currentEvent.EventColor);

            // --- SỬA ĐỔI: Xóa chuỗi định danh để người dùng không thấy ---
            txtDescriptionEdit.Text = _currentEvent.Description?.Replace(QuickAddIdentifier, "").Trim();

            chkRemindBeforeEdit.Checked = _currentEvent.ReminderTime.HasValue &&
                                          _currentEvent.ReminderTime < _currentEvent.EndTime;
        }

        private void SwitchState(FormState newState)
        {
            this.ClientSize = new Size(533, 450);

            if (newState == FormState.View)
            {
                pnlView.Visible = true;
                pnlEdit.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                PopulateViewData();
            }
            else // Chế độ Sửa
            {
                pnlView.Visible = false;
                pnlEdit.Visible = true;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                PopulateEditData();
                txtTitleEdit.Focus();
                txtTitleEdit.SelectAll();

                // --- SỬA ĐỔI: Logic ẩn checkbox ---
                // Bây giờ IsQuickAddEvent đã tồn tại và sẽ hoạt động đúng
                if (IsQuickAddEvent)
                {
                    chkRemindBeforeEdit.Visible = false;
                }
                else
                {
                    chkRemindBeforeEdit.Visible = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime finalStartTime = _eventDate.Date + dtpStartTime.Value.TimeOfDay;
            DateTime finalEndTime = _eventDate.Date + dtpEndTime.Value.TimeOfDay;

            if (finalEndTime <= finalStartTime)
            {
                MessageBox.Show("Thời gian kết thúc phải sau thời gian bắt đầu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _currentEvent.Title = txtTitleEdit.Text;
            _currentEvent.StartTime = finalStartTime;
            _currentEvent.EndTime = finalEndTime;
            _currentEvent.EventColor = btnColorPicker.FillColor;
            _currentEvent.TextColor = btnColorPicker.ForeColor;

            // --- SỬA ĐỔI: Logic mô tả và nhắc nhở được đảm bảo an toàn ---
            string finalDescription = txtDescriptionEdit.Text;
            if (IsQuickAddEvent)
            {
                // Nếu là Hẹn giờ nhanh, luôn đảm bảo chuỗi định danh tồn tại
                if (!finalDescription.Contains(QuickAddIdentifier))
                {
                    finalDescription += QuickAddIdentifier;
                }
                // VÀ quan trọng nhất: Ép nhắc nhở phải đúng vào lúc kết thúc
                _currentEvent.ReminderTime = finalEndTime;
            }
            else
            {
                // Nếu là sự kiện thường, xử lý nhắc nhở theo checkbox
                if (chkRemindBeforeEdit.Checked)
                {
                    _currentEvent.ReminderTime = finalEndTime.AddMinutes(-5);
                }
                else
                {
                    _currentEvent.ReminderTime = finalEndTime;
                }
            }
            _currentEvent.Description = finalDescription;
            // --- KẾT THÚC SỬA ĐỔI ---

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = btnColorPicker.FillColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    btnColorPicker.FillColor = colorDialog.Color;
                    UpdateColorPickerTextContrast(btnColorPicker, colorDialog.Color);
                }
            }
        }

        private void UpdateColorPickerTextContrast(Guna2Button button, Color backColor)
        {
            float brightness = backColor.GetBrightness();
            button.ForeColor = (brightness > 0.65f) ? Color.Black : Color.White;
        }

        private void btnEdit_Click(object sender, EventArgs e) => SwitchState(FormState.Edit);

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Sửa đổi nhẹ: nếu là Hẹn giờ nhanh, nút Cancel luôn đóng form
            // Nếu là sự kiện thường, nút Cancel quay về chế độ xem
            if (_isQuickAddMode || IsQuickAddEvent)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                SwitchState(FormState.View);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void vsbView_Scroll(object sender, ScrollEventArgs e)
        {
            targetScrollValue = e.NewValue;
            scrollTimer.Start();
        }
    }
}