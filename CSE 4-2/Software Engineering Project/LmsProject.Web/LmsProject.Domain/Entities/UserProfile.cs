using System;

namespace LmsProject.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        // This holds the matching long text GUID string from AspNetUsers
        public string IdentityUserId { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty; // Admin, Instructor, Student
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;
    }
}