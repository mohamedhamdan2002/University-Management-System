using UMS.DataAccess.Entities.Models;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;

namespace UMS.Presentation.Utilities
{
    public static class NavigationProperties
    {
        public const string University = nameof(University);
        public const string Faculty = nameof(Faculty);
        public const string Group = nameof(Group);
        public const string Department = nameof(Department);
        public const string Division = nameof(Division);
        public const string Course = nameof(Course);
        public const string Doctor = nameof(Doctor);
        public const string Staff = nameof(Staff);
        public const string Student = nameof(Student);
        public const string Faculties = nameof(Faculties);
        public const string Departments = nameof(Departments);
        public const string Divisions = nameof(Divisions);
        public const string Groups = nameof(Groups);
        public const string Students = nameof(Students);
        public const string Courses = nameof(Courses);
        public const string Enrollments = nameof(Enrollments);
        public const string Enrollments_Student = $"{Enrollments}.{Student}";
        public const string Enrollments_Course = $"{Enrollments}.{Course}";
        public const string DepartmentDivisions = nameof(DepartmentDivisions);
        public const string DepartmentDivisions_Department = $"{DepartmentDivisions}.{Department}";
        public const string DepartmentDivisions_Division = $"{DepartmentDivisions}.{Division}";
    }
}
