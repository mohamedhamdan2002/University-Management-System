using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Faculty
{
    public abstract record FacultyForManipulationViewModel
    {
        [Required(ErrorMessage = "Faculty Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Faculty Description is a required field.")]
        [MaxLength(500, ErrorMessage = "Maximum length for the Description is 500 characters.")]
        public string? Description { get; init; }
    }
}
