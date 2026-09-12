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

        // NEW FEATURE: Enrollment Dates
        public DateTime EnrollmentStartDate { get; set; }
        public DateTime EnrollmentDeadline { get; set; }

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();

        // NEW FEATURE: Enrolled Users Relationship
        public ICollection<UserProfile> EnrolledUsers { get; set; } = new List<UserProfile>();
    }
}