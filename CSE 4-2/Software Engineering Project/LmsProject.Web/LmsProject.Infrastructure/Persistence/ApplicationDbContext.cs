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

        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<QuizAttempt> QuizAttempts { get; set; } = null!;

        // NEW FEATURE: Attendance
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; } = null!;

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

            modelBuilder.Entity<Course>()
                .HasMany(c => c.EnrolledUsers)
                .WithMany(u => u.EnrolledCourses);

            // Configure Quiz Relationships
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Course)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(q => q.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.Quiz)
                .WithMany(q => q.QuizAttempts)
                .HasForeignKey(qa => qa.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Attendance Relationships
            modelBuilder.Entity<AttendanceRecord>()
                .HasOne(a => a.Course)
                .WithMany(c => c.AttendanceRecords)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttendanceRecord>()
                .HasOne(a => a.UserProfile)
                .WithMany(u => u.AttendanceRecords)
                .HasForeignKey(a => a.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}