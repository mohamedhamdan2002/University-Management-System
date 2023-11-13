using EMS.DataAccess.Entities.Models;

namespace EMS.DataAccess.Repositories.Contracts
{
    public interface IDivisionRepository
    {
        Task<IEnumerable<Division>> GetDivisionsAsync(Guid departmentId, bool trackChanges);
        Task<Division?> GetDivisionAsync(Guid departmentId, Guid id, bool trackChanges, string[]? includes = null);
        Task<Division?> GetDivisionAsync(Guid id, bool trackChanges, string[]? includes = null);

        void CreateDivisionForDepartment(Guid departmentId, Division division);
        void DeleteDivision(Division division);
    }
}
