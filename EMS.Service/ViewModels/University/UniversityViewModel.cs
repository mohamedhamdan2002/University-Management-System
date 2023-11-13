using EMS.Service.ViewModels.Faculty;

namespace EMS.Service.ViewModels.University
{
    public record UniversityViewModel(Guid Id, string Name, string Location, IEnumerable<FacultyViewModel>? Faculties = null);
}
