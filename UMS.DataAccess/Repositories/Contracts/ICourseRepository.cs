
using UMS.DataAccess.Entities.Models;

namespace UMS.DataAccess.Repositories.Contracts
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync(Guid divisionId, bool trackChanges);
        Task<Course?> GetCourseAsync(Guid divisionId, Guid id, bool trackChanges, string[]? includes = null);
        Task<Course?> GetCourseAsync(Guid id, bool trackChanges, string[]? includes = null);

        void CreateCourseForDivision(Guid divisionId, Course course );
        void DeleteCourse(Course course);
    }
}
