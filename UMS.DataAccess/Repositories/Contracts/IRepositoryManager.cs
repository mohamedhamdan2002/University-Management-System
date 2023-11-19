namespace UMS.DataAccess.Repositories.Contracts
{
    public interface IRepositoryManager 
    {
        IUniveristyRepository Univeristy {  get; }
        IFacultyRepository Faculty { get; }
        IGroupRepository Group { get; }
        IDepartmentRepository Department { get; }
        IDivisionRepository Division { get; }
        ICourseRepository Course { get; }
        IStudentRepository Student { get; }
        IDoctorRepository Doctor { get; }
        IStaffRepository Staff { get; }
        Task SaveAsync();
    }
}
