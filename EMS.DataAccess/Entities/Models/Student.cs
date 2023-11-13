using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DataAccess.Entities.Models
{
    public class Student : BaseEntity
    {
        public int Level { get; set; }
        public decimal TotalMark { get; set; }
        public int RegisteredHours { get; set; }
        public decimal GPA { get; set; }
        public Guid DivisionId { get; set; }
        public Division Division { get; set; }
        public Guid GroupId { get; set; }   
        public Group Group { get; set; }    
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
