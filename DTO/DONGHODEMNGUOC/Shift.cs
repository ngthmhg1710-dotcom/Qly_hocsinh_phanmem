using System;

namespace DTO
{
    // Lớp này đại diện cho một ca làm việc
    public class Shift
    {
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}