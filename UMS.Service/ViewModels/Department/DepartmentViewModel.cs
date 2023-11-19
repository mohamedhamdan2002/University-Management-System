using UMS.Service.ViewModels.Division;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Department
{
    public record DepartmentViewModel(Guid Id, string Name, List<DivisionViewModel>? Divisions = null);
}
