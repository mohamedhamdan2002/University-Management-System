using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using EMS.Service.ViewModels.Department;
using EMS.Service.ViewModels.Faculty;
using EMS.Service.ViewModels.Group;
using EMS.Service.ViewModels.Student;
using EMS.Services.Contracts;


namespace EMS.Services.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shard;
        public GroupService(IRepositoryManager repository, ShardService shard)
        {
            _repository = repository;
            _shard = shard;
        }
        public async Task<GroupViewModel> CreateGroupForFacultyAsync(Guid facultyId, GroupForCreationViewModel facultyForCreation, bool trackChanges)
        {
            await _shard.CheckIfFacultyExists(facultyId, trackChanges);
            var groupDb = new Group
            {
                Name = facultyForCreation.Name!,
            };
            _repository.Group.CreateGroupForFaculty(facultyId, groupDb);
            await _repository.SaveAsync();
            var facultyToReturn = new GroupViewModel(groupDb.Id, groupDb.Name, groupDb.Scientific.ToString());
            return facultyToReturn;
        }

        public async Task DeleteGroupForFacultyAsync(Guid facultyId, Guid id, bool trackChanges)
        {
            await _shard.CheckIfFacultyExists(facultyId, trackChanges);
            var groupFromDb = await _shard.GetGroupForFacultyAndCheckIfItExists(facultyId, id, trackChanges);
            _repository.Group.DeleteGroup(groupFromDb);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroupsAsync(Guid facultyId, bool trackChanges)
        {
            var groupsFromDb = await _repository.Group.GetGroupsAsync(facultyId, trackChanges);
            var groupsToReturn = groupsFromDb.Select(g => new GroupViewModel(g.Id, g.Name, g.Scientific.ToString())).ToList();
            return groupsToReturn;
        }

        public async Task<GroupViewModel> GetGroupAsync(Guid facultyId, Guid id, bool trackChanges, string[]? includes = null)
        {
            await _shard.CheckIfFacultyExists(facultyId, trackChanges);
            var groupFromDb = await _shard.GetGroupForFacultyAndCheckIfItExists(facultyId, id, trackChanges, includes);
            var students = GetStudentsForGroupIfItExist(groupFromDb);
            var departments = GetDepartmentsForGroupIfItExist(groupFromDb);
            var groupToReturn = new GroupViewModel(groupFromDb.Id, groupFromDb.Name, groupFromDb.Scientific.ToString(), departments, students);
            return groupToReturn;
        }

        public async Task UpdateGroupForFacultyAsync(Guid facultyId, Guid id, GroupForUpdateViewModel groupForUpdate, bool facultyTrackChanges, bool groupTrackChanges)
        {
            await _shard.CheckIfFacultyExists(facultyId, facultyTrackChanges);
            var groupFromDb = await _shard.GetGroupForFacultyAndCheckIfItExists(facultyId, id, groupTrackChanges);
            groupFromDb.Name = groupForUpdate.Name!;
            await _repository.SaveAsync();
        }
        
        private List<StudentViewModel>? GetStudentsForGroupIfItExist(Group group)
        {
            List<StudentViewModel>? students = null;
            if (group.Students is not null && group.Students.Any())
                students = group!.Students!.Select(s => new StudentViewModel(s.Id, s.FullName, s.NationalID, s.Level, s.TotalMark, s.RegisteredHours, s.GPA)).ToList();
            return students;
        }

        private List<DepartmentViewModel>? GetDepartmentsForGroupIfItExist(Group group)
        {
            List<DepartmentViewModel>? departments = null;
            if (group.Departments is not null && group.Departments.Any())
                departments = group!.Departments!.Select(d => new DepartmentViewModel(d.Id, d.Name)).ToList();
            return departments;
        }
    }
}
