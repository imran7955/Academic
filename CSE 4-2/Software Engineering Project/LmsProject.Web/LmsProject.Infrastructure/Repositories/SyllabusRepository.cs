using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LmsProject.Domain.Entities;
using LmsProject.Domain.Repositories;
using LmsProject.Infrastructure.Persistence;

namespace LmsProject.Infrastructure.Repositories
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly ApplicationDbContext _context;

        public SyllabusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CourseMaterial?> GetCourseMaterialRowAsync(int courseId, int materialId)
        {
            return await _context.CourseMaterials
                .FirstOrDefaultAsync(cm => cm.CourseId == courseId && cm.MaterialId == materialId);
        }

        public async Task AddMaterialAsync(Material material)
        {
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
        }

        public async Task AddCourseMaterialLinkAsync(CourseMaterial courseMaterial)
        {
            await _context.CourseMaterials.AddAsync(courseMaterial);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCourseMaterialLinkAsync(CourseMaterial courseMaterial)
        {
            _context.CourseMaterials.Remove(courseMaterial);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMaterialAsync(Material material)
        {
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCourseMaterialsRangeAsync(IEnumerable<CourseMaterial> courseMaterials)
        {
            _context.CourseMaterials.RemoveRange(courseMaterials);
            await _context.SaveChangesAsync();
        }
    }
}