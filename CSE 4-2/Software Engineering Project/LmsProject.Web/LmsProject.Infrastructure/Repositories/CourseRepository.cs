using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;
using LmsProject.Infrastructure.Persistence; // Points directly to ApplicationDbContext

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
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdWithDetailsAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Instructors)
                .Include(c => c.CourseMaterials)
                    .ThenInclude(cm => cm.Material)
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

        // The working explicit persistence update method
        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}