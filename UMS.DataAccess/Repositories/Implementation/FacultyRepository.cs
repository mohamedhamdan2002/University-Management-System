using UMS.DataAccess.Data;
using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace UMS.DataAccess.Repositories.Implementation
{
    internal sealed class FacultyRepository : RepositoryBase<Faculty>, IFacultyRepository
    {
        public FacultyRepository(AppDbContext context)
            : base(context) { }

        public void CreateFacultyForUniversity(Guid universityId, Faculty faculty)
        {
            faculty.UniversityId = universityId;
            Create(faculty);
        }

        public void DeleteFaculty(Faculty faculty)
            => Delete(faculty);
        public async Task<IEnumerable<Faculty>> GetFacultiesAsync(Guid universtiyId, bool trackChanges)
            => await GetByCondition(f => f.UniversityId == universtiyId, trackChanges)
                .OrderBy(f => f.Name).ToListAsync();

        public async Task<Faculty?> GetFacultyAsync(Guid universtiyId, Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(f => f.UniversityId == universtiyId && f.Id == id, trackChanges, includes)
                .SingleOrDefaultAsync();

        public async Task<Faculty?> GetFacultyAsync(Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(f => f.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();
    }
}
