using EMS.DataAccess.Entities.Models;

namespace EMS.DataAccess.Repositories.Contracts
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroupsAsync(Guid facultyId, bool trackChanges);
        Task<Group?> GetGroupAsync(Guid facultyId, Guid id, bool trackChanges, string[]? includes = null);
        Task<Group?> GetGroupAsync(Guid id, bool trackChanges, string[]? includes = null);
        void CreateGroupForFaculty(Guid facultyId, Group group);
        void DeleteGroup(Group group);
    }
}
