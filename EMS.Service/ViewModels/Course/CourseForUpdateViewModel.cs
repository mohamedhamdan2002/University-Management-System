﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.ViewModels.Course
{
    public record class CourseForUpdateViewModel : CourseForManipulationViewModel
    {
        public DateTime UpdatedAt { get; } = DateTime.Now;
    }
}