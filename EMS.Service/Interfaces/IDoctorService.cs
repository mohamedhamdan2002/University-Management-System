using EMS.Service.ViewModels.Doctor;
namespace EMS.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorViewModel>> GetDoctorsAsync(bool trackChanges);
        Task<DoctorViewModel> GetDoctorAsync(Guid id, bool trackChanges, string[]? includes = null);
        Task<DoctorViewModel> CreateDoctorAsync(DoctorForCreationViewModel doctorForCreation);
        Task UpdateDoctorAsync(Guid id, DoctorForUpdateViewModel doctorForUpdate, bool trackChanges);
        Task DeleteDoctorAsync(Guid id, bool trackChanges);
    }
}
