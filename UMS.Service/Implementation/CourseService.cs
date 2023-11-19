using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using UMS.Service.ViewModels.Course;
using UMS.Service.ViewModels.Student;
using UMS.Services.Contracts;


namespace UMS.Services.Implementation
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
            var courseToReturn = new CourseViewModel(divisionId, courseDb.Id, courseDb.Name!, courseDb.Code, courseDb.Description, courseDb.Semester.ToString(), courseDb.Credits);
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
            var students = GetStudentsFromEnrollmentsIfItExist(courseFromDb);
            var courseToReturn = new CourseViewModel(divisionId, courseFromDb.Id, courseFromDb.Name!, courseFromDb.Code, courseFromDb.Description, courseFromDb.Semester.ToString(), courseFromDb.Credits, students);
            return courseToReturn;
        }

        public async Task<IEnumerable<CourseViewModel>> GetCoursesAsync(Guid divisionId, bool trackChanges)
        {
            await _shardService.CheckIfDivisionExists(divisionId, trackChanges);
            var courses = await _repository.Course.GetCoursesAsync(divisionId, trackChanges);
            var coursesToReturn = courses.Select(c => new CourseViewModel(divisionId, c.Id, c.Name!, c.Code, c.Description, c.Semester.ToString(), c.Credits));
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
        private List<StudentViewModel>? GetStudentsFromEnrollmentsIfItExist(Course course)
        {
            List<StudentViewModel>? students = null;
            if(course.Enrollments is not null &&  course.Enrollments.Any())
                students = course.Enrollments.Select(s => new StudentViewModel(s.Student!.Id, s.Student.FullName, s.Student.NationalID, s.Student.Level, s.Student.TotalMark, s.Student.RegisteredHours, s.Student.GPA)).ToList();
            return students;
        }
    }
}
