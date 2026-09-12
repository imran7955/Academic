using System.Collections.Generic;
using System.Threading.Tasks;
using LmsProject.Domain.Entities;

namespace LmsProject.Domain.Repositories
{
    public interface ISyllabusRepository
    {
        Task<CourseMaterial?> GetCourseMaterialRowAsync(int courseId, int materialId);
        Task AddMaterialAsync(Material material);
        Task AddCourseMaterialLinkAsync(CourseMaterial courseMaterial);
        Task RemoveCourseMaterialLinkAsync(CourseMaterial courseMaterial);
        Task RemoveMaterialAsync(Material material);
        Task RemoveCourseMaterialsRangeAsync(IEnumerable<CourseMaterial> courseMaterials);
    }
}