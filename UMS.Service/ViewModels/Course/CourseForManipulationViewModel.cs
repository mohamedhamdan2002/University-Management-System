using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Service.ViewModels.Course
{
    public abstract record CourseForManipulationViewModel
    {
        [Required(ErrorMessage = "Course Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Course Code is a required field.")]
        [MaxLength(5, ErrorMessage = "Maximum length for the Code is 5 characters.")]
        public string? Code { get; init; }

        [Required(ErrorMessage = "Course Description is a required field.")]
        [MaxLength(350, ErrorMessage = "Maximum length for the Description is 350 characters.")]
        public string? Description { get; init; }

        [Required(ErrorMessage = "Course Semester is a required field.")]
        [MaxLength(7, ErrorMessage = "Maximum length for the Semester is 7 characters.")]
        public string? Semester { get; init; }

        [Required(ErrorMessage = "Course Credits is a required field.")]
        [Range(minimum: 0, maximum: 3, ErrorMessage = "Creadits hours must be in Range [0 - 3]")]
        public uint Credits { get; init; }
    }
}
