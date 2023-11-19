using UMS.Service.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Course
{
    public record CourseViewModel(Guid divisionId, Guid Id, string Name, string Code, string Description, string Semester, uint Credits, List<StudentViewModel>? Students = null);

}
