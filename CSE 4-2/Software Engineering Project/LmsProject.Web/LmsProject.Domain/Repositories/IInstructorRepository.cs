using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;

namespace LmsProject.Domain.Repositories
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
        Task<Instructor?> GetInstructorByIdAsync(int id);
        Task AddInstructorAsync(Instructor instructor);

        // FIXED: Added missing method contract signature to clear the service compiler error
        Task AddUserProfileAsync(UserProfile profile);
    }
}