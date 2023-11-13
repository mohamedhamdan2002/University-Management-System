using EMS.Service.ViewModels.Faculty;
namespace EMS.Services.Contracts
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyViewModel>> GetFacultiesAsync(Guid universityId, bool trackChanges);
        Task<FacultyViewModel> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges, string[]? includes = null);
        Task<FacultyViewModel> CreateFacultyForUniversityAsync(Guid universityId, FacultyForCreationViewModel faculty, bool trackChanges);
        Task DeleteFacultyForUniverstiyAsync(Guid universityId, Guid id, bool trackChanges);
        Task UpdateFacultyForUniversityAsync(Guid universityId, Guid id, FacultyForUpdateViewModel faculty, bool universityTrackChanges, bool facultyTrackChanges);
    }
}
