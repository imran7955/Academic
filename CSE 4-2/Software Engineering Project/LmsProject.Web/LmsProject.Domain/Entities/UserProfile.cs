using System;
using System.Collections.Generic;

namespace LmsProject.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

        // NEW FEATURE: Enrolled Courses Relationship
        public ICollection<Course> EnrolledCourses { get; set; } = new List<Course>();
    }
}