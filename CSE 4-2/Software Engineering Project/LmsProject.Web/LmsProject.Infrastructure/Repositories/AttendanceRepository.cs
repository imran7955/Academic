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
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course?> GetCourseWithEnrolledUsersAsync(int courseId)
        {
            return await _context.Courses
                .Include(c => c.EnrolledUsers)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<IEnumerable<AttendanceRecord>> GetAttendanceRecordsForCourseAsync(int courseId)
        {
            return await _context.AttendanceRecords
                .Where(a => a.CourseId == courseId)
                .ToListAsync();
        }

        public async Task SaveDailyAttendanceAsync(int courseId, DateTime date, List<AttendanceRecord> records)
        {
            // Find if there are already records for this exact date to overwrite them
            var existingRecords = await _context.AttendanceRecords
                .Where(a => a.CourseId == courseId && a.Date.Date == date.Date)
                .ToListAsync();

            _context.AttendanceRecords.RemoveRange(existingRecords);
            await _context.AttendanceRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();
        }
    }
}