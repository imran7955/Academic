using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;
using LmsProject.Infrastructure.Persistence; // Adjust this namespace if your DbContext is in a "Data" folder

namespace LmsProject.Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext _context;

        public InstructorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieves all academic instructor rows from the database
        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            return await _context.Instructors.ToListAsync();
        }

        // Finds a specific instructor tracking record by its integer primary key
        public async Task<Instructor?> GetInstructorByIdAsync(int id)
        {
            return await _context.Instructors.FindAsync(id);
        }

        // Adds a brand new Instructor entity row into the database
        public async Task AddInstructorAsync(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();
        }

        // FIXED: Implemented to satisfy the updated IInstructorRepository interface contract.
        // This persists the synchronized user profile data directly into the database.
        public async Task AddUserProfileAsync(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();
        }
    }
}