using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DataAccess.Entities.Models
{
    public class Faculty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UniversityId { get; set; }
        public University University { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
