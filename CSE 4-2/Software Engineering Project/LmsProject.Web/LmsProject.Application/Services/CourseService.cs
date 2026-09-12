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

        public async Task<IEnumerable<Course>> GetFilteredCoursesAsync(string domain, string instructor, string search)
        {
            var courses = await _courseRepository.GetAllCoursesWithDetailsAsync();

            // 1. Filter by Domain Selection
            if (!string.IsNullOrEmpty(domain) && domain != "All")
            {
                courses = courses.Where(c => c.Domain.Equals(domain, StringComparison.OrdinalIgnoreCase));
            }

            // 2. Filter by explicit Instructor Dropdown Selection
            if (!string.IsNullOrEmpty(instructor) && instructor != "All")
            {
                courses = courses.Where(c => c.Instructors.Any(i => i.Name.Equals(instructor, StringComparison.OrdinalIgnoreCase)));
            }

            // 3. Filter by live Search input string matching title or domain names
            if (!string.IsNullOrEmpty(search))
            {
                courses = courses.Where(c => c.Title.Contains(search, StringComparison.OrdinalIgnoreCase)
                                          || c.Domain.Contains(search, StringComparison.OrdinalIgnoreCase));
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
    }
}