namespace LmsProject.Domain.Entities
{
    public class CourseMaterial
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;

        // Tracks the appropriate position/sequence of the material for this specific course
        public int Position { get; set; }
    }
}