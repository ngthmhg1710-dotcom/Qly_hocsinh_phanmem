using System;

namespace DTO.Game
{
    public class CauHoiRandomDTO
    {
        // QUAN TRỌNG: Phải để tên tiếng Anh như dưới đây để khớp với Form1 và GameForm
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string ImagePath { get; set; }

        public CauHoiRandomDTO()
        {
            QuestionText = string.Empty;
            ImagePath = string.Empty;
        }
    }
}