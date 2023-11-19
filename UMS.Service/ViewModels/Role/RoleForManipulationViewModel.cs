using System.ComponentModel.DataAnnotations;

namespace UMS.Service.ViewModels.Role
{
    public abstract record RoleForManipulationViewModel
    {
        [Required(ErrorMessage = "Role Name is Required")]
        [Display(Name = "Role Name")]
        public string? RoleName { get; init; }
    }
}
