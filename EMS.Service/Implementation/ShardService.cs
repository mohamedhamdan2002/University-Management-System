
using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using EMS.Service.ViewModels.Course;
using EMS.Service.ViewModels.Student;

namespace EMS.Services.Implementation
{
    public sealed class ShardService
    {
        private readonly IRepositoryManager _repository;
        public ShardService(IRepositoryManager repository) => _repository = repository;
        public async Task CheckIfUniversityExists(Guid id, bool trackChanges)
        {
            var universityDb = await _repository.Univeristy.GetUniversityAsync(id, trackChanges);
            if (universityDb is null)
                throw new Exception();
        }

        public async Task<Faculty> GetFacultyForUniversityAndCheckIfItExists(Guid universityId, Guid id, bool trackChanges, string[]? includes = null)
        {
            var facultyDb = await _repository.Faculty.GetFacultyAsync(universityId, id, trackChanges, includes);
            if (facultyDb is null)
                throw new Exception();
            return facultyDb;
        }
        public async Task<University> GetUniversityAndCheckIfItExists(Guid id, bool trackChanges, string[]? includes = null)
        {
            var universityDb = await _repository.Univeristy.GetUniversityAsync(id, trackChanges, includes);
            if (universityDb is null)
                throw new Exception();
            return universityDb;
        }
        public async Task CheckIfFacultyExists(Guid id, bool trackChanges)
        {
            var facultyDb = await _repository.Faculty.GetFacultyAsync(id, trackChanges);
            if (facultyDb is null)
                throw new Exception();
        }
        public async Task<Doctor> GetDoctorCheckIfItExists(Guid id, bool trackChanges, string[]? includes = null)
        {
            var doctorDb = await _repository.Doctor.GetDocotrAsync(id, trackChanges, includes);
            if (doctorDb is null)
                throw new Exception();
            return doctorDb;
        }
        public async Task<Staff> GetStaffCheckIfItExists(Guid id, bool trackChanges, string[]? includes = null)
        {
            var staffDb = await _repository.Staff.GetStaffAsync(id, trackChanges, includes);
            if (staffDb is null)
                throw new Exception();
            return staffDb;
        }
        public async Task CheckIfGroupExists(Guid id, bool trackChanges)
        {
            var groupDb = await _repository.Group.GetGroupAsync(id, trackChanges);
            if (groupDb is null)
                throw new Exception();
        }
        public async Task<Group> GetGroupForFacultyAndCheckIfItExists(Guid facultyId, Guid id, bool trackChanges, string[]? includes = null)
        {
            var groupDb = await _repository.Group.GetGroupAsync(facultyId, id, trackChanges, includes);
            if (groupDb is null)
                throw new Exception();
            return groupDb;
        }
        public async Task<Student> GetStudentAndCheckIfItExists(Guid groupId, Guid id, bool trackChanges, string[]? includes = null)
        {
            var studentDb = await _repository.Student.GetStudentAsync(groupId, id, trackChanges, includes);
            if (studentDb is null)
                throw new Exception();
            return studentDb;
        }
        public async Task<Department> GetDepartmentForGroupAndCheckIfItExists(Guid groupId, Guid id, bool trackChanges, string[]? includes = null)
        {
            var departmentDb = await _repository.Department.GetDepartmentAsync(groupId, id, trackChanges, includes);
            if (departmentDb is null)
                throw new Exception();
            return departmentDb;
        }
        public async Task CheckIfDepartmentExists(Guid id, bool trackChanges)
        {
            var departmentDb = await _repository.Department.GetDepartmentAsync(id, trackChanges);
            if (departmentDb is null)
                throw new Exception();
        }
        public async Task<Division> GetDivisionAndCheckIfItExists(Guid departmentId, Guid id, bool trackChanges, string[]? includes = null)
        {
            var divisionDb = await _repository.Division.GetDivisionAsync(departmentId, id, trackChanges, includes);
            if (divisionDb is null)
                throw new Exception();
            return divisionDb;
        }
        public async Task CheckIfDivisionExists(Guid id, bool trackChanges)
        {
            var divisionDb = await _repository.Division.GetDivisionAsync(id, trackChanges);
            if (divisionDb is null)
                throw new Exception();
        }
        public async Task<Course> GetCourseAndCheckIfItExists(Guid divisionId, Guid id, bool trackChanges, string[]? includes = null)
        {
            var courseDb = await _repository.Course.GetCourseAsync(divisionId, id, trackChanges, includes);
            if (courseDb is null)
                throw new Exception();
            return courseDb;
        }
        public async Task CheckIfCourseExists(Guid id, bool trackChanges)
        {
            var courseDb = await _repository.Course.GetCourseAsync(id, trackChanges);
            if (courseDb is null)
                throw new Exception();
        }

        public List<StudentViewModel>? GetStudentsIfItExist(IStudents entity)
        {
            List<StudentViewModel>? students = null;
            if (entity.Students is not null && entity.Students.Any())
                students = entity!.Students!.Select(s => new StudentViewModel(s.Id, s.FullName, s.NationalID, s.Level, s.TotalMark, s.RegisteredHours, s.GPA)).ToList();
            return students;
        }
        public List<CourseViewModel>? GetCourseIfItExist(ICourses entity)
        {
            List<CourseViewModel>? courses = null;
            if (entity.Courses is not null && entity.Courses.Any())
                courses = entity!.Courses!.Select(c => new CourseViewModel(c.Id, c.Name, c.Code, c.Description, c.Semester.ToString(), c.Credits)).ToList();
            return courses;
        }
    }
}
