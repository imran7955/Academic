using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmsProject.Application.DTOs;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;

namespace LmsProject.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<AttendanceSessionDto?> GetAttendanceSessionAsync(int courseId, DateTime date)
        {
            var course = await _attendanceRepository.GetCourseWithEnrolledUsersAsync(courseId);
            if (course == null) return null;

            var allRecords = (await _attendanceRepository.GetAttendanceRecordsForCourseAsync(courseId)).ToList();

            // Calculate how many unique dates attendance was taken for to get the denominator
            var totalClasses = allRecords.Select(r => r.Date.Date).Distinct().Count();

            // Isolate today's records if they exist
            var targetDateRecords = allRecords.Where(r => r.Date.Date == date.Date).ToList();

            var session = new AttendanceSessionDto
            {
                CourseId = course.Id,
                CourseTitle = course.Title,
                SelectedDate = date.Date,
                TotalClasses = totalClasses
            };

            foreach (var student in course.EnrolledUsers)
            {
                // Calculate historical attendance for this student
                var studentTotalPresent = allRecords.Count(r => r.UserProfileId == student.Id && r.IsPresent);

                // Determine if they were checked as present on the specific date we are viewing
                var isPresentForTargetDate = targetDateRecords.Any(r => r.UserProfileId == student.Id && r.IsPresent);

                session.Students.Add(new StudentAttendanceDto
                {
                    UserProfileId = student.Id,
                    FullName = student.FullName,
                    ClassesAttended = studentTotalPresent,
                    IsPresent = isPresentForTargetDate
                });
            }

            return session;
        }

        public async Task SaveAttendanceAsync(int courseId, DateTime date, List<int> presentUserProfileIds)
        {
            var course = await _attendanceRepository.GetCourseWithEnrolledUsersAsync(courseId);
            if (course == null) return;

            var newRecords = new List<AttendanceRecord>();

            foreach (var student in course.EnrolledUsers)
            {
                newRecords.Add(new AttendanceRecord
                {
                    CourseId = courseId,
                    UserProfileId = student.Id,
                    Date = date.Date,
                    // If the student's ID is in the submitted list, they are present. Otherwise, absent.
                    IsPresent = presentUserProfileIds != null && presentUserProfileIds.Contains(student.Id)
                });
            }

            await _attendanceRepository.SaveDailyAttendanceAsync(courseId, date.Date, newRecords);
        }
    }
}