// File: Question.cs
namespace RANDOMSO
{
    public class Question
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        // Chỉ cần một đường dẫn ảnh duy nhất
        public string ImagePath { get; set; }

        public Question()
        {
            // Khởi tạo giá trị mặc định
            QuestionText = string.Empty;
            ImagePath = string.Empty;
        }
    }
}