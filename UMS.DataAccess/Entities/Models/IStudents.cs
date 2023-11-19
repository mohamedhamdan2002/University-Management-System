namespace UMS.DataAccess.Entities.Models
{
    public interface IStudents
    {
        ICollection<Student> Students { get; set; }
    }
}
