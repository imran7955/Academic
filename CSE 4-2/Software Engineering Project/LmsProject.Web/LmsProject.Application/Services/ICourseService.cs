using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;

namespace LmsProject.Application.Services
{
    public interface ICourseService
    {
        // ADDED: enrolledStatus and currentIdentityUserId for filtering
        Task<IEnumerable<Course>> GetFilteredCoursesAsync(string enrolledStatus, string domain, string instructor, string search, string? currentIdentityUserId);
        Task<Course?> GetCourseDetailsAsync(int id);
        Task<IEnumerable<string>> GetFilterDomainsAsync();

        // ADDED: Enrollment action
        Task EnrollUserAsync(int courseId, string identityUserId);
    }
}