using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Application.DTOs;
using LmsProject.Domain.Entities;

namespace LmsProject.Application.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();

        // FIXED: Modified to accept the unique identity authentication user ID string tracking token
        Task RegisterInstructorAsync(string name, string identityUserId);

        Task RegisterCourseAsync(string title, string domain, string description, List<int> instructorIds, string? imageUrl);

        // NEW METHOD ADDITION FOR CORRECT TRACK UPDATES
        Task UpdateCourseDetailsAsync(int id, string title, string domain, string description, List<int> instructorIds, string? imageUrl);

        Task SaveBulkSyllabusAsync(int courseId, List<SyllabusItemDto> materialsDto);

        Task CreateUserProfileAsync(string identityUserId, string fullName, string emailAddress, string accountType);
    }
}

//using System.Collections.Generic;
//using System.Threading.Tasks;
//using LmsProject.Application.DTOs;
//using LmsProject.Domain.Entities;

//namespace LmsProject.Application.Services
//{
//    public interface IInstructorService
//    {
//        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();
//        Task RegisterInstructorAsync(string name);
//        Task RegisterCourseAsync(string title, string domain, string description, List<int> instructorIds);
//        Task SaveBulkSyllabusAsync(int courseId, List<SyllabusItemDto> materialsDto);
//    }
//}