using eindcaseAPI.DAL;

public interface ICourseInstanceRepository
{
    Task<List<CourseInstance>> GetCourseInstancesAsync();
    Task<CourseInstance?> GetCourseInstanceByIdAsync(int id);
    Task CreateCourseInstanceAsync(CourseInstance courseInstance);
    Task<CourseInstance?> UpdateCourseInstanceAsync(CourseInstance courseInstance);
    Task<CourseInstance?> GetLastCourseInstanceAsync();
}
