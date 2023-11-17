namespace EMS.DataAccess.Entities.Models
{
    public interface ICourses
    {
        ICollection<Course> Courses { get; set; }
    }
}
