using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;

namespace LmsProject.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> GetFilteredCoursesAsync(string enrolledStatus, string domain, string instructor, string search, string? currentIdentityUserId)
        {
            var courses = await _courseRepository.GetAllCoursesWithDetailsAsync();

            // 1. Filter by Enrollment Status
            if (!string.IsNullOrEmpty(enrolledStatus) && enrolledStatus != "N/A" && !string.IsNullOrEmpty(currentIdentityUserId))
            {
                if (enrolledStatus == "Enrolled")
                {
                    courses = courses.Where(c => c.EnrolledUsers.Any(u => u.IdentityUserId == currentIdentityUserId));
                }
                else if (enrolledStatus == "Not Enrolled")
                {
                    courses = courses.Where(c => !c.EnrolledUsers.Any(u => u.IdentityUserId == currentIdentityUserId));
                }
            }

            // 2. Filter by Domain Selection
            if (!string.IsNullOrEmpty(domain) && domain != "All")
            {
                courses = courses.Where(c => c.Domain.Equals(domain, StringComparison.OrdinalIgnoreCase));
            }

            // 3. Filter by Instructor Dropdown
            if (!string.IsNullOrEmpty(instructor) && instructor != "All")
            {
                courses = courses.Where(c => c.Instructors.Any(i => i.Name.Equals(instructor, StringComparison.OrdinalIgnoreCase)));
            }

            // 4. Filter by live Search input (MODIFIED: Now searches Title only)
            if (!string.IsNullOrEmpty(search))
            {
                courses = courses.Where(c => c.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return courses.ToList();
        }

        public async Task<Course?> GetCourseDetailsAsync(int id)
        {
            return await _courseRepository.GetCourseByIdWithDetailsAsync(id);
        }

        public async Task<IEnumerable<string>> GetFilterDomainsAsync()
        {
            return await _courseRepository.GetDistinctDomainsAsync();
        }

        public async Task EnrollUserAsync(int courseId, string identityUserId)
        {
            await _courseRepository.EnrollUserAsync(courseId, identityUserId);
        }
    }
}