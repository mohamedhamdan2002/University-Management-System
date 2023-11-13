using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.ViewModels.Division
{
    public abstract record DivisionForManipulationViewModel
    {
        [Required(ErrorMessage = "Divsion Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string? Name { get; init; }

    }
}
