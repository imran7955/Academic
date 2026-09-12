using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LmsProject.Application.Services;
using LmsProject.Web.Models;
using System;

namespace LmsProject.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;

        public CourseController(ICourseService courseService, IInstructorService instructorService)
        {
            _courseService = courseService;
            _instructorService = instructorService;
        }

        // GET: Course/ViewCourses
        public async Task<IActionResult> ViewCourses(string enrolled, string domain, string instructor, string search)
        {
            var chosenEnrolled = string.IsNullOrEmpty(enrolled) ? "N/A" : enrolled;
            var chosenDomain = string.IsNullOrEmpty(domain) ? "All" : domain;
            var chosenInstructor = string.IsNullOrEmpty(instructor) ? "All" : instructor;
            var searchString = search ?? "";

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var courses = await _courseService.GetFilteredCoursesAsync(chosenEnrolled, chosenDomain, chosenInstructor, searchString, currentUserId);
            var domainsList = await _courseService.GetFilterDomainsAsync();
            var instructorsList = await _instructorService.GetAllInstructorsAsync();

            var model = new CourseViewModel
            {
                Courses = courses.ToList(),
                Domains = domainsList.ToList(),
                Instructors = instructorsList.ToList(),
                SelectedEnrollment = chosenEnrolled,
                SelectedDomain = chosenDomain,
                SelectedInstructor = chosenInstructor,
                SearchTerm = searchString
            };

            return View(model);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseService.GetCourseDetailsAsync(id);
            if (course == null) return NotFound();

            return View(course);
        }

        // POST: Course/Enroll/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null) return RedirectToAction("Login", "Account"); // Redirects guest

            var course = await _courseService.GetCourseDetailsAsync(id);
            if (course == null) return NotFound();

            if (DateTime.UtcNow < course.EnrollmentStartDate || DateTime.UtcNow > course.EnrollmentDeadline)
            {
                return BadRequest("Enrollment is closed for this course.");
            }

            await _courseService.EnrollUserAsync(id, currentUserId);

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}