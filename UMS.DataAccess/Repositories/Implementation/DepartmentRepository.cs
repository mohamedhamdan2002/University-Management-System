using UMS.DataAccess.Data;
using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace UMS.DataAccess.Repositories.Implementation
{
    internal sealed class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository 
    {
        public DepartmentRepository(AppDbContext context) 
            : base(context) { }
        public void CreateDepartmentForGroup(Guid groupId, Department department)
        {
            department.GroupId = groupId;
            Create(department);
        }

        public void DeleteDepartment(Department department)
            => Delete(department);
        public async Task<Department?> GetDepartmentAsync(Guid groupId, Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(d => d.GroupId == groupId && d.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();

        public async Task<Department?> GetDepartmentAsync(Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(d => d.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Department>> GetDepartmentsAsync(Guid groupId, bool trackChanges)
            => await GetByCondition(d => d.GroupId == groupId, trackChanges)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }
}
