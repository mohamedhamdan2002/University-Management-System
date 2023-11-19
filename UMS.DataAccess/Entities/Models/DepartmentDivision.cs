using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.DataAccess.Entities.Models
{
    public class DepartmentDivision
    {
        public Guid DepartmentId { get; set; }
        public Guid DivisionId { get; set; }
        public Department? Department { get; set; }
        public Division? Division { get; set; }

    }
}
