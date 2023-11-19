using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using UMS.DataAccess.Repositories.Implementation;
using UMS.Service.ViewModels.Department;
using UMS.Service.ViewModels.Faculty;
using UMS.Service.ViewModels.University;
using UMS.Services.Contracts;

namespace UMS.Services.Implementation
{
    public class UniversityService : IUniversityService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shard;
        public UniversityService(IRepositoryManager repository, ShardService shard)
        {
            _repository = repository;
            _shard = shard;
        }

        public async Task<UniversityViewModel> CreateUniversityAsync(UniversityForCreationViewModel universityForCreation)
        {
            var universityDb = new University
            {
                Name = universityForCreation.Name!,
                Location = universityForCreation.Location!
            };
            _repository.Univeristy.CreateUniversity(universityDb);
            await _repository.SaveAsync();
            var universityToReturn = new UniversityViewModel(universityDb.Id, universityDb.Name, universityDb.Location);
            return universityToReturn;
        }
        public async Task DeleteUniversityAsync(Guid id, bool trackChanges)
        {
            var universityFromDb = await _shard.GetUniversityAndCheckIfItExists(id, trackChanges);
            _repository.Univeristy.DeleteUniversity(universityFromDb);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<UniversityViewModel>> GetUniversitiesAsync(bool trackChanges)
        {
            var universitiesFromDb = await _repository.Univeristy.GetUniversitiesAsync(trackChanges);
            var universitiesToReturn = universitiesFromDb.Select(u => new UniversityViewModel(u.Id, u.Name, u.Location));
            return universitiesToReturn;
        }

        public async Task<UniversityViewModel> GetUniversityAsync(Guid id, bool trackChanges, string[]? includes = null)
        {
            var universityFromDb = await _shard.GetUniversityAndCheckIfItExists(id, trackChanges, includes);
            var faculties = GetFacultiesForUniversityIfItExist(universityFromDb);
            var universityToReturn = new UniversityViewModel(universityFromDb.Id, universityFromDb.Name, universityFromDb.Location, faculties);
            return universityToReturn;
        }
        public async Task UpdateUniversityAsync(Guid id, UniversityForUpdateViewModel universityForUpdate, bool trackChanges)
        {
            var universityFromDb = await _shard.GetUniversityAndCheckIfItExists(id, trackChanges);
            universityFromDb.Name = universityForUpdate.Name!;
            universityFromDb.Location = universityForUpdate.Location!;
            await _repository.SaveAsync();
        }
        private List<FacultyViewModel>? GetFacultiesForUniversityIfItExist(University university)
        {
            List<FacultyViewModel>? faculties = null;
            if (university.Faculties is not null && university.Faculties.Any())
                faculties = university!.Faculties!.Select(f => new FacultyViewModel(f.Id, f.Name, f.Description)).ToList();
            return faculties;
        }
    }
}
