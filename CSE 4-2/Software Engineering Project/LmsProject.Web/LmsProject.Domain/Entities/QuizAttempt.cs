using System;

namespace LmsProject.Domain.Entities
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string IdentityUserId { get; set; } = string.Empty;

        public decimal Score { get; set; }
        public DateTime CompletedAt { get; set; }

        public Quiz Quiz { get; set; } = null!;
    }
}