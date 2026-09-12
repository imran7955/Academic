using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LmsProject.Domain.Entities;

namespace LmsProject.Infrastructure.Persistence
{
    // FIXED: Inherit from IdentityDbContext<IdentityUser> to support secure identity tables
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
            // CRITICAL: Keeps Identity tables configurations initialized cleanly
            base.OnModelCreating(modelBuilder);

            // Configure Many-to-Many Join Table for Course and Material with Position tracking
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

            // Configure Implicit Many-to-Many relationship between Course and Instructor
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors)
                .WithMany(i => i.Courses);
        }
    }
}