using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;

namespace LmsProject.Domain.Repositories
{
    public interface IAttendanceRepository
    {
        Task<Course?> GetCourseWithEnrolledUsersAsync(int courseId);
        Task<IEnumerable<AttendanceRecord>> GetAttendanceRecordsForCourseAsync(int courseId);
        Task SaveDailyAttendanceAsync(int courseId, DateTime date, List<AttendanceRecord> records);
    }
}