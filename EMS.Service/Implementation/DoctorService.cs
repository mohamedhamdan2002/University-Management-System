using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using EMS.Service.ViewModels.Doctor;
using EMS.Services.Contracts;
namespace EMS.Services.Implementation
{
    internal sealed class DoctorService : IDoctorService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shardService;
        public DoctorService(IRepositoryManager repository, ShardService shardService)
        {
            _repository = repository;
            _shardService = shardService;
        }
        public async Task<DoctorViewModel> CreateDoctorAsync(DoctorForCreationViewModel doctorForCreation)
        {
            var doctorDb = new Doctor
            {
                FirstName = doctorForCreation.FirstName!,
                LastName = doctorForCreation.LastName!,
                NationalID = doctorForCreation.NationalID!,
                Address = doctorForCreation.Address!,
            };
            _repository.Doctor.CreateDoctor(doctorDb);
            await _repository.SaveAsync();
            var doctorViewModel = new DoctorViewModel(doctorDb.Id, $"{doctorDb.FirstName} {doctorDb.LastName}", doctorDb.NationalID);
            return doctorViewModel;
        }

        public async Task DeleteDoctorAsync(Guid id, bool trackChanges)
        {
            var doctorFromDb = await _shardService.GetDoctorCheckIfItExists(id, trackChanges);
            _repository.Doctor.DeleteDoctor(doctorFromDb);
            await _repository.SaveAsync();
        }

        public async Task<DoctorViewModel> GetDoctorAsync(Guid id, bool trackChanges, string[]? includes = null)
        {
            var doctorFromDb = await _shardService.GetDoctorCheckIfItExists(id, trackChanges);
            var doctorToReturn = new DoctorViewModel(doctorFromDb.Id, $"{doctorFromDb.FirstName} {doctorFromDb.LastName}", doctorFromDb.NationalID);
            return doctorToReturn;
        }

        public async Task<IEnumerable<DoctorViewModel>> GetDoctorsAsync(bool trackChanges)
        {
            var doctors = await _repository.Doctor.GetDoctorsAsync(trackChanges);
            var doctorsToReturn = doctors.Select(doctorFromDb => new DoctorViewModel(doctorFromDb.Id, $"{doctorFromDb.FirstName} {doctorFromDb.LastName}", doctorFromDb.NationalID)).ToList();
            return doctorsToReturn;

        }

        public async Task UpdateDoctorAsync(Guid id, DoctorForUpdateViewModel doctorForUpdate, bool trackChanges)
        {
            var doctorFromDb = await _shardService.GetDoctorCheckIfItExists(id, trackChanges);
            doctorFromDb.FirstName = doctorForUpdate.FirstName!;
            doctorFromDb.LastName = doctorForUpdate.LastName!;
            doctorFromDb.NationalID = doctorForUpdate.NationalID!;
            doctorFromDb.Address = doctorForUpdate.Address!;
            await _repository.SaveAsync();
        }
    }
}
