using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Application.DTOs;
using LmsProject.Domain.Entities;

namespace LmsProject.Application.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<Instructor>> GetAllInstructorsAsync();

        Task RegisterInstructorAsync(string name, string identityUserId);

        Task RegisterCourseAsync(string title, string domain, string description, List<int> instructorIds, string? imageUrl, DateTime enrollmentStartDate, DateTime enrollmentDeadline);

        Task UpdateCourseDetailsAsync(int id, string title, string domain, string description, List<int> instructorIds, string? imageUrl, DateTime enrollmentStartDate, DateTime enrollmentDeadline);

        Task SaveBulkSyllabusAsync(int courseId, List<SyllabusItemDto> materialsDto);

        Task CreateUserProfileAsync(string identityUserId, string fullName, string emailAddress, string accountType);
    }
}