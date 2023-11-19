using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using UMS.Service.ViewModels.Doctor;
using UMS.Service.ViewModels.Staff;
using UMS.Services.Contracts;
namespace UMS.Services.Implementation
{
    internal sealed class StaffService : IStaffService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shardService;
        public StaffService(IRepositoryManager repository, ShardService shardService)
        {
            _repository = repository;
            _shardService = shardService;
        }
        public async Task<StaffViewModel> CreateStaffAsync(StaffForCreationViewModel staffForCreation)
        {
            var staffDb = new Staff
            {
                FirstName = staffForCreation.FirstName!,
                LastName = staffForCreation.LastName!,
                NationalID = staffForCreation.NationalID!,
                Address = staffForCreation.Address!,
            };
            _repository.Staff.CreateStaff(staffDb);
            await _repository.SaveAsync();
            var staffToReturn = new StaffViewModel(staffDb.Id, $"{staffDb.FirstName} {staffDb.LastName}", staffDb.NationalID);
            return staffToReturn;
        }

        public async Task DeleteStaffAsync(Guid id, bool trackChanges)
        {
            var staffFromDb = await _shardService.GetStaffCheckIfItExists(id, trackChanges);
            _repository.Staff.DeleteStaff(staffFromDb);
            await _repository.SaveAsync();
        }

        public async Task<StaffViewModel> GetStaffAsync(Guid id, bool trackChanges, string[]? includes = null)
        {
            var staffFromDb = await _shardService.GetStaffCheckIfItExists(id, trackChanges);
            var staffToReturn = new StaffViewModel(staffFromDb.Id, $"{staffFromDb.FirstName} {staffFromDb.LastName}", staffFromDb.NationalID);
            return staffToReturn;
        }

        public async Task<IEnumerable<StaffViewModel>> GetStaffsAsync(bool trackChanges)
        {
            var Staffs = await _repository.Staff.GetStaffsAsync(trackChanges);
            var staffsToReturn = Staffs.Select(staffFromDb => new StaffViewModel(staffFromDb.Id, $"{staffFromDb.FirstName} {staffFromDb.LastName}", staffFromDb.NationalID)).ToList();
            return staffsToReturn;

        }

        public async Task UpdateStaffAsync(Guid id, StaffForUpdateViewModel staffForUpdate, bool trackChanges)
        {
            var staffFromDb = await _shardService.GetStaffCheckIfItExists(id, trackChanges);
            staffFromDb.FirstName = staffForUpdate.FirstName!;
            staffFromDb.LastName = staffForUpdate.LastName!;
            staffFromDb.NationalID = staffForUpdate.NationalID!;
            staffFromDb.Address = staffForUpdate.Address!;
            await _repository.SaveAsync();
        }
    }
}
