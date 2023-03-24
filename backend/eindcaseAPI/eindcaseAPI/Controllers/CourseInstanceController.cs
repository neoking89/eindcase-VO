using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eindcaseAPI.DAL;
using Microsoft.EntityFrameworkCore;

namespace eindcaseAPI.Controllers
{
    [Route("api/course-instances")]
    [ApiController]
    public class CourseInstanceController : ControllerBase
    {
        private readonly DataContext _context;

        public CourseInstanceController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseInstance>>> GetCourseInstances()
        {
            var courseInstances = await _context.CourseInstances.ToListAsync();
            if (courseInstances == null || !courseInstances.Any())
            {
                return NotFound("No course instances were found.");
            }
            return Ok(courseInstances);
        }

        [HttpPost]
        public async Task<ActionResult<CourseInstance>> CreateCourseInstance(CourseInstance courseInstance)
        {
            if (courseInstance == null)
            {
                return BadRequest("Course instance cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.CourseInstances.AddAsync(courseInstance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseInstanceById), new { id = courseInstance.Id }, courseInstance);
        }

        [HttpPut]
        public async Task<ActionResult<CourseInstance>> UpdateCourseInstance(CourseInstance courseInstance)
        {
            if (courseInstance == null)
            {
                return BadRequest("Course instance cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CourseInstances.Update(courseInstance);
            await _context.SaveChangesAsync();

            return Ok(courseInstance);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseInstance>> GetCourseInstanceById(int id)
        {
            var courseInstance = await _context.CourseInstances.FindAsync(id);

            if (courseInstance == null)
            {
                return NotFound();
            }

            return courseInstance;
        }
    }
}