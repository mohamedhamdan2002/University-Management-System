using UMS.DataAccess.Entities.Models;

namespace UMS.DataAccess.Repositories.Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudentsAsync(Guid groupId, bool trackChanges);
        Task<Student?> GetStudentAsync(Guid groupId , Guid id, bool trackChanges, string[]? includes = null);
        void CreateStudentForGroup(Guid groupId, Student student);
        void DeleteStudent(Student student);
    }
}
