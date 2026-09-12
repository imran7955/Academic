using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;
using LmsProject.Infrastructure.Persistence;

namespace LmsProject.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesWithDetailsAsync()
        {
            return await _context.Courses
                .Include(c => c.Instructors)
                .Include(c => c.CourseMaterials)
                    .ThenInclude(cm => cm.Material)
                .Include(c => c.EnrolledUsers) // ADDED: Load enrolled users
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdWithDetailsAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Instructors)
                .Include(c => c.CourseMaterials)
                    .ThenInclude(cm => cm.Material)
                .Include(c => c.EnrolledUsers) // ADDED: Load enrolled users
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetDistinctDomainsAsync()
        {
            return await _context.Courses
                .Select(c => c.Domain)
                .Distinct()
                .ToListAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        // NEW FEATURE: Handles database link between UserProfile and Course
        public async Task EnrollUserAsync(int courseId, string identityUserId)
        {
            var course = await _context.Courses
                .Include(c => c.EnrolledUsers)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(u => u.IdentityUserId == identityUserId);

            if (course != null && userProfile != null && !course.EnrolledUsers.Any(u => u.Id == userProfile.Id))
            {
                course.EnrolledUsers.Add(userProfile);
                await _context.SaveChangesAsync();
            }
        }
    }
}