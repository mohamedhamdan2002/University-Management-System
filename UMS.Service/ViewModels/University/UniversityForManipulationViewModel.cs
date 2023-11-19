using System.ComponentModel.DataAnnotations;

namespace UMS.Service.ViewModels.University
{
    public abstract record UniversityForManipulationViewModel
    {
        [Required(ErrorMessage = "University Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "University location is a required field.")]
        [MaxLength(250, ErrorMessage = "Maximum length for the Location is 250 characters.")]
        public string? Location { get; init; }
    }
}
