using EMS.DataAccess.Data;
using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataAccess.Repositories.Implementation
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext context)
            : base(context) { }
        public void CreateGroupForFaculty(Guid facultyId, Group group)
        {
            group.FacultyId = facultyId;
            Create(group);
        }


        public void DeleteGroup(Group group)
            => Delete(group);

        public async Task<Group?> GetGroupAsync(Guid facultyId, Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(g => g.FacultyId == facultyId && g.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();
        public async Task<Group?> GetGroupAsync(Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(g => g.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();
        public async Task<IEnumerable<Group>> GetGroupsAsync(Guid facultyId, bool trackChanges)
            => await GetByCondition(g => g.FacultyId == facultyId, trackChanges)
            .OrderBy(g => g.Name)
            .ToListAsync();

    }
}
