using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.DataAccess.Entities.Models
{
    public class Division : IStudents, ICourses
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<DepartmentDivision> DepartmentDivisions { get; set; } = new List<DepartmentDivision>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
