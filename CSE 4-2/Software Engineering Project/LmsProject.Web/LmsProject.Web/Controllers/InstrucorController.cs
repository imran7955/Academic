using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using LmsProject.Application.Services;
using LmsProject.Domain.Entities;
using LmsProject.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace LmsProject.Web.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly ICourseService _courseService;
        private readonly IQuizService _quizService;
        private readonly IAttendanceService _attendanceService; // ADDED
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InstructorController(
            IInstructorService instructorService,
            ICourseService courseService,
            IQuizService quizService,
            IAttendanceService attendanceService, // ADDED
            UserManager<IdentityUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _instructorService = instructorService;
            _courseService = courseService;
            _quizService = quizService;
            _attendanceService = attendanceService; // ADDED
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<string?> ProcessUploadedFile(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "courses");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/courses/" + uniqueFileName;
        }

        public async Task<IActionResult> Dashboard()
        {
            var courses = await _courseService.GetFilteredCoursesAsync("N/A", "All", "All", "", null);
            var instructors = await _instructorService.GetAllInstructorsAsync();

            var courseList = courses.ToList();

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                courseList = courseList.Where(c => c.Instructors.Any(i => i.IdentityUserId == currentUserId)).ToList();
            }

            var model = new CourseViewModel
            {
                Courses = courseList,
                Instructors = instructors.ToList(),
                Domains = (await _courseService.GetFilterDomainsAsync()).ToList(),
                SelectedEnrollment = "N/A",
                SelectedDomain = "All",
                SelectedInstructor = "All",
                SearchTerm = string.Empty
            };

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateInstructor()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInstructor(string name, string email, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Name, Email, and Password details are all required inputs.");
                return await ReloadDashboardViewWithErrors();
            }

            var identityUser = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var identityResult = await _userManager.CreateAsync(identityUser, password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, "Instructor");
                await _instructorService.RegisterInstructorAsync(name, identityUser.Id);

                return RedirectToAction(nameof(Dashboard));
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return await ReloadDashboardViewWithErrors();
        }

        private async Task<IActionResult> ReloadDashboardViewWithErrors()
        {
            var courses = await _courseService.GetFilteredCoursesAsync("N/A", "All", "All", "", null);
            var instructors = await _instructorService.GetAllInstructorsAsync();

            var model = new CourseViewModel
            {
                Courses = courses.ToList(),
                Instructors = instructors.ToList(),
                Domains = (await _courseService.GetFilterDomainsAsync()).ToList(),
                SelectedEnrollment = "N/A",
                SelectedDomain = "All",
                SelectedInstructor = "All",
                SearchTerm = string.Empty
            };
            return View("Dashboard", model);
        }

        public async Task<IActionResult> CreateCourse()
        {
            var domainsList = await _courseService.GetFilterDomainsAsync();
            var allInstructorsList = await _instructorService.GetAllInstructorsAsync();

            ViewBag.Domains = domainsList.ToList();
            ViewBag.Instructors = allInstructorsList.ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(string title, string domain, string description, List<int> selectedInstructors, IFormFile? imageFile, DateTime enrollmentStartDate, DateTime enrollmentDeadline)
        {
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(domain))
            {
                var instructorIds = selectedInstructors ?? new List<int>();
                string? imageUrl = await ProcessUploadedFile(imageFile);

                await _instructorService.RegisterCourseAsync(title, domain, description, instructorIds, imageUrl, enrollmentStartDate, enrollmentDeadline);
                return RedirectToAction(nameof(Dashboard));
            }

            ViewBag.Domains = (await _courseService.GetFilterDomainsAsync()).ToList();
            ViewBag.Instructors = (await _instructorService.GetAllInstructorsAsync()).ToList();

            return View();
        }

        public async Task<IActionResult> EditCourse(int id)
        {
            var course = await _courseService.GetCourseDetailsAsync(id);
            if (course == null) return NotFound();

            if (!User.IsInRole("Admin"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!course.Instructors.Any(i => i.IdentityUserId == currentUserId)) return RedirectToAction("AccessDenied", "Account");
            }

            ViewBag.Domains = await _courseService.GetFilterDomainsAsync();
            ViewBag.Instructors = await _instructorService.GetAllInstructorsAsync();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(int id, string title, string domain, string description, List<int> selectedInstructors, IFormFile? imageFile, DateTime enrollmentStartDate, DateTime enrollmentDeadline)
        {
            if (id <= 0) return BadRequest("Invalid course tracking reference identifier context.");

            if (!User.IsInRole("Admin"))
            {
                var courseCheck = await _courseService.GetCourseDetailsAsync(id);
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (courseCheck == null || !courseCheck.Instructors.Any(i => i.IdentityUserId == currentUserId)) return RedirectToAction("AccessDenied", "Account");
            }

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(domain))
            {
                var instructorIds = selectedInstructors ?? new List<int>();
                string? imageUrl = null;
                if (imageFile != null) imageUrl = await ProcessUploadedFile(imageFile);

                await _instructorService.UpdateCourseDetailsAsync(id, title, domain, description, instructorIds, imageUrl, enrollmentStartDate, enrollmentDeadline);
                return RedirectToAction(nameof(Dashboard));
            }

            ViewBag.Domains = await _courseService.GetFilterDomainsAsync();
            ViewBag.Instructors = await _instructorService.GetAllInstructorsAsync();
            var originalCourseFallbackModel = await _courseService.GetCourseDetailsAsync(id);
            return View(originalCourseFallbackModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBulkSyllabus([FromBody] WebBulkSyllabusRequest request)
        {
            if (request == null || request.CourseId <= 0) return BadRequest("Invalid payload format mapping request.");

            if (!User.IsInRole("Admin"))
            {
                var courseCheck = await _courseService.GetCourseDetailsAsync(request.CourseId);
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (courseCheck == null || !courseCheck.Instructors.Any(i => i.IdentityUserId == currentUserId)) return Forbid();
            }

            var applicationMaterialsList = request.Materials.Select(m => new LmsProject.Application.DTOs.SyllabusItemDto
            {
                Title = m.Title,
                YouTubeLink = m.YouTubeLink,
                Position = m.Position
            }).ToList();

            await _instructorService.SaveBulkSyllabusAsync(request.CourseId, applicationMaterialsList);
            return Ok();
        }

        [HttpGet]
        public IActionResult AddQuiz(int courseId)
        {
            var model = new CreateQuizViewModel { CourseId = courseId };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuiz(CreateQuizViewModel model)
        {
            if (model.Questions == null || !model.Questions.Any())
            {
                ModelState.AddModelError(string.Empty, "A quiz must contain at least one question.");
                return View(model);
            }

            var quiz = new Quiz
            {
                CourseId = model.CourseId,
                Title = model.Title,
                Description = model.Description,
                TimeLimitMinutes = model.TimeLimitMinutes,
                AvailableFrom = model.AvailableFrom,
                AvailableTo = model.AvailableTo
            };

            foreach (var q in model.Questions)
            {
                quiz.Questions.Add(new Question { Text = q.Text, OptionA = q.OptionA, OptionB = q.OptionB, OptionC = q.OptionC, OptionD = q.OptionD, CorrectOption = q.CorrectOption, MarkValue = q.MarkValue });
            }

            await _quizService.CreateQuizAsync(quiz);
            return RedirectToAction(nameof(Dashboard));
        }

        // NEW FEATURE: Attendance GET
        [HttpGet]
        public async Task<IActionResult> Attendance(int courseId, DateTime? date)
        {
            // If no date is provided by the URL, default to today's date
            var targetDate = date ?? DateTime.UtcNow.Date;

            var model = await _attendanceService.GetAttendanceSessionAsync(courseId, targetDate);
            if (model == null) return NotFound();

            return View(model);
        }

        // NEW FEATURE: Attendance POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAttendance(int courseId, DateTime selectedDate, List<int> presentUserIds)
        {
            // Ensure we pass an empty list instead of null if absolutely no one is marked present
            await _attendanceService.SaveAttendanceAsync(courseId, selectedDate, presentUserIds ?? new List<int>());

            // Redirect back to the same date view so they can see their saved work instantly
            return RedirectToAction(nameof(Attendance), new { courseId = courseId, date = selectedDate.ToString("yyyy-MM-dd") });
        }
    }

    public class WebBulkSyllabusRequest
    {
        public int CourseId { get; set; }
        public List<WebSyllabusItem> Materials { get; set; } = new List<WebSyllabusItem>();
    }

    public class WebSyllabusItem
    {
        public string Title { get; set; } = string.Empty;
        public string YouTubeLink { get; set; } = string.Empty;
        public int Position { get; set; }
    }
}