using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.DataAccess.Entities.Models
{
    public class Department
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<DepartmentDivision> DepartmentDivisions { get; set; } = new List<DepartmentDivision>();
    }
}
