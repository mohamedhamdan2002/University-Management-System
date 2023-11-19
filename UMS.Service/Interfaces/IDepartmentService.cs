using UMS.Service.ViewModels.Department;
namespace UMS.Services.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentViewModel>> GetDepartmentsAsync(Guid groupId, bool trackChanges);
        Task<DepartmentViewModel> GetDepartmentAsync(Guid groupId, Guid id, bool trackChanges, string[]? includes = null);
        Task<DepartmentViewModel> CreateDepartmentForGroupAsync(Guid groupId, DepartmentForCreationViewModel department, bool trackChanges);
        Task DeleteDepartmentForGroupAsync(Guid groupId, Guid id, bool trackChanges);
        Task UpdateDepartmentForGroupAsync(Guid groupId, Guid id, DepartmentForUpdateViewModel department, bool groupTrackChanges, bool departmentTrackChanges);
    }
}
