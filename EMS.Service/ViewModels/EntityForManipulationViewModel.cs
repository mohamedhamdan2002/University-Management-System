using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.ViewModels
{
    public abstract record EntityForManipulationViewModel
    {
        [Required(ErrorMessage = "First Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the First Name is 50 characters.")]
        public string? FirstName { get; init; }

        [Required(ErrorMessage = "Last Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Last Name is 50 characters.")]
        public string? LastName { get; init; }

        [Required(ErrorMessage = "Address is a required field.")]
        [MaxLength(350, ErrorMessage = "Maximum length for the Address is 350 characters.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "NationalID  is a required field.")]
        [MaxLength(14, ErrorMessage = "Maximum length for the NationalID is 14 characters.")]
        public string? NationalID { get; init; }
    }
}
