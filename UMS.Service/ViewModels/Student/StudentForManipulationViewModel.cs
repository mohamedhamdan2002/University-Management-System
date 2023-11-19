using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Student
{
    public abstract record StudentForManipulationViewModel : EntityForManipulationViewModel
    {

        [Required(ErrorMessage = "Level is a required field.")]
        [Range(minimum: 1, maximum: 5, ErrorMessage = " Level must be in Range [1 - 5]")]
        public uint Level { get; init; }
        public decimal TotalMark { get; init; }
        public uint RegisteredHours { get; init; }
        public decimal GPA { get; init; }
    }
}
