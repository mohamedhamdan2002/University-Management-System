using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Group
{
    public abstract record GroupForManipulationViewModel
    {
        [Required(ErrorMessage = "Group Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Group Scientific is a required field.")]
        public string? Scientific { get; init; }
    }
}
