using System;
using System.Collections.Generic;

namespace LmsProject.Application.DTOs
{
    public class AttendanceSessionDto
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public DateTime SelectedDate { get; set; }
        public int TotalClasses { get; set; }
        public List<StudentAttendanceDto> Students { get; set; } = new List<StudentAttendanceDto>();
    }

    public class StudentAttendanceDto
    {
        public int UserProfileId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int ClassesAttended { get; set; }
        public bool IsPresent { get; set; }
    }
}