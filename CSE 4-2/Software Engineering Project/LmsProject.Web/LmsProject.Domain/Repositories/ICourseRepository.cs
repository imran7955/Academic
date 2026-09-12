using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;

namespace LmsProject.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCoursesWithDetailsAsync();
        Task<Course?> GetCourseByIdWithDetailsAsync(int id);
        Task AddCourseAsync(Course course);
        Task<IEnumerable<string>> GetDistinctDomainsAsync();
        Task UpdateCourseAsync(Course course);

        // NEW FEATURE: Enrollment handling
        Task EnrollUserAsync(int courseId, string identityUserId);
    }
}