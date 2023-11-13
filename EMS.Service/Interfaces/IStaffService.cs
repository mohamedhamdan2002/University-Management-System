using EMS.Service.ViewModels.Staff;
namespace EMS.Services.Contracts
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffViewModel>> GetStaffsAsync(bool trackChanges);
        Task<StaffViewModel> GetStaffAsync(Guid id, bool trackChanges, string[]? includes = null);
        Task<StaffViewModel> CreateStaffAsync(StaffForCreationViewModel staffForCreation);
        Task UpdateStaffAsync(Guid id, StaffForUpdateViewModel staffForUpdate, bool trackChanges);
        Task DeleteStaffAsync(Guid id, bool trackChanges);
    }
}
