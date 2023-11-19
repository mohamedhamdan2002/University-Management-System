namespace UMS.DataAccess.Entities.Models
{
    public interface ICourses
    {
        ICollection<Course> Courses { get; set; }
    }
}
