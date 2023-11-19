using EMS.Service.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.ViewModels.Doctor
{
    public record DoctorViewModel(Guid Id, string FullName, string NationalID, List<CourseViewModel>? Courses = null);
}
