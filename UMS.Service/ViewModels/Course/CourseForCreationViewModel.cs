using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Course
{
    public record CourseForCreationViewModel : CourseForManipulationViewModel
    {
        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}
