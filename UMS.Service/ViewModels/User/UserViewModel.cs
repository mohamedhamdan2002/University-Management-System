using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.User
{
    public record UserViewModel(Guid Id, string UserName, string Email);
}
