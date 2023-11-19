using UMS.DataAccess.Entities.Models;

namespace UMS.DataAccess.Repositories.Contracts
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> GetFacultiesAsync(Guid universtiyId, bool trackChanges);
        Task<Faculty?> GetFacultyAsync(Guid universtiyId, Guid id, bool trackChanges, string[]? includes = null);
        Task<Faculty?> GetFacultyAsync(Guid id, bool trackChanges, string[]? includes = null);
        void CreateFacultyForUniversity(Guid universityId, Faculty faculty);
        void DeleteFaculty(Faculty faculty);
    }
}
