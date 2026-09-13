using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;
using LmsProject.Infrastructure.Persistence;

namespace LmsProject.Infrastructure.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddQuizAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task<Quiz?> GetQuizByIdWithQuestionsAsync(int quizId)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task<IEnumerable<(QuizAttempt Attempt, string StudentName)>> GetLeaderboardForQuizAsync(int quizId)
        {
            // Joins the QuizAttempt with the UserProfile to grab the actual FullName instantly
            var query = from attempt in _context.QuizAttempts
                        join user in _context.UserProfiles on attempt.IdentityUserId equals user.IdentityUserId
                        where attempt.QuizId == quizId
                        orderby attempt.Score descending, attempt.CompletedAt ascending
                        select new { Attempt = attempt, StudentName = user.FullName };

            var results = await query.ToListAsync();

            return results.Select(r => (r.Attempt, r.StudentName));
        }

        public async Task AddQuizAttemptAsync(QuizAttempt attempt)
        {
            await _context.QuizAttempts.AddAsync(attempt);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasUserAttemptedQuizAsync(int quizId, string identityUserId)
        {
            return await _context.QuizAttempts
                .AnyAsync(qa => qa.QuizId == quizId && qa.IdentityUserId == identityUserId);
        }
    }
}