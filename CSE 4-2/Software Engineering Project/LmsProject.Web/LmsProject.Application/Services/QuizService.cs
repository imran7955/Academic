using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;

namespace LmsProject.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task CreateQuizAsync(Quiz quiz)
        {
            await _quizRepository.AddQuizAsync(quiz);
        }

        public async Task<Quiz?> GetActiveQuizForStudentAsync(int quizId)
        {
            return await _quizRepository.GetQuizByIdWithQuestionsAsync(quizId);
        }

        public async Task<QuizAttempt?> SubmitQuizAttemptAsync(int quizId, string identityUserId, Dictionary<int, string> userAnswers)
        {
            // ENFORCE BUSINESS RULE: One-time attempt only
            bool alreadyAttempted = await _quizRepository.HasUserAttemptedQuizAsync(quizId, identityUserId);
            if (alreadyAttempted) return null;

            var quiz = await _quizRepository.GetQuizByIdWithQuestionsAsync(quizId);
            if (quiz == null) return null;

            decimal totalScore = 0;

            // GRADE THE QUIZ INSTANTLY
            foreach (var question in quiz.Questions)
            {
                if (userAnswers.TryGetValue(question.Id, out var selectedOption))
                {
                    // Check if the selected option string (e.g. "A") matches the CorrectOption string
                    if (string.Equals(question.CorrectOption, selectedOption, StringComparison.OrdinalIgnoreCase))
                    {
                        totalScore += question.MarkValue;
                    }
                }
            }

            var attempt = new QuizAttempt
            {
                QuizId = quizId,
                IdentityUserId = identityUserId,
                Score = totalScore,
                CompletedAt = DateTime.UtcNow
            };

            await _quizRepository.AddQuizAttemptAsync(attempt);
            return attempt;
        }

        public async Task<IEnumerable<(QuizAttempt Attempt, string StudentName)>> GetQuizLeaderboardAsync(int quizId)
        {
            return await _quizRepository.GetLeaderboardForQuizAsync(quizId);
        }
    }
}