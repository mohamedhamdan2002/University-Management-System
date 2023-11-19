using UMS.DataAccess.Data;
using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace UMS.DataAccess.Repositories.Implementation
{
    internal sealed class DivisionRepository : RepositoryBase<Division>, IDivisionRepository
    {
        public DivisionRepository(AppDbContext context) 
            : base(context) {}
        public void CreateDivisionForDepartment(Guid departmentId, Division division)
        {
            Create(division);
            division.DepartmentDivisions = new List<DepartmentDivision>() { new DepartmentDivision { DepartmentId = departmentId } };
        }

        public void DeleteDivision(Division division)
            => Delete(division);

        public async Task<Division?> GetDivisionAsync(Guid departmentId, Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(div => div.DepartmentDivisions.Any(d => d.DepartmentId == departmentId) && div.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();

        public async Task<Division?> GetDivisionAsync(Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(div => div.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Division>> GetDivisionsAsync(Guid departmentId, bool trackChanges)
            => await GetByCondition(div => div.DepartmentDivisions.Any(d => d.DepartmentId == departmentId), trackChanges)
            .OrderBy(div => div.Name)
            .ToListAsync();
    }
}
