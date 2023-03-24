using eindcaseAPI.Controllers;
using eindcaseAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eindcaseAPI.Tests
{
    public class CourseControllerTests
    {
        private ICourseRepository _courseRepository;
        private CourseController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .Options;

            var dbContext = new DataContext(options);

            _courseRepository = new CourseRepository(dbContext);
            _controller = new CourseController(_courseRepository);
        }

        [Test]
        public async Task GetCourses_ReturnsListOfCourses_WhenCoursesExist()
        {
            // Arrange
            var courses = new List<Course>
            {
                new Course {Code = "C#", Title = "C#" , Duration = 5},
                new Course {Code = "JAVA", Title = "Java" , Duration = 2}
            };
            foreach (var course in courses)
            {
                await _courseRepository.PostCourseAsync(course);
            }

            // Act
            var result = await _controller.GetCourses();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var courseList = okResult.Value as List<Course>;
            Assert.That(courseList.Count, Is.EqualTo(courses.Count));
            foreach (var course in courses)
            {
                Assert.IsTrue(courseList.Contains(course));
            }
        }

        [Test]
        public async Task PostCourseAsync_ReturnsOk_WhenSameCourseCodeAlreadyExists()
        {
            // Arrange
            var firstCourse = new Course { Code = "C#" };
            await _courseRepository.PostCourseAsync(firstCourse);
            var duplicateCourse = new Course { Code = "C#" };

            // Act
            var result = await _controller.PostCourseAsync(duplicateCourse);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            StringAssert.Contains("already exists.", okResult.Value.ToString());
        }


        [Test]
        public async Task PostCourseAsync_ReturnsBadRequest_WhenCourseIsNull()
        {
            // Arrange
            Course course = null;

            // Act
            var result = await _controller.PostCourseAsync(course);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo("Course instance cannot be null."));
        }

        [Test]
        public async Task PostCourseAsync_ReturnsBadRequest_WhenCourseCodeIsNull()
        {
            // Arrange
            var course = new Course { Id = 1, Code = null, Title = "C#", Duration = 5 };

            // Act
            var result = await _controller.PostCourseAsync(course);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.That(badRequestResult.Value, Is.EqualTo("Coursecode cannot be null."));
        }
    }
}