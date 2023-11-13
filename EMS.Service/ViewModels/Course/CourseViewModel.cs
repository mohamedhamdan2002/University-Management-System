using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.ViewModels.Course
{
    public record CourseViewModel(Guid Id, string Name, string Code, string Description, string Semester, uint Credits);

}
