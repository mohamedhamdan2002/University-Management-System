﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.DataAccess.Entities.Models
{
    public class User : IdentityUser<Guid>
    {
    }
}
