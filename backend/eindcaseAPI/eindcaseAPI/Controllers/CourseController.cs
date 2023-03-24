using eindcaseAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eindcaseAPI.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            var courses = await _courseRepository.GetCoursesAsync();

            if (courses == null || !courses.Any())
            {
                return NotFound("No course instances were found.");
            }

            return Ok(courses);
        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourseAsync(Course course)
        {
            if (course == null)
            {
                return BadRequest("Course instance cannot be null.");
            }

            if (course.Code==null)
            {
                return BadRequest("Coursecode cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var existingCourse = await _courseRepository.GetCourseByCode(course.Code);
            if (existingCourse != null)
            {
                return Ok(new
                {
                    message = $"A course with code {course.Code} already exists."
                });

            }

            await _courseRepository.PostCourseAsync(course);

            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course?>> GetCourseById(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);

            if (course == null)
            {
                return NotFound($"No course instance with id {id} was found.");
            }
            
            return course;
        }

        [HttpPut]
        public async Task<ActionResult<Course>> PutCourse(Course course)
        {
            if (course == null)
            {
                return BadRequest("Course instance cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _courseRepository.UpdateCourseAsync(course);

            return Ok(course);
        }
    }
}
