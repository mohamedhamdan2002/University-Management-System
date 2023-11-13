using EMS.DataAccess.Entities.Models;

namespace EMS.DataAccess.Repositories.Contracts
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetStaffsAsync(bool trackChanges);
        Task<Staff?> GetStaffAsync(Guid id, bool trackChanges, string[]? includes = null);
        void CreateStaff(Staff staff);
        void DeleteStaff(Staff staff);
    }
}
