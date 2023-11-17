namespace EMS.DataAccess.Entities.Models
{
    public interface IStudents
    {
        ICollection<Student> Students { get; set; }
    }
}
