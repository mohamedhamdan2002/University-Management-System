using UMS.Service.ViewModels.Department;
using UMS.Service.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Group
{
    public record GroupViewModel(
        Guid Id, string Name, string Scientific,
        IEnumerable<DepartmentViewModel>? Departments = null,
        IEnumerable<StudentViewModel>? Students = null);
}
