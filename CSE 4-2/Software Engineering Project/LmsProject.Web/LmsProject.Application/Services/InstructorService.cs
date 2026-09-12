using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmsProject.Application.DTOs;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;

namespace LmsProject.Application.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ISyllabusRepository _syllabusRepository;

        public InstructorService(
            ICourseRepository courseRepository,
            IInstructorRepository instructorRepository,
            ISyllabusRepository syllabusRepository)
        {
            _courseRepository = courseRepository;
            _instructorRepository = instructorRepository;
            _syllabusRepository = syllabusRepository;
        }

        public async Task<IEnumerable<Instructor>> GetAllInstructorsAsync()
        {
            return await _instructorRepository.GetAllInstructorsAsync();
        }

        public async Task RegisterInstructorAsync(string name, string identityUserId)
        {
            var instructor = new Instructor
            {
                Name = name,
                IdentityUserId = identityUserId
            };
            await _instructorRepository.AddInstructorAsync(instructor);
        }

        // ADDED: string? imageUrl to signature and mapped it to Course
        public async Task RegisterCourseAsync(string title, string domain, string description, List<int> instructorIds, string? imageUrl)
        {
            var course = new Course
            {
                Title = title,
                Domain = domain,
                Description = description,
                ImageUrl = imageUrl // MAP NEW FIELD
            };

            if (instructorIds != null)
            {
                foreach (var id in instructorIds)
                {
                    var instructor = await _instructorRepository.GetInstructorByIdAsync(id);
                    if (instructor != null)
                    {
                        course.Instructors.Add(instructor);
                    }
                }
            }

            await _courseRepository.AddCourseAsync(course);
        }

        // ADDED: string? imageUrl to signature and mapped it to Course
        public async Task UpdateCourseDetailsAsync(int id, string title, string domain, string description, List<int> instructorIds, string? imageUrl)
        {
            var existingCourse = await _courseRepository.GetCourseByIdWithDetailsAsync(id);
            if (existingCourse == null) return;

            existingCourse.Title = title;
            existingCourse.Domain = domain;
            existingCourse.Description = description;

            // Only update the image if a new one was actually uploaded
            if (imageUrl != null)
            {
                existingCourse.ImageUrl = imageUrl;
            }

            existingCourse.Instructors.Clear();

            if (instructorIds != null)
            {
                foreach (var instId in instructorIds)
                {
                    var instructor = await _instructorRepository.GetInstructorByIdAsync(instId);
                    if (instructor != null)
                    {
                        existingCourse.Instructors.Add(instructor);
                    }
                }
            }

            await _courseRepository.UpdateCourseAsync(existingCourse);
        }

        public async Task SaveBulkSyllabusAsync(int courseId, List<SyllabusItemDto> materialsDto)
        {
            var course = await _courseRepository.GetCourseByIdWithDetailsAsync(courseId);
            if (course == null) return;

            if (course.CourseMaterials != null && course.CourseMaterials.Any())
            {
                var materialsToDelete = course.CourseMaterials.Select(cm => cm.Material).ToList();
                await _syllabusRepository.RemoveCourseMaterialsRangeAsync(course.CourseMaterials);

                foreach (var oldMat in materialsToDelete)
                {
                    await _syllabusRepository.RemoveMaterialAsync(oldMat);
                }
            }

            if (materialsDto != null)
            {
                foreach (var item in materialsDto)
                {
                    var material = new Material
                    {
                        Title = item.Title,
                        YouTubeLink = item.YouTubeLink
                    };

                    await _syllabusRepository.AddMaterialAsync(material);

                    var courseMaterialLink = new CourseMaterial
                    {
                        CourseId = courseId,
                        MaterialId = material.Id,
                        Position = item.Position
                    };

                    await _syllabusRepository.AddCourseMaterialLinkAsync(courseMaterialLink);
                }
            }
        }

        public async Task CreateUserProfileAsync(string identityUserId, string fullName, string emailAddress, string accountType)
        {
            var customProfile = new UserProfile
            {
                IdentityUserId = identityUserId,
                FullName = fullName,
                EmailAddress = emailAddress,
                AccountType = accountType,
                RegisteredOn = DateTime.UtcNow
            };

            await _instructorRepository.AddUserProfileAsync(customProfile);
        }
    }
}