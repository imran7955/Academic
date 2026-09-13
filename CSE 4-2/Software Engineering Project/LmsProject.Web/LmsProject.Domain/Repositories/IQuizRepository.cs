using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;

namespace LmsProject.Domain.Repositories
{
    public interface IQuizRepository
    {
        Task AddQuizAsync(Quiz quiz);
        Task<Quiz?> GetQuizByIdWithQuestionsAsync(int quizId);

        // UPDATED: Returns the Attempt AND the matched Student's FullName
        Task<IEnumerable<(QuizAttempt Attempt, string StudentName)>> GetLeaderboardForQuizAsync(int quizId);

        Task AddQuizAttemptAsync(QuizAttempt attempt);
        Task<bool> HasUserAttemptedQuizAsync(int quizId, string identityUserId);
    }
}