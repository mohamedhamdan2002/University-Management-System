using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.ViewModels.Role
{

    public record RoleFroUpdateViewModel : RoleForManipulationViewModel
    {
        public Guid id { get; init; }
        public List<string?>? Users { get; init; }
    }
}
