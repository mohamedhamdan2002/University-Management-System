using UMS.DataAccess.Data;
using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace UMS.DataAccess.Repositories.Implementation
{
    internal sealed class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        public StaffRepository(AppDbContext context)
            : base(context) { }

        public void CreateStaff(Staff staff)
            => Create(staff);

        public void DeleteStaff(Staff staff)
            => Delete(staff);
        public async Task<Staff?> GetStaffAsync(Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(d => d.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();
        public async Task<IEnumerable<Staff>> GetStaffsAsync(bool trackChanges)
            => await GetAll(trackChanges).OrderBy(d => d.FirstName)
            .ToListAsync();
    }
}
