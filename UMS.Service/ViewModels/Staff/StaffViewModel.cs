using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Staff
{
    public record StaffViewModel(Guid Id, string FullName, string NationalID);
}
