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

        // Many-to-Many relationship directly with Instructors
        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

        // Relationship to Materials through our custom join entity
        public ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
    }
}