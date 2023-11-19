using UMS.Service.ViewModels.Course;
namespace UMS.Services.Contracts
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseViewModel>> GetCoursesAsync(Guid divisionId, bool trackChanges);
        Task<CourseViewModel> GetCourseAsync(Guid divisionId, Guid id, bool trackChanges, string[]? includes = null);
        Task<CourseViewModel> CreateCourseForDivisionAsync(Guid divisionId, CourseForCreationViewModel course, bool trackChanges);
        Task DeleteCourseForDivisionAsync(Guid divisionId, Guid id, bool trackChanges);
        Task UpdateCourseForDivisionAsync(Guid divisionId, Guid id, CourseForUpdateViewModel course, bool divisionTrackChanges, bool courseTrackChanges);
    }

}
