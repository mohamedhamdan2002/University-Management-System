using UMS.Service.ViewModels.Faculty;

namespace UMS.Service.ViewModels.University
{
    public record UniversityViewModel(Guid Id, string Name, string Location, IEnumerable<FacultyViewModel>? Faculties = null);
}
