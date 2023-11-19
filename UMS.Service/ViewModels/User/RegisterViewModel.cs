using System.ComponentModel.DataAnnotations;

namespace UMS.Service.ViewModels.User
{
    public record RegisterViewModel
    {
        [Required]
        public string? UserName { get; init; }
        [Required]
        [EmailAddress]
        public string? Email { get; init; } 

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; init; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The Confirm password must be the same of the Password")]
        public string? ConfirmPassword { get; init; } 
    }
}
