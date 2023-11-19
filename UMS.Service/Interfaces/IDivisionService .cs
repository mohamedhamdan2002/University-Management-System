
using UMS.Service.ViewModels.Division;

namespace UMS.Services.Contracts
{
    public interface IDivisionService
    {
        Task<IEnumerable<DivisionViewModel>> GetDivisionsAsync(Guid departmentId, bool trackChanges);
        Task<DivisionViewModel> GetDivisionAsync(Guid departmentId, Guid id, bool trackChanges, string[]? includes = null);
        Task<DivisionViewModel> CreateDivisionForDepartmentAsync(Guid departmentId, DivisionForCreationViewModel division, bool trackChanges);
        Task DeleteDivisionForDepartmentAsync(Guid departmentId, Guid id, bool trackChanges);
        Task UpdateDivisionForDepartmentAsync(Guid departmentId, Guid id, DivisionForUpdateViewModel divison, bool departmentTrackChanges, bool divisionTrackChanges);
    }
}
