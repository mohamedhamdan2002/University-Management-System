using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using EMS.Service.ViewModels.Faculty;
using EMS.Service.ViewModels.Group;
using EMS.Services.Contracts;


namespace EMS.Services.Implementation
{
    public class FacultyService : IFacultyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shard;
        public FacultyService(IRepositoryManager repository, ShardService shard)
        {
            _repository = repository;
            _shard = shard;
        }
        public async Task<FacultyViewModel> CreateFacultyForUniversityAsync(Guid universityId, FacultyForCreationViewModel facultyForCreation, bool trackChanges)
        {
            await _shard.CheckIfUniversityExists(universityId, trackChanges);
            var facultyDb = new Faculty
            {
                Name = facultyForCreation.Name!,
                Description = facultyForCreation.Description!
            };
            _repository.Faculty.CreateFacultyForUniversity(universityId, facultyDb);
            await _repository.SaveAsync();
            var facultyToReturn = new FacultyViewModel(facultyDb.Id, facultyDb.Name, facultyDb.Description);
            return facultyToReturn;
        }
        public async Task DeleteFacultyForUniverstiyAsync(Guid universityId, Guid id, bool trackChanges)
        {
            await _shard.CheckIfUniversityExists(universityId, trackChanges);
            var facultyFromDb = await _shard.GetFacultyForUniversityAndCheckIfItExists(universityId, id, trackChanges);
            _repository.Faculty.DeleteFaculty(facultyFromDb);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<FacultyViewModel>> GetFacultiesAsync(Guid universityId, bool trackChanges)
        {
            var facultiesFromDb = await _repository.Faculty.GetFacultiesAsync(universityId, trackChanges);
            var facultiesToReturn = facultiesFromDb.Select(f => new FacultyViewModel(f.Id, f.Name, f.Description));
            return facultiesToReturn;
        }
        public async Task<FacultyViewModel> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges, string[]? includes = null)
        {
            await _shard.CheckIfUniversityExists(universityId, trackChanges);
            var facultyFromDb = await _shard.GetFacultyForUniversityAndCheckIfItExists(universityId, id, trackChanges, includes);
            var groups = GetGroupssForFacultyIfItExist(facultyFromDb);
            var facultyToReturn = new FacultyViewModel(facultyFromDb.Id, facultyFromDb.Name, facultyFromDb.Description, groups);
            return facultyToReturn;
        }
        public async Task UpdateFacultyForUniversityAsync(Guid universityId, Guid id, FacultyForUpdateViewModel facultyForUpdate, bool universityTrackChanges, bool facultyTrackChanges)
        {
            await _shard.CheckIfUniversityExists(universityId, universityTrackChanges);
            var facultyFromDb = await _shard.GetFacultyForUniversityAndCheckIfItExists(universityId, id, facultyTrackChanges);
            facultyFromDb.Name = facultyForUpdate.Name!;
            facultyFromDb.Description = facultyForUpdate.Description!;
            await _repository.SaveAsync();
        }
        private List<GroupViewModel>? GetGroupssForFacultyIfItExist(Faculty faculty)
        {
            List<GroupViewModel>? groups = null;
            if (faculty.Groups is not null && faculty.Groups.Any())
                groups = faculty!.Groups!.Select(g => new GroupViewModel(g.Id, g.Name, g.Scientific.ToString())).ToList();
            return groups;
        }
    }
}
