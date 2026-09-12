using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LmsProject.Domain.Entities;

namespace LmsProject.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Instructor> Instructors { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;
        public DbSet<CourseMaterial> CourseMaterials { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseMaterial>()
                .HasKey(cm => new { cm.CourseId, cm.MaterialId });

            modelBuilder.Entity<CourseMaterial>()
                .HasOne(cm => cm.Course)
                .WithMany(c => c.CourseMaterials)
                .HasForeignKey(cm => cm.CourseId);

            modelBuilder.Entity<CourseMaterial>()
                .HasOne(cm => cm.Material)
                .WithMany(m => m.CourseMaterials)
                .HasForeignKey(cm => cm.MaterialId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors)
                .WithMany(i => i.Courses);

            // NEW: Configure Many-to-Many relationship between Course and UserProfile
            modelBuilder.Entity<Course>()
                .HasMany(c => c.EnrolledUsers)
                .WithMany(u => u.EnrolledCourses);
        }
    }
}