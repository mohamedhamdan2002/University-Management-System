using UMS.Service.ViewModels.University;
namespace UMS.Services.Contracts
{
    public interface IUniversityService
    {
        Task<IEnumerable<UniversityViewModel>> GetUniversitiesAsync(bool trackChanges);
        Task<UniversityViewModel> GetUniversityAsync(Guid universityId, bool trackChanges, string[]? includes = null);
        Task<UniversityViewModel> CreateUniversityAsync(UniversityForCreationViewModel universityForCreation);
        Task UpdateUniversityAsync(Guid id, UniversityForUpdateViewModel universityForUpdate, bool trackChanges);
        Task DeleteUniversityAsync(Guid id, bool trackChanges);
    }
}
