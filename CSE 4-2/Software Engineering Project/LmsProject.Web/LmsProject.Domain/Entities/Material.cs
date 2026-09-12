using System.Collections.Generic;

namespace LmsProject.Domain.Entities
{
    public class Material
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string YouTubeLink { get; set; } = string.Empty;

        // Navigation property back to the join table
        public ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
    }
}