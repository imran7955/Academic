using System.Collections.Generic;

namespace LmsProject.Domain.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string IdentityUserId { get; set; } = string.Empty;

        // Navigation property for the many-to-many relationship with Courses
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}