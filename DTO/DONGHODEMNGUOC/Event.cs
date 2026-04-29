// Trong file Event.cs (Project DTO)

using System;
using System.Drawing;

namespace DTO
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Color EventColor { get; set; }
        public Color TextColor { get; set; }
        public DateTime? ReminderTime { get; set; }
        // --- THÊM DÒNG NÀY ---
        // Thuộc tính này không lưu trong CSDL, chỉ dùng lúc runtime
        public bool IsReminderSent { get; set; }
    }
}