using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.DataAccess.Entities.Models
{
    public class Group : IStudents
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ScientificType Scientific { get; set; }
        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

    
    public enum ScientificType
    {
        Math,
        Science,
        Literary
    }
}
