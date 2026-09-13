using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;

namespace LmsProject.Application.Services
{
    public interface IQuizService
    {
        Task CreateQuizAsync(Quiz quiz);
        Task<Quiz?> GetActiveQuizForStudentAsync(int quizId);
        Task<QuizAttempt?> SubmitQuizAttemptAsync(int quizId, string identityUserId, Dictionary<int, string> userAnswers);
        Task<IEnumerable<(QuizAttempt Attempt, string StudentName)>> GetQuizLeaderboardAsync(int quizId);
    }
}