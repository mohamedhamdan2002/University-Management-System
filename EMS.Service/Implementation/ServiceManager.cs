using EMS.DataAccess.Entities.Models;
using EMS.DataAccess.Repositories.Contracts;
using EMS.Service.Implementation;
using EMS.Service.Interfaces;
using EMS.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace EMS.Services.Implementation
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService;
        private readonly Lazy<IUniversityService> _universityService;
        private readonly Lazy<IFacultyService> _facultyService;
        private readonly Lazy<IGroupService> _groupService;
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IDivisionService> _divisionService;
        private readonly Lazy<ICourseService> _courseService;
        private readonly Lazy<IStudentService> _studentService;
        private readonly Lazy<IDoctorService> _doctorService;
        private readonly Lazy<IStaffService> _staffService;

        public ServiceManager(IRepositoryManager repositoryManager, ShardService shardService, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _authService = new Lazy<IAuthService>(() => new AuthService(userManager, signInManager, roleManager));
            _universityService = new Lazy<IUniversityService>(() => new  UniversityService(repositoryManager, shardService));
            _facultyService = new Lazy<IFacultyService>(() => new FacultyService(repositoryManager, shardService));
            _groupService = new Lazy<IGroupService>(() => new GroupService(repositoryManager, shardService));
            _departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repositoryManager, shardService));
            _divisionService = new Lazy<IDivisionService>(() => new DivisionService(repositoryManager, shardService));
            _courseService = new Lazy<ICourseService>(() => new CourseService(repositoryManager, shardService));
            _studentService = new Lazy<IStudentService>(() => new StudentService(repositoryManager, shardService));
            _doctorService = new Lazy<IDoctorService>(() => new DoctorService(repositoryManager,shardService));
            _staffService = new Lazy<IStaffService>(() => new StaffService(repositoryManager, shardService));
        }
        public IAuthService AuthService => _authService.Value;
        public IUniversityService UniversityService => _universityService.Value;
        public IFacultyService FacultyService => _facultyService.Value;
        public IGroupService GroupService => _groupService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
        public IDivisionService DivisionService => _divisionService.Value;
        public ICourseService CourseService => _courseService.Value;
        public IStudentService StudentService => _studentService.Value;
        public IDoctorService DoctorService => _doctorService.Value;
        public IStaffService StaffService => _staffService.Value;
    }
}
