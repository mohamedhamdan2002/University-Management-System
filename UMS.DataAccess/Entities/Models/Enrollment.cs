using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.DataAccess.Entities.Models
{
    public class Enrollment 
    {
        public Guid CourseId { get; set; }  
        public Guid StudentId { get; set; }
        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}
