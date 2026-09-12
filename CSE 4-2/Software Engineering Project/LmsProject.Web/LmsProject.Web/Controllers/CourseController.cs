using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LmsProject.Application.Services;
using LmsProject.Web.Models;

namespace LmsProject.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;

        // Dependency Injection pulls in our clean application logic layer
        public CourseController(ICourseService courseService, IInstructorService instructorService)
        {
            _courseService = courseService;
            _instructorService = instructorService;
        }

        // GET: Course/ViewCourses
        public async Task<IActionResult> ViewCourses(string domain, string instructor, string search)
        {
            // Normalize default parameters to match UI state expectations
            var chosenDomain = string.IsNullOrEmpty(domain) ? "All" : domain;
            var chosenInstructor = string.IsNullOrEmpty(instructor) ? "All" : instructor;
            var searchString = search ?? "";

            // Fetch clean data from core application layer services
            var courses = await _courseService.GetFilteredCoursesAsync(chosenDomain, chosenInstructor, searchString);
            var domainsList = await _courseService.GetFilterDomainsAsync();
            var instructorsList = await _instructorService.GetAllInstructorsAsync();

            // Explicitly maintain and bind state properties requested by ViewCourses.cshtml layout
            var model = new CourseViewModel
            {
                Courses = courses.ToList(),
                Domains = domainsList.ToList(),
                Instructors = instructorsList.ToList(),
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
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }
    }
}