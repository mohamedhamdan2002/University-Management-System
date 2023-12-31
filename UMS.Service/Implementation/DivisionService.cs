﻿using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using UMS.Service.ViewModels.Course;
using UMS.Service.ViewModels.Division;
using UMS.Service.ViewModels.Student;
using UMS.Services.Contracts;
namespace UMS.Services.Implementation
{
    internal sealed class DivisionService : IDivisionService
    {
        private readonly IRepositoryManager _repository;
        private readonly ShardService _shardService;
        public DivisionService(IRepositoryManager repository, ShardService shardService)
        {
            _repository = repository;
            _shardService = shardService;
        }
        public async Task<DivisionViewModel> CreateDivisionForDepartmentAsync(Guid departmentId, DivisionForCreationViewModel division, bool trackChanges)
        {
            await _shardService.CheckIfDepartmentExists(departmentId, trackChanges);
            var divisionDb = new Division
            {
                Name = division.Name!
            };
            _repository.Division.CreateDivisionForDepartment(departmentId, divisionDb);
            await _repository.SaveAsync();
            var divisionToReturn = new DivisionViewModel(divisionDb.Id, divisionDb.Name);
            return divisionToReturn;
        }

        public async Task DeleteDivisionForDepartmentAsync(Guid departmentId, Guid id, bool trackChanges)
        {
            await _shardService.CheckIfDepartmentExists(departmentId, trackChanges);
            var divisionFromDb = await _shardService.GetDivisionAndCheckIfItExists(departmentId, id, trackChanges);
            _repository.Division.DeleteDivision(divisionFromDb);
            await _repository.SaveAsync();
        }

        public async Task<DivisionViewModel> GetDivisionAsync(Guid departmentId, Guid id, bool trackChanges, string[]? includes = null)
        {
            await _shardService.CheckIfDepartmentExists(departmentId, trackChanges);
            var divisionFromDb = await _shardService.GetDivisionAndCheckIfItExists(departmentId, id, trackChanges, includes);
            var students = _shardService.GetStudentsIfItExist(divisionFromDb);
            var courses = _shardService.GetCourseIfItExist(divisionFromDb);
            var divisionToReturn = new DivisionViewModel(divisionFromDb.Id, divisionFromDb.Name, courses, students);
            return divisionToReturn;
        }

        public async Task<IEnumerable<DivisionViewModel>> GetDivisionsAsync(Guid departmentId, bool trackChanges)
        {
            await _shardService.CheckIfDepartmentExists(departmentId, trackChanges);
            var divisionsFromDb = await _repository.Division.GetDivisionsAsync(departmentId, trackChanges);
            var divisionsToReturn = divisionsFromDb.Select(div => new DivisionViewModel(div.Id, div.Name));
            return divisionsToReturn;
        }

        public async Task UpdateDivisionForDepartmentAsync(Guid departmentId, Guid id, DivisionForUpdateViewModel divison, bool deptrackChanges, bool divTrackChanges)
        {
            await _shardService.CheckIfDepartmentExists(departmentId, deptrackChanges);
            var divisionFromDb = await _shardService.GetDivisionAndCheckIfItExists(departmentId, id, divTrackChanges);
            divisionFromDb.Name = divison.Name!;
            await _repository.SaveAsync();
        }
    }
}
