using eindcaseAPI.DAL;

public interface ICourseRepository
{
    Task<List<Course>> GetCoursesAsync();
    Task<Course?> GetCourseByIdAsync(int id);
    Task PostCourseAsync(Course course);
    Task<Course?> UpdateCourseAsync(Course course);
    Task<Course?> GetCourseByCode(string code);
}
