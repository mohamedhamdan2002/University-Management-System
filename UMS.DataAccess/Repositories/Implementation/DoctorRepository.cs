using UMS.DataAccess.Data;
using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace UMS.DataAccess.Repositories.Implementation
{
    internal sealed class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext context)
            : base(context) { }

        public void CreateDoctor(Doctor doctor)
            => Create(doctor);

        public void DeleteDoctor(Doctor doctor)
            => Delete(doctor);
        public async Task<Doctor?> GetDocotrAsync(Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(d => d.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();
        public async Task<IEnumerable<Doctor>> GetDoctorsAsync(bool trackChanges)
            => await GetAll(trackChanges).OrderBy(d => d.FirstName)
            .ToListAsync();
    }
}
