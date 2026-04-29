namespace DTO
{
    public class Flashcard
    {
        public string FrontText { get; set; }
        public string BackText { get; set; }
        public string FrontImagePath { get; set; }
        public string BackImagePath { get; set; }

        public Flashcard(string frontText, string backText, string frontImagePath = null, string backImagePath = null)
        {
            FrontText = frontText;
            BackText = backText;
            FrontImagePath = frontImagePath;
            BackImagePath = backImagePath;
        }
    }
}