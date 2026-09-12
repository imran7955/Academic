using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;

namespace LmsProject.Application.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetFilteredCoursesAsync(string domain, string instructor, string search);
        Task<Course?> GetCourseDetailsAsync(int id);
        Task<IEnumerable<string>> GetFilterDomainsAsync();
    }
}