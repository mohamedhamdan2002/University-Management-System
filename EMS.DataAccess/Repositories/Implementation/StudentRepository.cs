using EMS.DataAccess.Data;
using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EMS.DataAccess.Repositories.Implementation
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) 
            : base(context) {}

        public void CreateStudentForGroup(Guid groupId, Student student)
        {
            student.GroupId = groupId;
            Create(student);
        }

        public void DeleteStudent(Student student)
            => Delete(student);
        public async Task<Student?> GetStudentAsync(Guid groupId, Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(s => s.GroupId == groupId && s.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Student>> GetStudentsAsync(Guid groupId, bool trackChanges)
            => await GetByCondition(s => s.GroupId == groupId, trackChanges)
            .OrderBy(s => s.FirstName)
            .ToListAsync();

    }
}
