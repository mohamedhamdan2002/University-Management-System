using UMS.DataAccess.Repositories.Contracts;
using UMS.Services.Contracts;
using UMS.Service.ViewModels.Department;
using UMS.DataAccess.Entities.Models;
using UMS.Service.ViewModels.Division;

namespace UMS.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shard;
        public DepartmentService(IRepositoryManager repository, ShardService shard)
        {
            _repository = repository;
            _shard = shard;
        }
        public async Task<DepartmentViewModel> CreateDepartmentForGroupAsync(Guid groupId, DepartmentForCreationViewModel groupForCreation, bool trackChanges)
        {
            await _shard.CheckIfGroupExists(groupId, trackChanges);
            var departmentDb = new Department
            {
                Name = groupForCreation.Name!,
            };
            _repository.Department.CreateDepartmentForGroup(groupId, departmentDb);
            await _repository.SaveAsync();
            var groupToReturn = new DepartmentViewModel(departmentDb.Id, departmentDb.Name);
            return groupToReturn;
        }

        public async Task DeleteDepartmentForGroupAsync(Guid groupId, Guid id, bool trackChanges)
        {
            await _shard.CheckIfGroupExists(groupId, trackChanges);
            var departmentFromDb = await _shard.GetDepartmentForGroupAndCheckIfItExists(groupId, id, trackChanges);
            _repository.Department.DeleteDepartment(departmentFromDb);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetDepartmentsAsync(Guid groupId, bool trackChanges)
        {
            var departmentsFromDb = await _repository.Department.GetDepartmentsAsync(groupId, trackChanges);
            var departmentsToReturn = departmentsFromDb.Select(g => new DepartmentViewModel(g.Id, g.Name)).ToList();
            return departmentsToReturn;
        }

        public async Task<DepartmentViewModel> GetDepartmentAsync(Guid groupId, Guid id, bool trackChanges, string[]? includes = null)
        {
            await _shard.CheckIfGroupExists(groupId, trackChanges);
            var departmentFromDb = await _shard.GetDepartmentForGroupAndCheckIfItExists(groupId, id, trackChanges, includes);
            var divisions = GetDivisionsForDepartmentIfItExist(departmentFromDb);
            var departmentToReturn = new DepartmentViewModel(departmentFromDb.Id, departmentFromDb.Name, divisions);
            return departmentToReturn;
        }

        public async Task UpdateDepartmentForGroupAsync(Guid groupId, Guid id, DepartmentForUpdateViewModel departmentForUpdate, bool groupTrackChanges, bool departmentTrackChanges)
        {
            await _shard.CheckIfGroupExists(groupId, groupTrackChanges);
            var departmentFromDb = await _shard.GetDepartmentForGroupAndCheckIfItExists(groupId, id, departmentTrackChanges);
            departmentFromDb.Name = departmentForUpdate.Name!;
            await _repository.SaveAsync();
        }
        private List<DivisionViewModel>? GetDivisionsForDepartmentIfItExist(Department department)
        {
            List<DivisionViewModel>? divisions = null;
            if (department.DepartmentDivisions is not null && department.DepartmentDivisions.Any())
                divisions = department!.DepartmentDivisions!.Select(d => new DivisionViewModel(d.Division!.Id, d.Division.Name)).ToList();
            return divisions;
        }
    }
}
