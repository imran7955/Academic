using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LmsProject.Application.Services;
using LmsProject.Web.Models;

namespace LmsProject.Web.Controllers
{
    [Authorize] // Only logged-in users can interact with quizzes
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;
        private readonly ICourseService _courseService;

        public QuizController(IQuizService quizService, ICourseService courseService)
        {
            _quizService = quizService;
            _courseService = courseService;
        }

        // GET: Quiz/Info/5
        // Shows the description and "Start" button
        public async Task<IActionResult> Info(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null) return Unauthorized();

            var quiz = await _quizService.GetActiveQuizForStudentAsync(id);
            if (quiz == null) return NotFound();

            // Prevent showing info if they already took it
            var leaderboard = await _quizService.GetQuizLeaderboardAsync(id);
            var alreadyAttempted = leaderboard.Any(l => l.Attempt.IdentityUserId == currentUserId);
            if (alreadyAttempted)
            {
                return RedirectToAction(nameof(Result), new { id = quiz.Id });
            }

            return View(quiz);
        }

        // GET: Quiz/Take/5
        // Loads the actual questions and starts the JS timer
        public async Task<IActionResult> Take(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var quiz = await _quizService.GetActiveQuizForStudentAsync(id);
            if (quiz == null) return NotFound();

            var leaderboard = await _quizService.GetQuizLeaderboardAsync(id);
            if (leaderboard.Any(l => l.Attempt.IdentityUserId == currentUserId))
            {
                return RedirectToAction(nameof(Result), new { id = quiz.Id }); // Cannot retake
            }

            var viewModel = new TakeQuizViewModel
            {
                QuizId = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                TimeLimitMinutes = quiz.TimeLimitMinutes,
                Questions = quiz.Questions.Select(q => new QuizQuestionViewModel
                {
                    QuestionId = q.Id,
                    Text = q.Text,
                    OptionA = q.OptionA,
                    OptionB = q.OptionB,
                    OptionC = q.OptionC,
                    OptionD = q.OptionD,
                    MarkValue = q.MarkValue
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Quiz/Submit/5
        // Parses form data, calculates grade via service, redirects to result
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int id, IFormCollection form)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null) return Unauthorized();

            var userAnswers = new Dictionary<int, string>();

            // The frontend radio buttons will be named "q_15" where 15 is the QuestionId
            foreach (var key in form.Keys)
            {
                if (key.StartsWith("q_"))
                {
                    if (int.TryParse(key.Substring(2), out int qId))
                    {
                        userAnswers[qId] = form[key].ToString();
                    }
                }
            }

            var attempt = await _quizService.SubmitQuizAttemptAsync(id, currentUserId, userAnswers);

            if (attempt == null)
            {
                // Indicates they already took it or quiz not found
                return RedirectToAction(nameof(Result), new { id = id });
            }

            return RedirectToAction(nameof(Result), new { id = id });
        }

        // GET: Quiz/Result/5
        // Shows user's score and the Leaderboard
        public async Task<IActionResult> Result(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var quiz = await _quizService.GetActiveQuizForStudentAsync(id);
            if (quiz == null) return NotFound();

            var rawLeaderboard = await _quizService.GetQuizLeaderboardAsync(id);

            // Find the current user's score from the leaderboard
            var userAttempt = rawLeaderboard.FirstOrDefault(l => l.Attempt.IdentityUserId == currentUserId);
            decimal studentScore = userAttempt.Attempt != null ? userAttempt.Attempt.Score : 0;

            decimal totalPossible = quiz.Questions.Sum(q => q.MarkValue);

            var resultModel = new QuizResultViewModel
            {
                QuizId = quiz.Id,
                CourseId = quiz.CourseId,
                Title = quiz.Title,
                StudentScore = studentScore,
                TotalPossibleMarks = totalPossible,
                Leaderboard = rawLeaderboard.Select(l => new LeaderboardEntry
                {
                    StudentName = l.StudentName,
                    Score = l.Attempt.Score,
                    CompletedAt = l.Attempt.CompletedAt,
                    // ADD THIS EXACT LINE
                    IsCurrentUser = l.Attempt.IdentityUserId == currentUserId
                }).ToList()
            };

            return View(resultModel);
        }
    }
}