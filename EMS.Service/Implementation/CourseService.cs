using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using EMS.Service.ViewModels.Course;
using EMS.Services.Contracts;


namespace EMS.Services.Implementation
{
    internal sealed class CourseService : ICourseService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shardService;
        public CourseService(IRepositoryManager repository, ShardService shardService)
        {
            _repository = repository;
            _shardService = shardService;
        }

        public async Task<CourseViewModel> CreateCourseForDivisionAsync(Guid divisionId, CourseForCreationViewModel course, bool trackChanges)
        {
            await _shardService.CheckIfDivisionExists(divisionId, trackChanges);
            var courseDb = new Course
            {
                Name = course.Name!,
                Code = course.Code!,
                Description = course.Description!,
                Credits = course.Credits!,
                Semester = (course?.Semester?.ToLower() == "first") ? SemesterType.First : (course?.Semester?.ToLower() == "second") ? SemesterType.Second : SemesterType.Summery,
                CreatedAt = course!.CreatedAt,
            };
            _repository.Course.CreateCourseForDivision(divisionId, courseDb);
            await _repository.SaveAsync();
            var courseToReturn = new CourseViewModel(courseDb.Id, courseDb.Name!, courseDb.Code, courseDb.Description, courseDb.Semester.ToString(), courseDb.Credits);
            return courseToReturn;
        }

        public async Task DeleteCourseForDivisionAsync(Guid divisionId, Guid id, bool trackChanges)
        {
            await _shardService.CheckIfDivisionExists(divisionId, trackChanges);
            var courseFromDb = await _shardService.GetCourseAndCheckIfItExists(divisionId, id, trackChanges);
            _repository.Course.DeleteCourse(courseFromDb);
            await _repository.SaveAsync();
        }

        public async Task<CourseViewModel> GetCourseAsync(Guid divisionId, Guid id, bool trackChanges, string[]? includes = null)
        {
            await _shardService.CheckIfDivisionExists(divisionId, trackChanges);
            var courseFromDb = await _shardService.GetCourseAndCheckIfItExists(divisionId, id, trackChanges);
            var courseToReturn = new CourseViewModel(courseFromDb.Id, courseFromDb.Name!, courseFromDb.Code, courseFromDb.Description, courseFromDb.Semester.ToString(), courseFromDb.Credits);
            return courseToReturn;
        }

        public async Task<IEnumerable<CourseViewModel>> GetCoursesAsync(Guid divisionId, bool trackChanges)
        {
            await _shardService.CheckIfDivisionExists(divisionId, trackChanges);
            var courses = await _repository.Course.GetCoursesAsync(divisionId, trackChanges);
            var coursesToReturn = courses.Select(c => new CourseViewModel(c.Id, c.Name!, c.Code, c.Description, c.Semester.ToString(), c.Credits));
            return coursesToReturn;
        }

        public async Task UpdateCourseForDivisionAsync(Guid divisionId, Guid id, CourseForUpdateViewModel course, bool trackChanges, bool couTrackChanges)
        {
            await _shardService.CheckIfDivisionExists(divisionId, trackChanges);
            var courseFromDb = await _shardService.GetCourseAndCheckIfItExists(divisionId, id, couTrackChanges);
            courseFromDb.Name = course.Name!;
            courseFromDb.Code = course.Code!;
            courseFromDb.Description = course.Description!;
            courseFromDb.Semester = (course?.Semester?.ToLower() == "first") ? SemesterType.First : (course?.Semester?.ToLower() == "second") ? SemesterType.Second : SemesterType.Summery;
            await _repository.SaveAsync();
        }
    }
}
