using EMS.DataAccess.Data;
using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataAccess.Repositories.Implementation
{
    internal sealed class UniversityRepository : RepositoryBase<University>, IUniveristyRepository
    {
        public UniversityRepository(AppDbContext context)
            : base(context) { }

        public void CreateUniversity(University university)
            => Create(university);

        public void DeleteUniversity(University university)
            => Delete(university);

        public async Task<IEnumerable<University>> GetUniversitiesAsync(bool trackChanges, string[]? includes = null)
            => await GetAll(trackChanges, includes).OrderBy(u => u.Name).ToListAsync();

        public async Task<University?> GetUniversityAsync(Guid universityId, bool trackChanges, string[]? includes = null)
            => await GetByCondition(u => u.Id == universityId, trackChanges, includes).SingleOrDefaultAsync();
    

    }
}
