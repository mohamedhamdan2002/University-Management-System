﻿using EMS.Service.ViewModels.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.ViewModels.Faculty
{
     public record FacultyViewModel(Guid Id, string Name, string Description, IEnumerable<GroupViewModel>? Groups = null);
}
