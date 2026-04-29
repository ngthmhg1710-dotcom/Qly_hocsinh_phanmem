using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using BUS; // Thêm tham chiếu đến project BUS
using DTO; // Thêm tham chiếu đến project DTO

// Namespace này phải khớp với tên project Giao diện của bạn
namespace Calendar
{
    public partial class MainForm : Form
    {
        // --- Hằng số để vẽ ---
        private const int HourHeight = 60;
        private const int DayHeaderHeight = 60;
        private const int TimeColumnWidth = 60;
        private const int HeaderGridPadding = 3;


        // =========================================================================
        // !!! Biến này được giữ lại theo yêu cầu !!!
        // =========================================================================
        // --- Lớp logic nghiệp vụ ---
        private readonly LichHenGioBUS _lichHenGioBUS;

        // --- Dữ liệu ---
        private List<Event> _events = new List<Event>();
        private readonly int _currentTeacherId;
        private List<Shift> _shifts;
        private DateTime _currentWeekStart;
        private DateTime _selectedDateOnCalendar;
        private List<Guna2Button> _calendarDayButtons = new List<Guna2Button>();

        // --- Biến cho chức năng kéo-thả ---
        private bool _isDragging = false;
        private Point _startPoint;
        private Rectangle _previewRect;
        private Rectangle _lastPreviewRect;
        private DateTime _previewStartTime;
        private DateTime _previewEndTime;

        // --- Biến cờ và biến cho auto-scroll ---
        private bool _isUpdatingCalendar = false;
        private int _scrollDirection = 0;
        private const int ScrollZoneHeight = 40;
        private const int ScrollAmount = 20;

        // --- THÊM MỚI: Biến cho hệ thống nhắc nhở ---
        private Timer _reminderTimer;
        private NotifyIcon _notifyIcon;
        public MainForm()
        {
            InitializeComponent();
            this.pnlCanvas.Dock = DockStyle.Top;

            _lichHenGioBUS = new LichHenGioBUS();

            _currentTeacherId = UserSession.TeacherId;
            if (_currentTeacherId == 0)
            {
                MessageBox.Show("Không xác định được người dùng. Vui lòng đăng nhập lại.", "Lỗi phiên đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Load += (s, e) => this.Close();
                return;
            }

            this.pnlCanvas.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.pnlCanvas, true, null);
            this.FormClosing += MainForm_FormClosing; // --- THÊM MỚI ---
        }

        #region Các hàm khởi tạo và cập nhật giao diện
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_currentTeacherId == 0)
            {
                this.Close();
                return;
            }

            LoadEventsFromDatabase();

            InitializeShifts();
            InitializeMiniCalendar();
            InitializeQuickTimer();
            InitializeReminderSystem(); // --- THÊM MỚI ---

