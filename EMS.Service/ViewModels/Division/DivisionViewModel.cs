using EMS.DataAccess.Entities.Models;
using EMS.Service.ViewModels.Course;
using EMS.Service.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.ViewModels.Division
{
    public record DivisionViewModel(Guid Id, string Name, List<CourseViewModel>? Courses = null, List<StudentViewModel>? Students = null);
}
