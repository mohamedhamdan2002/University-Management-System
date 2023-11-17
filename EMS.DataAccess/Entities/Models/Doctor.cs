using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DataAccess.Entities.Models
{
    public class Doctor : BaseEntity, ICourses
    {
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
