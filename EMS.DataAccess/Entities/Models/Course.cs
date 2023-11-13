using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DataAccess.Entities.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public SemesterType Semester { get; set; }
        public string Description { get; set; }
        public uint Credits { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid DivisionId { get; set; }
        public Division Division { get; set; }

        public Guid? DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid? staffId { get; set; }
        public Staff Staff { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
    public enum SemesterType
    {
        First,
        Second,
        Summery
    }
}
