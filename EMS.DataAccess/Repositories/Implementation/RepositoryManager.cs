using EMS.DataAccess.Data;
using EMS.DataAccess.Repositories.Contracts;


namespace EMS.DataAccess.Repositories.Implementation
{
    public sealed class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IUniveristyRepository> _univeristyRepository;
        private readonly Lazy<IFacultyRepository> _faultyRepository;
        private readonly Lazy<IGroupRepository> _groupRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IDivisionRepository> _divisionRepository;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<IStudentRepository> _studentRepository;
        private readonly Lazy<IDoctorRepository> _doctorRepository;
        private readonly Lazy<IStaffRepository> _staffRepository;
        private bool _disposed = false;
        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _univeristyRepository = new Lazy<IUniveristyRepository>(() => new UniversityRepository(_context));
            _faultyRepository = new Lazy<IFacultyRepository>(() => new FacultyRepository(_context));
            _groupRepository = new Lazy<IGroupRepository>(()  => new GroupRepository(_context));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_context));
            _divisionRepository = new Lazy<IDivisionRepository>(() => new DivisionRepository(_context));
            _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_context));
            _studentRepository = new Lazy<IStudentRepository>(() => new StudentRepository(_context));
            _doctorRepository = new Lazy<IDoctorRepository>(() => new DoctorRepository(_context));
            _staffRepository = new Lazy<IStaffRepository>(() => new StaffRepository(_context));
        }
        public IUniveristyRepository Univeristy => _univeristyRepository.Value;
        public IFacultyRepository Faculty => _faultyRepository.Value;
        public IGroupRepository Group => _groupRepository.Value;
        public IDepartmentRepository Department => _departmentRepository.Value;
        public IDivisionRepository Division => _divisionRepository.Value;
        public ICourseRepository Course => _courseRepository.Value;
        public IStudentRepository Student => _studentRepository.Value;
        public IDoctorRepository Doctor => _doctorRepository.Value;
        public IStaffRepository Staff => _staffRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();


        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~RepositoryManager() => this.Dispose(false);

    }
}
