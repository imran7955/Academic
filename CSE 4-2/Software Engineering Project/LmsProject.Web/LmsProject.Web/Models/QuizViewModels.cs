using System;
using System.Collections.Generic;

namespace LmsProject.Web.Models
{
    // Models for the Instructor Form
    public class CreateQuizViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TimeLimitMinutes { get; set; }
        public DateTime AvailableFrom { get; set; } = DateTime.UtcNow;
        public DateTime AvailableTo { get; set; } = DateTime.UtcNow.AddDays(7);
        public List<CreateQuestionViewModel> Questions { get; set; } = new List<CreateQuestionViewModel>();
    }

    public class CreateQuestionViewModel
    {
        public string Text { get; set; } = string.Empty;
        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string OptionD { get; set; } = string.Empty;
        public string CorrectOption { get; set; } = "A";
        public decimal MarkValue { get; set; } = 1.0m;
    }

    // Models for the Student View
    public class TakeQuizViewModel
    {
        public int QuizId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TimeLimitMinutes { get; set; }
        public List<QuizQuestionViewModel> Questions { get; set; } = new List<QuizQuestionViewModel>();
    }

    public class QuizQuestionViewModel
    {
        public int QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string OptionA { get; set; } = string.Empty;
        public string OptionB { get; set; } = string.Empty;
        public string OptionC { get; set; } = string.Empty;
        public string OptionD { get; set; } = string.Empty;
        public decimal MarkValue { get; set; }
    }

    // Model for the Result & Leaderboard View
    public class QuizResultViewModel
    {
        public int QuizId { get; set; }

        // FIXED: Added CourseId so the Return to Course button works
        public int CourseId { get; set; }

        public string Title { get; set; } = string.Empty;
        public decimal StudentScore { get; set; }
        public decimal TotalPossibleMarks { get; set; }
        public List<LeaderboardEntry> Leaderboard { get; set; } = new List<LeaderboardEntry>();
    }

    public class LeaderboardEntry
    {
        public string StudentName { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public DateTime CompletedAt { get; set; }

        // FIXED: Added IsCurrentUser to properly highlight the logged-in user
        public bool IsCurrentUser { get; set; }
    }
}