using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eindcaseAPI.DAL
{
    public class CourseInstanceRepository : ICourseInstanceRepository
    {
        private readonly DataContext _context;

        public CourseInstanceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CourseInstance>> GetCourseInstancesAsync()
        {
            var courseInstances = await _context.CourseInstances.ToListAsync();
            return courseInstances;
        }

        public async Task<CourseInstance?> GetCourseInstanceByIdAsync(int id)
        {
            return await _context.CourseInstances.FindAsync(id);
        }

        public async Task CreateCourseInstanceAsync(CourseInstance courseInstance)
        {
            await _context.CourseInstances.AddAsync(courseInstance);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseInstance?> UpdateCourseInstanceAsync(CourseInstance courseInstance)
        {
            _context.CourseInstances.Update(courseInstance);
            await _context.SaveChangesAsync();

            return courseInstance;
        }

        public async Task<CourseInstance?> GetLastCourseInstanceAsync()
        {
            var lastCourseInstance = await _context.CourseInstances.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
            return lastCourseInstance;
        }

    }
}