            _selectedDateOnCalendar = DateTime.Today;
            _currentWeekStart = GetStartOfWeek(DateTime.Now, DayOfWeek.Sunday);
            UpdateWeekView();
        }

        private void InitializeQuickTimer()
        {
            var quickTimeOptions = new List<string> { "5 phút", "10 phút", "15 phút", "30 phút", "45 phút", "1 giờ", "2 giờ" };
            cmbQuickTime.DataSource = quickTimeOptions;
            cmbQuickTime.SelectedIndex = 0;
        }

        private void UpdateCanvasSize()
        {
            int canvasHeight = HourHeight * 24 + DayHeaderHeight;

            // Bây giờ chúng ta chỉ cần cập nhật chiều cao, 
            // chiều rộng đã được DockStyle.Top xử lý.
            pnlCanvas.Height = canvasHeight;
        }

        private void InitializeShifts()
        {
            _shifts = new List<Shift> {
                new Shift { Name = "Ca Sáng", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(12, 0, 0) },
                new Shift { Name = "Ca Chiều", StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                new Shift { Name = "Ca Tối", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(22, 0, 0) }
            };
        }

        private void UpdateWeekView()
        {
            _isUpdatingCalendar = true;
            DateTime weekEnd = _currentWeekStart.AddDays(6);
            if (_currentWeekStart.Month == weekEnd.Month) { lblMonthYear.Text = $"Tháng {_currentWeekStart.Month}, {_currentWeekStart.Year}"; }
            else { lblMonthYear.Text = $"Tháng {_currentWeekStart.Month} – Tháng {weekEnd.Month}, {_currentWeekStart.Year}"; }
            UpdateMiniCalendar();
            UpdateCanvasSize();
            pnlCanvas.Refresh();
            _isUpdatingCalendar = false;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateCanvasSize();
            pnlCanvas.Invalidate();
        }
        #endregion

        #region Tương tác Cơ sở dữ liệu (thông qua BUS)
        private void LoadEventsFromDatabase()
        {
            try
            {
                _events = _lichHenGioBUS.GetEventsByTeacherId(_currentTeacherId);
                // --- SỬA ĐỔI: Reset trạng thái nhắc nhở mỗi khi tải lại
                foreach (var evt in _events)
                {
                    evt.IsReminderSent = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu lịch từ cơ sở dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveEventToDatabase(Event newEvent)
        {
            try
            {
                _lichHenGioBUS.AddNewEvent(newEvent, _currentTeacherId);
                LoadEventsFromDatabase(); // Tải lại để có ID mới và đồng bộ dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu sự kiện vào cơ sở dữ liệu:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateEventInDatabase(Event eventToUpdate)
        {
            try
            {
                _lichHenGioBUS.UpdateEvent(eventToUpdate);
                // --- SỬA ĐỔI: Reset trạng thái nhắc nhở của sự kiện vừa cập nhật
                eventToUpdate.IsReminderSent = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật sự kiện:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteEventFromDatabase(Event eventToDelete)
        {
            try
            {
                _lichHenGioBUS.DeleteEvent(eventToDelete.Id);
                // Việc xóa khỏi list _events được xử lý ở HandleEventClick
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa sự kiện:\n" + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region --- THÊM MỚI: Hệ thống nhắc nhở ---
        // Trong MainForm.cs

        private void InitializeReminderSystem()
        {
            // Khởi tạo và cấu hình NotifyIcon
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Text = "Lịch hẹn của bạn";

            // Sử dụng icon có sẵn của hệ thống để tránh lỗi
            _notifyIcon.Icon = SystemIcons.Information;

            // Bắt buộc phải có dòng này để icon hiện ở khay hệ thống
            _notifyIcon.Visible = true;

            // Khởi tạo và cấu hình Timer
            _reminderTimer = new Timer();
            _reminderTimer.Interval = 30000; // Kiểm tra mỗi 30 giây
            _reminderTimer.Tick += ReminderTimer_Tick;
            _reminderTimer.Start();
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var nowMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

            // --- THAY ĐỔI DÒNG LỆNH LINQ BÊN DƯỚI ---
            var eventsToRemind = _events.Where(evt =>
                !evt.IsReminderSent &&
                evt.ReminderTime.HasValue && // Đảm bảo ReminderTime không null
                evt.ReminderTime.Value >= nowMinute &&
                evt.ReminderTime.Value < nowMinute.AddMinutes(1)
            ).ToList();

            foreach (var evt in eventsToRemind)
            {
                ElegantReminderForm reminder = new ElegantReminderForm(evt);
                reminder.Show();

                evt.IsReminderSent = true;
            }
        }

        // Hãy chắc chắn bạn đã dọn dẹp NotifyIcon khỏi các hàm khác nhé!

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dọn dẹp tài nguyên để tránh rò rỉ bộ nhớ
            _reminderTimer?.Stop();
            _reminderTimer?.Dispose();
            _notifyIcon?.Dispose();
        }
        #endregion

        #region Mini Calendar Logic
        private void InitializeMiniCalendar()
        {
            flpCalendarDays.Controls.Clear();
            _calendarDayButtons.Clear();
            for (int i = 0; i < 42; i++)
            {
                var dayButton = new Guna2Button
                {
                    Width = 40,
                    Height = 34,
                    Margin = new Padding(0),
                    Padding = new Padding(0),
                    Font = new Font("Segoe UI", 9f),
                    BorderRadius = 8,
                    FillColor = Color.FromArgb(46, 51, 73),
                    ForeColor = Color.White,
                    CheckedState = { FillColor = Color.FromArgb(0, 126, 249), ForeColor = Color.White }
                };
                dayButton.Click += DayButton_Click;
                flpCalendarDays.Controls.Add(dayButton);
                _calendarDayButtons.Add(dayButton);
            }
        }

        private void UpdateMiniCalendar()
        {
            if (!_calendarDayButtons.Any()) return;

            DateTime displayMonth = _selectedDateOnCalendar;
            lblCalMonthYear.Text = $"Tháng {displayMonth.Month}, {displayMonth.Year}";

            DateTime firstDayOfMonth = new DateTime(displayMonth.Year, displayMonth.Month, 1);
            int daysToOffset = ((int)firstDayOfMonth.DayOfWeek - (int)DayOfWeek.Sunday + 7) % 7;
            DateTime firstDayOfGrid = firstDayOfMonth.AddDays(-daysToOffset);

            for (int i = 0; i < 42; i++)
            {
                var button = _calendarDayButtons[i];
                DateTime currentDate = firstDayOfGrid.AddDays(i);

                button.Text = currentDate.Day.ToString();
                button.Tag = currentDate;
                button.Checked = false;

                if (currentDate.Month == displayMonth.Month)
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.FromArgb(0, 59, 149); // màu xanh đậm thay cho màu xám
                }
                else
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.FromArgb(12, 59, 135); // xanh sáng cho ngày ngoài tháng
                }

                // --- NHÓM 3: Ngày hôm nay ---
                if (currentDate.Date == DateTime.Today)
                {
                    button.FillColor = Color.FromArgb(0, 126, 249); // xanh dương sáng nổi bật
                    button.BorderThickness = 2;
                    button.BorderColor = Color.FromArgb(0, 172, 238);
                }
                else
                {
                    button.BorderThickness = 0;
                }

                // --- Ngày được chọn ---
                if (currentDate.Date == _selectedDateOnCalendar.Date)
                {
                    button.Checked = true;
                }
            }
            }


        private void DayButton_Click(object sender, EventArgs e)
        {
            if (_isUpdatingCalendar) return;
            var clickedButton = sender as Guna2Button;
            if (clickedButton?.Tag is DateTime selectedDate)
            {
                _selectedDateOnCalendar = selectedDate;
                _currentWeekStart = GetStartOfWeek(selectedDate, DayOfWeek.Sunday);
                UpdateWeekView();
            }
        }
        #endregion

        #region Vẽ Giao Diện (Painting)
        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int dayWidth = (pnlCanvas.Width - TimeColumnWidth) / 7;

            DrawGrid(g, dayWidth);
            DrawEvents(g, dayWidth);
            DrawDayHeaders(g, dayWidth);
            DrawCurrentTimeIndicator(g, dayWidth);
            DrawPreviewRectangle(g);
            DrawPreviewTimeText(g);
        }

        private void DrawGrid(Graphics g, int dayWidth)
        {
            using (Pen gridPen = new Pen(Color.FromArgb(224, 224, 224)))
            using (Brush textBrush = new SolidBrush(Color.DimGray))
            using (Font textFont = new Font("Segoe UI", 8))
            {
                for (int i = 0; i < 24; i++)
                {
                    int y = DayHeaderHeight + HeaderGridPadding + i * HourHeight;
                    g.DrawLine(gridPen, TimeColumnWidth, y, pnlCanvas.Width, y);
                    g.DrawString($"{i}:00", textFont, textBrush, 5, y - 6);
                }
                for (int i = 0; i < 7; i++)
                {
                    int x = TimeColumnWidth + i * dayWidth;
                    g.DrawLine(gridPen, x, 0, x, pnlCanvas.Height);
                }
            }
        }

        // Xóa hàm DrawDayHeaders cũ và thay bằng hàm mới này
        private void DrawDayHeaders(Graphics g, int dayWidth)
        {
            string[] vietnameseDayNames = { "CHỦ NHẬT", "THỨ 2", "THỨ 3", "THỨ 4", "THỨ 5", "THỨ 6", "THỨ 7" };
            Rectangle fullHeaderRect = new Rectangle(0, 0, pnlCanvas.Width, DayHeaderHeight);
            g.FillRectangle(SystemBrushes.Control, fullHeaderRect);

            using (Font dayOfWeekFont = new Font("Segoe UI", 8, FontStyle.Bold))
            using (Font dayNumberFont = new Font("Segoe UI", 14))
            {
                for (int i = 0; i < 7; i++)
                {
                    DateTime day = _currentWeekStart.AddDays(i);
                    string dayOfWeekText = vietnameseDayNames[(int)day.DayOfWeek];
                    string dayNumberText = day.Day.ToString();

                    Rectangle headerRect = new Rectangle(TimeColumnWidth + i * dayWidth, 0, dayWidth, DayHeaderHeight);

                    // Mặc định màu chữ
                    Color dayOfWeekColor = Color.DimGray;
                    Color numberColor = Color.Black;

                    // --- PHẦN NÂNG CẤP GIAO DIỆN CAO CẤP ---
                    if (day.Date == DateTime.Today)
                    {
                        // Đo kích thước của số ngày (với font in đậm) để vẽ nền chính xác
                        Size dayNumberSize;
                        using (Font boldDayNumberFont = new Font(dayNumberFont, FontStyle.Bold))
                        {
                            dayNumberSize = TextRenderer.MeasureText(dayNumberText, boldDayNumberFont);
                        }

                        // Vẽ nền hình "viên thuốc"
                        int pillHeight = dayNumberSize.Height + 6;
                        int pillWidth = dayNumberSize.Width + 20;
                        pillWidth = Math.Max(pillWidth, pillHeight); // Đảm bảo hình tròn đẹp với số có 1 chữ số

                        Rectangle pillRect = new Rectangle(
                            headerRect.X + (headerRect.Width - pillWidth) / 2,
                            headerRect.Y + (DayHeaderHeight - pillHeight) / 2 + 8, // Căn giữa và đẩy xuống 1 chút
                            pillWidth,
                            pillHeight
                        );

                        using (var path = CreateRoundedRectanglePath(pillRect, pillHeight / 2))
                        {
                            // --- NÂNG CẤP 1 (TÙY CHỌN): VẼ BÓNG MỜ (DROP SHADOW) ---
                            // Vẽ một bản sao của path với màu đen mờ và lệch đi một chút
                            g.TranslateTransform(1, 2); // Dịch chuyển 1px sang phải, 2px xuống dưới
                            using (var shadowBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 0)))
                            {
                                g.FillPath(shadowBrush, path);
                            }
                            g.ResetTransform(); // Reset lại vị trí vẽ

                            // --- NÂNG CẤP 2: SỬ DỤNG GRADIENT ---
                            // Tạo một gradient đẹp mắt thay vì màu đơn sắc
                            using (var gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                                pillRect,
                                Color.FromArgb(0, 172, 238), // Màu xanh da trời sáng (Sky Blue)
                                Color.FromArgb(0, 126, 249), // Màu xanh dương bạn đã dùng (Royal Blue)
                                90f)) // Góc gradient
                            {
                                g.FillPath(gradientBrush, path);
                            }
                        }

                        // Cập nhật màu chữ cho ngày hôm nay
                        dayOfWeekColor = Color.FromArgb(0, 126, 249); // Giữ màu xanh cho chữ "THỨ"
                        numberColor = Color.White;

                        // --- NÂNG CẤP 3: IN ĐẬM SỐ NGÀY HIỆN TẠI ---
                        // Tính toán lại vị trí và vẽ số ngày bằng font in đậm
                        Point dayNumberPos = new Point(
                            headerRect.X + (headerRect.Width - dayNumberSize.Width) / 2,
                            pillRect.Y + (pillRect.Height - dayNumberSize.Height) / 2
                        );
                        using (Font boldDayNumberFont = new Font(dayNumberFont, FontStyle.Bold))
                        {
                            TextRenderer.DrawText(g, dayNumberText, boldDayNumberFont, dayNumberPos, numberColor);
                        }
                    }
                    else
                    {
                        // Vẽ ngày bình thường (không phải hôm nay)
                        Size dayNumberSize = TextRenderer.MeasureText(dayNumberText, dayNumberFont);
                        Point dayNumberPos = new Point(headerRect.X + (headerRect.Width - dayNumberSize.Width) / 2, headerRect.Y + 28);
                        TextRenderer.DrawText(g, dayNumberText, dayNumberFont, dayNumberPos, numberColor);
                    }

                    // Vẽ chữ cho THỨ (T2, T3...)
                    Size dayOfWeekSize = TextRenderer.MeasureText(dayOfWeekText, dayOfWeekFont);
                    Point dayOfWeekPos = new Point(headerRect.X + (headerRect.Width - dayOfWeekSize.Width) / 2, headerRect.Y + 10);
                    TextRenderer.DrawText(g, dayOfWeekText, dayOfWeekFont, dayOfWeekPos, dayOfWeekColor);
                }
            }

            // Vẽ lại header cho cột thời gian và đường kẻ ngang
            Rectangle timeHeaderRect = new Rectangle(0, 0, TimeColumnWidth, DayHeaderHeight);
            g.FillRectangle(SystemBrushes.Control, timeHeaderRect);
            using (Pen borderPen = new Pen(Color.Gainsboro))
            {
                g.DrawLine(borderPen, 0, DayHeaderHeight - 1, pnlCanvas.Width, DayHeaderHeight - 1);
            }
        }

        // HÀM PHỤ ĐÃ CÓ: Đảm bảo bạn vẫn còn hàm này trong code
        private System.Drawing.Drawing2D.GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void DrawEvents(Graphics g, int dayWidth)
        {
            // StringFormat cho phép xuống dòng (dùng cho tiêu đề cột rộng và giờ cột hẹp)
            using (StringFormat multiLineSf = new StringFormat())
            {
                multiLineSf.Trimming = StringTrimming.EllipsisCharacter;
                multiLineSf.FormatFlags = StringFormatFlags.LineLimit;
                multiLineSf.LineAlignment = StringAlignment.Near;

                // StringFormat KHÔNG cho phép xuống dòng (dùng cho giờ ở cột rộng)
                using (StringFormat singleLineSf = new StringFormat())
                {
                    singleLineSf.Trimming = StringTrimming.EllipsisCharacter;
                    singleLineSf.FormatFlags = StringFormatFlags.NoWrap;
                    singleLineSf.LineAlignment = StringAlignment.Near;

                    for (int dayIndex = 0; dayIndex < 7; dayIndex++)
                    {
                        DateTime currentDate = _currentWeekStart.AddDays(dayIndex);
                        var dailyEvents = _events.Where(ev => ev.StartTime.Date == currentDate.Date).OrderBy(ev => ev.StartTime).ToList();
                        if (!dailyEvents.Any()) continue;

                        List<List<Event>> collisionGroups = new List<List<Event>>();
                        foreach (var evt in dailyEvents)
                        {
                            bool placed = false;
                            foreach (var group in collisionGroups)
                            {
                                if (group.Last().EndTime <= evt.StartTime) { group.Add(evt); placed = true; break; }
                            }
                            if (!placed) { collisionGroups.Add(new List<Event> { evt }); }
                        }

                        int totalColumns = collisionGroups.Count;
                        if (totalColumns == 0) continue;
                        int eventWidth = Math.Max(10, (dayWidth - 4) / totalColumns);

                        for (int colIndex = 0; colIndex < collisionGroups.Count; colIndex++)
                        {
                            var group = collisionGroups[colIndex];
                            foreach (var evt in group)
                            {
                                int x = TimeColumnWidth + dayIndex * dayWidth + colIndex * eventWidth + 2;
                                double startY = DayHeaderHeight + HeaderGridPadding + evt.StartTime.TimeOfDay.TotalHours * HourHeight;
                                double endY = DayHeaderHeight + HeaderGridPadding + evt.EndTime.TimeOfDay.TotalHours * HourHeight;
                                float height = (float)(endY - startY);
                                RectangleF eventRect = new RectangleF(x, (float)startY, eventWidth - 2, Math.Max(2, height));

                                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                                {
                                    int cornerRadius = Math.Min(8, (int)eventRect.Height / 3);
                                    if (cornerRadius > 0)
                                    {
                                        path.AddArc(eventRect.X, eventRect.Y, cornerRadius, cornerRadius, 180, 90);
                                        path.AddArc(eventRect.Right - cornerRadius, eventRect.Y, cornerRadius, cornerRadius, 270, 90);
                                        path.AddArc(eventRect.Right - cornerRadius, eventRect.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                                        path.AddArc(eventRect.X, eventRect.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                                        path.CloseFigure();
                                    }
                                    else { path.AddRectangle(eventRect); }

                                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(220, evt.EventColor))) { g.FillPath(brush, path); }
                                }

                                RectangleF textRect = eventRect;
                                textRect.Inflate(-5, -3);

                                using (Font eventFont = new Font("Segoe UI", 8, FontStyle.Bold))
                                using (SolidBrush textBrush = new SolidBrush(evt.TextColor))
                                {
                                    float singleLineHeight = eventFont.GetHeight(g);
                                    const int NARROW_COLUMN_THRESHOLD = 45; // Ngưỡng để coi là cột hẹp

                                    if (eventWidth < NARROW_COLUMN_THRESHOLD)
                                    {
                                        // --- LOGIC CHO CỘT HẸP (TRƯỜNG HỢP ĐẶC BIỆT) ---
                                        if (textRect.Height >= singleLineHeight && textRect.Width > 10)
                                        {
                                            // TẠO CHUỖI GIỜ CÓ DẤU CÁCH ĐỂ CÓ THỂ TỰ XUỐNG DÒNG
                                            string timeText = $"{evt.StartTime:HH:mm} - {evt.EndTime:HH:mm}";

                                            // Căn giữa giờ và cho phép nó xuống dòng nếu cần
                                            StringFormat centeredTimeSf = new StringFormat(multiLineSf);
                                            centeredTimeSf.LineAlignment = StringAlignment.Center;

                                            // Dùng multiLineSf để vẽ, cho phép giờ tự ngắt dòng
                                            g.DrawString(timeText, eventFont, textBrush, textRect, centeredTimeSf);
                                        }
                                    }
                                    else
                                    {
                                        // --- LOGIC CHO CỘT RỘNG (BÌNH THƯỜNG) ---
                                        if (textRect.Height >= singleLineHeight)
                                        {
                                            RectangleF timeRect = new RectangleF(textRect.Left, textRect.Bottom - singleLineHeight, textRect.Width, singleLineHeight);
                                            RectangleF titleRect = new RectangleF(textRect.Left, textRect.Top, textRect.Width, textRect.Height - singleLineHeight);

                                            if (titleRect.Height > 0)
                                            {
                                                g.DrawString(evt.Title, eventFont, textBrush, titleRect, multiLineSf);
                                            }

                                            string timeText = $"{evt.StartTime:HH:mm}-{evt.EndTime:HH:mm}";
                                            g.DrawString(timeText, eventFont, textBrush, timeRect, singleLineSf);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DrawCurrentTimeIndicator(Graphics g, int dayWidth)
        {
            if (_currentWeekStart.Date > DateTime.Today || _currentWeekStart.AddDays(7).Date <= DateTime.Today) { return; }
            DateTime now = DateTime.Now;
            int dayIndex = (int)(now.Date - _currentWeekStart.Date).TotalDays;
            if (dayIndex < 0 || dayIndex > 6) return;
            int x = TimeColumnWidth + dayIndex * dayWidth;
            double y = DayHeaderHeight + HeaderGridPadding + now.TimeOfDay.TotalHours * HourHeight;
            using (Pen redPen = new Pen(Color.Red, 2))
            {
                g.FillEllipse(Brushes.Red, x - 5, (float)y - 5, 10, 10);
                g.DrawLine(redPen, x, (float)y, x + dayWidth, (float)y);
            }
        }

        private void DrawPreviewRectangle(Graphics g)
        {
            if (_isDragging && !_previewRect.IsEmpty)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, 70, 130, 180))) { g.FillRectangle(brush, _previewRect); }
                g.DrawRectangle(Pens.SteelBlue, _previewRect);
            }
        }

        private void DrawPreviewTimeText(Graphics g)
        {
            if (!_isDragging || _previewRect.IsEmpty) return;
            string timeText = $"{_previewStartTime:HH:mm} - {_previewEndTime:HH:mm}";
            using (Font timeFont = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(timeText, timeFont);
                PointF textLocation = new PointF(_previewRect.Left + 5, _previewRect.Bottom + 5);
                if (textLocation.Y + textSize.Height > pnlCanvas.Height) { textLocation.Y = _previewRect.Top - textSize.Height - 5; }
                RectangleF backgroundRect = new RectangleF(textLocation, textSize);
                backgroundRect.Inflate(4, 2);
                using (var bgBrush = new SolidBrush(Color.FromArgb(220, Color.WhiteSmoke))) { g.FillRectangle(bgBrush, backgroundRect); }
                g.DrawRectangle(Pens.Gray, Rectangle.Round(backgroundRect));
                g.DrawString(timeText, timeFont, Brushes.Black, textLocation);
            }
        }
        #endregion

        #region Xử lý Chuột (Kéo-Thả và Click)
        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X > TimeColumnWidth && e.Y > DayHeaderHeight + HeaderGridPadding)
            {
                _isDragging = true;
                _startPoint = e.Location;
                _lastPreviewRect = Rectangle.Empty;
                pnlCanvas.Cursor = Cursors.Cross;
            }
        }

        private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                _lastPreviewRect = _previewRect;
                int dayWidth = (pnlCanvas.Width - TimeColumnWidth) / 7;
                int startDayIndex = (_startPoint.X - TimeColumnWidth) / dayWidth;
                int currentDayIndex = (e.X - TimeColumnWidth) / dayWidth;
                if (startDayIndex < 0 || startDayIndex > 6 || startDayIndex != currentDayIndex)
                {
                    _isDragging = false;
                    pnlCanvas.Invalidate(_lastPreviewRect);
                    return;
                }
                int x = TimeColumnWidth + startDayIndex * dayWidth;
                _previewRect = new Rectangle(x, Math.Min(_startPoint.Y, e.Y), dayWidth, Math.Abs(_startPoint.Y - e.Y));
                DateTime selectedDate = _currentWeekStart.AddDays(startDayIndex);
                double startHours = Math.Max(0, (double)(_previewRect.Top - DayHeaderHeight - HeaderGridPadding) / HourHeight);
                double endHours = Math.Min(24, (double)(_previewRect.Bottom - DayHeaderHeight - HeaderGridPadding) / HourHeight);
                _previewStartTime = selectedDate.AddHours(startHours);
                _previewEndTime = selectedDate.AddHours(endHours);
                Rectangle invalidationRect = Rectangle.Union(_lastPreviewRect, _previewRect);
                invalidationRect.Inflate(0, 40);
                pnlCanvas.Invalidate(invalidationRect);
                Point mousePosInContainer = pnlScrollContainer.PointToClient(pnlCanvas.PointToScreen(e.Location));
                if (mousePosInContainer.Y < ScrollZoneHeight)
                {
                    _scrollDirection = -1;
                    scrollTimer.Start();
                }
                else if (mousePosInContainer.Y > pnlScrollContainer.ClientSize.Height - ScrollZoneHeight)
                {
                    _scrollDirection = 1;
                    scrollTimer.Start();
                }
                else
                {
                    _scrollDirection = 0;
                    scrollTimer.Stop();
                }
            }
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            pnlCanvas.Cursor = Cursors.Default;
            if (!_isDragging) return;

            _isDragging = false;
            scrollTimer.Stop();

            bool isClick = Math.Abs(_startPoint.Y - e.Y) < 10 && Math.Abs(_startPoint.X - e.X) < 10;

            if (isClick)
            {
                HandleEventClick(e.Location);
                _previewRect = Rectangle.Empty;
                pnlCanvas.Invalidate();
            }
            else
            {
                HandleEventCreation(e.Location);
                _previewRect = Rectangle.Empty;
                pnlCanvas.Invalidate();
            }
        }

        private void HandleEventClick(Point clickLocation)
        {
            Event clickedEvent = FindEventAtLocation(clickLocation);
            if (clickedEvent != null)
            {
                using (EventDetailsForm form = new EventDetailsForm(clickedEvent))
                {
                    var result = form.ShowDialog();

                    if (result == DialogResult.Abort)
                    {
                        DeleteEventFromDatabase(clickedEvent);
                        _events.Remove(clickedEvent); // Xóa khỏi danh sách trong bộ nhớ
                        pnlCanvas.Refresh();
                    }
                    else if (result == DialogResult.OK)
                    {
                        UpdateEventInDatabase(clickedEvent);
                        pnlCanvas.Refresh();
                    }
                }
            }
        }

        private Event FindEventAtLocation(Point location)
        {
            int dayWidth = (pnlCanvas.Width - TimeColumnWidth) / 7;
            foreach (Event evt in _events.AsEnumerable().Reverse())
            {
                if (evt.StartTime.Date >= _currentWeekStart.Date && evt.StartTime.Date < _currentWeekStart.AddDays(7).Date)
                {
                    int dayIndex = (int)(evt.StartTime.Date - _currentWeekStart.Date).TotalDays;
                    var dailyEvents = _events.Where(e => e.StartTime.Date == evt.StartTime.Date).OrderBy(e => e.StartTime).ToList();
                    List<List<Event>> collisionGroups = new List<List<Event>>();
                    foreach (var currentEvt in dailyEvents)
                    {
                        bool placed = false;
                        foreach (var group in collisionGroups)
                        {
                            if (!group.Any() || group.Last().EndTime <= currentEvt.StartTime) { group.Add(currentEvt); placed = true; break; }
                        }
                        if (!placed) { collisionGroups.Add(new List<Event> { currentEvt }); }
                    }
                    int totalColumns = collisionGroups.Count;
                    if (totalColumns == 0) continue;
                    int eventWidth = (dayWidth - 4) / totalColumns;
                    int colIndex = -1;
                    for (int c = 0; c < totalColumns; c++)
                    {
                        if (collisionGroups[c].Contains(evt))
                        {
                            colIndex = c;
                            break;
                        }
                    }
                    if (colIndex == -1) continue;
                    int x = TimeColumnWidth + dayIndex * dayWidth + colIndex * eventWidth + 2;
                    double startY = DayHeaderHeight + HeaderGridPadding + evt.StartTime.TimeOfDay.TotalHours * HourHeight;
                    double endY = DayHeaderHeight + HeaderGridPadding + evt.EndTime.TimeOfDay.TotalHours * HourHeight;
                    RectangleF eventRect = new RectangleF(x, (float)startY, eventWidth - 2, (float)(endY - startY));
                    if (eventRect.Contains(location))
                    {
                        return evt;
                    }
                }
            }
            return null;
        }

        private void HandleEventCreation(Point endPoint)
        {
            int dayWidth = (pnlCanvas.Width - TimeColumnWidth) / 7;
            int dayIndex = (_startPoint.X - TimeColumnWidth) / dayWidth;
            if (dayIndex < 0 || dayIndex > 6) return;
            DateTime selectedDate = _currentWeekStart.AddDays(dayIndex);
            double startHours = (double)(Math.Min(_startPoint.Y, endPoint.Y) - DayHeaderHeight - HeaderGridPadding) / HourHeight;
            double endHours = (double)(Math.Max(_startPoint.Y, endPoint.Y) - DayHeaderHeight - HeaderGridPadding) / HourHeight;
            startHours = Math.Max(0, startHours);
            endHours = Math.Min(24, endHours);
            if (endHours - startHours < 0.25) return;
            DateTime startTime = selectedDate.AddHours(startHours);
            DateTime endTime = selectedDate.AddHours(endHours);
            using (EventForm form = new EventForm(startTime, endTime))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SaveEventToDatabase(form.NewEvent);
                    pnlCanvas.Refresh();
                }
            }
        }
        #endregion

        #region Điều hướng và Chức năng khác
        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            if (!_isDragging || _scrollDirection == 0)
            {
                scrollTimer.Stop();
                return;
            }
            int newScrollValue = pnlScrollContainer.VerticalScroll.Value + (_scrollDirection * ScrollAmount);
            newScrollValue = Math.Max(pnlScrollContainer.VerticalScroll.Minimum, newScrollValue);
            newScrollValue = Math.Min(pnlScrollContainer.VerticalScroll.Maximum, newScrollValue);
            if (pnlScrollContainer.VerticalScroll.Value != newScrollValue)
            {
                pnlScrollContainer.VerticalScroll.Value = newScrollValue;
                pnlScrollContainer.PerformLayout();
                Point currentMouseScreenPos = Cursor.Position;
                Point currentMouseCanvasPos = pnlCanvas.PointToClient(currentMouseScreenPos);
                pnlCanvas_MouseMove(sender, new MouseEventArgs(MouseButtons.Left, 1, currentMouseCanvasPos.X, currentMouseCanvasPos.Y, 0));
            }
            else
            {
                scrollTimer.Stop();
            }
        }

        private void btnCalPrev_Click(object sender, EventArgs e)
        {
            _selectedDateOnCalendar = _selectedDateOnCalendar.AddMonths(-1);
            _currentWeekStart = GetStartOfWeek(_selectedDateOnCalendar, DayOfWeek.Sunday);
            UpdateWeekView();
        }

        private void btnCalNext_Click(object sender, EventArgs e)
        {
            _selectedDateOnCalendar = _selectedDateOnCalendar.AddMonths(1);
            _currentWeekStart = GetStartOfWeek(_selectedDateOnCalendar, DayOfWeek.Sunday);
            UpdateWeekView();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentWeekStart = _currentWeekStart.AddDays(-7);
            _selectedDateOnCalendar = _currentWeekStart;
            UpdateWeekView();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentWeekStart = _currentWeekStart.AddDays(7);
            _selectedDateOnCalendar = _currentWeekStart;
            UpdateWeekView();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            _selectedDateOnCalendar = DateTime.Today;
            _currentWeekStart = GetStartOfWeek(DateTime.Now, DayOfWeek.Sunday);
            UpdateWeekView();
            double targetY = DayHeaderHeight + HeaderGridPadding + DateTime.Now.TimeOfDay.TotalHours * HourHeight;
            int scrollValue = (int)targetY - (pnlScrollContainer.ClientSize.Height / 2);
            scrollValue = Math.Max(pnlScrollContainer.VerticalScroll.Minimum, scrollValue);
            scrollValue = Math.Min(pnlScrollContainer.VerticalScroll.Maximum - pnlScrollContainer.VerticalScroll.LargeChange + 1, scrollValue);
            pnlScrollContainer.VerticalScroll.Value = scrollValue;
            pnlScrollContainer.PerformLayout();
        }

        private void AddEventByShift(Shift shift)
        {
            DateTime selectedDay = _selectedDateOnCalendar.Date;
            DateTime startTime = selectedDay + shift.StartTime;
            DateTime endTime = selectedDay + shift.EndTime;
            using (EventForm form = new EventForm(startTime, endTime))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    SaveEventToDatabase(form.NewEvent);
                    if (selectedDay >= _currentWeekStart && selectedDay < _currentWeekStart.AddDays(7))
                    {
                        pnlCanvas.Refresh();
                    }
                }
            }
        }

        private void btnShiftMorning_Click(object sender, EventArgs e) { AddEventByShift(_shifts[0]); }
        private void btnShiftAfternoon_Click(object sender, EventArgs e) { AddEventByShift(_shifts[1]); }
        private void btnShiftEvening_Click(object sender, EventArgs e) { AddEventByShift(_shifts[2]); }

        private DateTime GetStartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        private void btnQuickAdd_Click(object sender, EventArgs e)
        {
            string userInput = cmbQuickTime.Text;
            int minutes = ParseMinutesFromInput(userInput);
            if (minutes <= 0)
            {
                MessageBox.Show("Vui lòng nhập một khoảng thời gian hợp lệ (ví dụ: '15 phút', '1 giờ', hoặc '25').",
                                "Thời gian không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AddQuickEvent(minutes);
        }

        // Trong MainForm.cs
        // Trong MainForm.cs

        private void AddQuickEvent(int minutes)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddMinutes(minutes);
            string title = minutes < 60 ? $"Hẹn giờ {minutes} phút" : $"Hẹn giờ {minutes / 60.0:G2} giờ";
            const string quickAddIdentifier = "::QUICK_ADD_EVENT::";
            Event newEvent = new Event
            {
                Title = title,
                Description = $"Sự kiện được tạo nhanh.{quickAddIdentifier}",
                StartTime = startTime,
                EndTime = endTime,
                EventColor = Color.FromArgb(231, 76, 60), // Màu đỏ Alizarin
                TextColor = Color.White,

                // Quy tắc: Hẹn giờ nhanh luôn nhắc vào đúng lúc kết thúc
                ReminderTime = endTime
            };

            // --- BẮT ĐẦU THAY ĐỔI QUAN TRỌNG ---
            // Bỏ qua việc mở form, gọi thẳng hàm lưu vào CSDL
            SaveEventToDatabase(newEvent);

            // Cập nhật lại giao diện để hiển thị sự kiện mới
            DateTime today = DateTime.Today;
            bool isCurrentWeekDisplayed = (today >= _currentWeekStart.Date && today < _currentWeekStart.AddDays(7));
            if (!isCurrentWeekDisplayed)
            {
                // Nếu người dùng đang xem tuần khác, tự động chuyển về tuần hiện tại
                btnToday_Click(this, EventArgs.Empty);
            }
            else
            {
                // Nếu đang xem tuần hiện tại, chỉ cần vẽ lại lịch
                pnlCanvas.Refresh();
            }
            // --- KẾT THÚC THAY ĐỔI ---
        }

        private int ParseMinutesFromInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;
            input = input.Trim().ToLower();
            int minutes = 0;
            try
            {
                string numberPart = System.Text.RegularExpressions.Regex.Match(input, @"\d+(\.\d+)?").Value;
                if (string.IsNullOrEmpty(numberPart)) return 0;
                if (input.Contains("giờ"))
                {
                    if (double.TryParse(numberPart, NumberStyles.Any, CultureInfo.InvariantCulture, out double hours))
                        minutes = (int)(hours * 60);
                }
                else
                {
                    if (int.TryParse(numberPart, out int parsedMinutes))
                        minutes = parsedMinutes;
                }
            }
            catch { return 0; }
            return minutes;
        }
        #endregion

        private void flpCalendarDays_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}