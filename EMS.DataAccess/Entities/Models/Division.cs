using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DataAccess.Entities.Models
{
    public class Division
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<DepartmentDivision> DepartmentDivisions { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
