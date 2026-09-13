using System;
using System.Collections.Generic;

namespace LmsProject.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime EnrollmentStartDate { get; set; }
        public DateTime EnrollmentDeadline { get; set; }

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
        public ICollection<UserProfile> EnrolledUsers { get; set; } = new List<UserProfile>();

        // NEW FEATURE: Quizzes assigned to this course
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

        public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    }
}