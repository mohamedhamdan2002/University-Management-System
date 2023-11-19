using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.User
{
    public record LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; init; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; init; }
    }
}
