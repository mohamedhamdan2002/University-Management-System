using EMS.Service.ViewModels.Student;
namespace EMS.Services.Contracts
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentViewModel>> GetStudentsAsync(Guid groupId, bool trackChanges);
        Task<StudentViewModel> GetStudentAsync(Guid groupId, Guid id, bool trackChanges, string[]? includes = null);
        Task<StudentViewModel> CreateStudentForGroupAsync(Guid groupdId, StudentForCreationViewModel studentForCreation, bool trackChanges);
        Task UpdateStudentForGroupAsync(Guid groupId, Guid id, StudentForUpdateViewModel studentForUpdate, bool groupTrackChanges, bool studentTrackChanges);
        Task DeleteStudentForGroupAsync(Guid groupId, Guid id, bool trackChanges);
    }
}
