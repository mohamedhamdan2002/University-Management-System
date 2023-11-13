using EMS.DataAccess.Entities.Models;

namespace EMS.DataAccess.Repositories.Contracts
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetDoctorsAsync(bool trackChanges);
        Task<Doctor?> GetDocotrAsync(Guid id, bool trackChanges, string[]? includes = null);
        void CreateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
    }
}
