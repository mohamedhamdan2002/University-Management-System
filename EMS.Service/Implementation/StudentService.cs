using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using EMS.Service.ViewModels.Student;
using EMS.Services.Contracts;

namespace EMS.Services.Implementation
{
    internal sealed class StudentService : IStudentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shardService;

        public StudentService(IRepositoryManager repository, ShardService shardService)
        {
            _repository = repository;
            _shardService = shardService;
        }

        public async Task<StudentViewModel> CreateStudentForGroupAsync(Guid groupId, StudentForCreationViewModel studentForCreation, bool trackChanges)
        {
            await _shardService.CheckIfGroupExists(groupId, trackChanges);
            var studentDb = new Student
            {
                FirstName = studentForCreation.FirstName!,
                LastName = studentForCreation.LastName!,
                NationalID = studentForCreation.NationalID!,
                Address = studentForCreation.Address!,
                Level = (int)studentForCreation.Level!,
            };
            _repository.Student.CreateStudentForGroup(groupId, studentDb);
            await _repository.SaveAsync();
            var studentToReturn = new StudentViewModel(studentDb.Id, $"{studentDb.FirstName} {studentDb.LastName}", studentDb.NationalID, studentDb.Level, studentDb.TotalMark, studentDb.RegisteredHours, studentDb.GPA);
            return studentToReturn;
        }

        public async Task DeleteStudentForGroupAsync(Guid groupId, Guid id, bool trackChanges)
        {
            await _shardService.CheckIfGroupExists(groupId, trackChanges);
            var studentFromDb = await _shardService.GetStudentAndCheckIfItExists(groupId, id, trackChanges);
            _repository.Student.DeleteStudent(studentFromDb);
            await _repository.SaveAsync();
        }

        public async Task<StudentViewModel> GetStudentAsync(Guid groupId, Guid id, bool trackChanges, string[]? includes = null)
        {
            await _shardService.CheckIfGroupExists(groupId, trackChanges);
            var studentFromDb = await _shardService.GetStudentAndCheckIfItExists(groupId, id, trackChanges);
            var studentToReturn = new StudentViewModel(studentFromDb.Id, $"{studentFromDb.FirstName} {studentFromDb.LastName}", studentFromDb.NationalID, studentFromDb.Level, studentFromDb.TotalMark, studentFromDb.RegisteredHours, studentFromDb.GPA);
            return studentToReturn;
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudentsAsync(Guid groupId, bool trackChanges)
        {
            await _shardService.CheckIfGroupExists(groupId, trackChanges);
            var students = await _repository.Student.GetStudentsAsync(groupId, trackChanges);
            var studentsToReturn = students.Select(s => new StudentViewModel(s.Id, $"{s.FirstName} {s.LastName}", s.NationalID, s.Level, s.TotalMark, s.RegisteredHours
                , s.GPA)).ToList();
            return studentsToReturn;
        }

        public async Task UpdateStudentForGroupAsync(Guid groupId, Guid id, StudentForUpdateViewModel studentForUpdate, bool groupTrackChanges, bool studentTrackChanges)
        {
            await _shardService.CheckIfGroupExists(groupId, groupTrackChanges);
            var studentFromDb = await _shardService.GetStudentAndCheckIfItExists(groupId, id, studentTrackChanges);
            studentFromDb.FirstName = studentForUpdate.FirstName!;
            studentFromDb.LastName = studentForUpdate.LastName!;
            studentFromDb.Address = studentForUpdate.Address!;
            studentFromDb.NationalID = studentForUpdate.NationalID!;
            studentFromDb.Level = (int)studentForUpdate.Level;
            studentFromDb.TotalMark = studentForUpdate.TotalMark;
            studentFromDb.RegisteredHours = (int)studentForUpdate.RegisteredHours;
            studentFromDb.GPA = studentForUpdate.GPA;
            await _repository.SaveAsync();
        }
    }
}
