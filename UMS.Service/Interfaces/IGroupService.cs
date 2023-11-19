using UMS.Service.ViewModels.Group;
namespace UMS.Services.Contracts
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupViewModel>> GetGroupsAsync(Guid facultyId, bool trackChanges);
        Task<GroupViewModel> GetGroupAsync(Guid facultyId, Guid id, bool trackChanges, string[]? includes = null);
        Task<GroupViewModel> CreateGroupForFacultyAsync(Guid facultyId, GroupForCreationViewModel groupForCreation, bool trackChanges);
        Task DeleteGroupForFacultyAsync(Guid facultyId, Guid id, bool trackChanges);
        Task UpdateGroupForFacultyAsync(Guid facultyId, Guid id, GroupForUpdateViewModel groupForUpdate, bool facultyTrackChanges, bool groupTrackChanges);
    }
}
