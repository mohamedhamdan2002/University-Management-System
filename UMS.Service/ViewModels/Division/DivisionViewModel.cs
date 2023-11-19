using UMS.DataAccess.Entities.Models;
using UMS.Service.ViewModels.Course;
using UMS.Service.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Division
{
    public record DivisionViewModel(Guid Id, string Name, List<CourseViewModel>? Courses = null, List<StudentViewModel>? Students = null);
}
