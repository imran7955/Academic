using System;

namespace LmsProject.Domain.Entities
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public int CourseId { get; set; }

        // We link directly to UserProfile to easily grab their FullName later
        public int UserProfileId { get; set; }

        // We will store just the Date (Time stripped out) to group classes easily
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }

        public Course Course { get; set; } = null!;
        public UserProfile UserProfile { get; set; } = null!;
    }
}