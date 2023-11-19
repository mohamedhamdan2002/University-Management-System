using UMS.DataAccess.Data;
using UMS.DataAccess.Entities.Models;
using UMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace UMS.DataAccess.Repositories.Implementation
{
    internal sealed class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) 
            : base(context) { }
        public void CreateCourseForDivision(Guid divisionId, Course course)
        {
            course.DivisionId = divisionId;
            Create(course);
        }

        public void DeleteCourse(Course course)
            => Delete(course);

        public async Task<Course?> GetCourseAsync(Guid divisionId, Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(c => c.DivisionId == divisionId &&  c.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();

        public async Task<Course?> GetCourseAsync(Guid id, bool trackChanges, string[]? includes = null)
            => await GetByCondition(c => c.Id == id, trackChanges, includes)
            .SingleOrDefaultAsync();  
        public async Task<IEnumerable<Course>> GetCoursesAsync(Guid divisionId, bool trackChanges)
            => await GetByCondition(c => c.DivisionId == divisionId, trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
}
