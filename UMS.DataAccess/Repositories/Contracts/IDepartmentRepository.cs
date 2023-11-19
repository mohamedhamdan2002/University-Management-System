using UMS.DataAccess.Entities.Models;

namespace UMS.DataAccess.Repositories.Contracts
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync(Guid groupId, bool trackChanges);
        Task<Department?> GetDepartmentAsync(Guid groupId, Guid id, bool trackChanges, string[]? includes = null);
        Task<Department?> GetDepartmentAsync(Guid id, bool trackChanges, string[]? includes = null);

        void CreateDepartmentForGroup(Guid groupId, Department department);
        void DeleteDepartment(Department department);
    }
}
