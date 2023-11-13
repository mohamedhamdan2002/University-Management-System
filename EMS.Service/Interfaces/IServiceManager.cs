using EMS.Service.Interfaces;

namespace EMS.Services.Contracts
{
    public interface IServiceManager
    {
        IUniversityService UniversityService { get; }
        IFacultyService FacultyService { get; }
        IGroupService GroupService { get; }
        IDepartmentService DepartmentService { get; }
        IDivisionService DivisionService { get; }
        ICourseService CourseService { get; }
        IStudentService StudentService { get; }
        IDoctorService DoctorService { get; }
        IStaffService StaffService { get; }
        IAuthService AuthService { get; }

    }
}
