namespace LmsProject.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int QuizId { get; set; }

        public string Text { get; set; } = string.Empty;
        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string OptionD { get; set; } = string.Empty;

        // Will store "A", "B", "C", or "D" based on the instructor's radio button selection
        public string CorrectOption { get; set; } = string.Empty;
        public decimal MarkValue { get; set; }

        public Quiz Quiz { get; set; } = null!;
    }
}