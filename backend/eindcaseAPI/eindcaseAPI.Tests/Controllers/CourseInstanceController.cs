using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eindcaseAPI.Controllers;
using eindcaseAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;



    namespace eindcaseAPI.Tests
    {
        public class CourseInstanceControllerTests
        {
            private DataContext _dataContext;
            private CourseInstanceController _controller;

            [SetUp]
            public void Setup()
            {
                var options = new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("testdb")
                    .Options;

                _dataContext = new DataContext(options);
                _controller = new CourseInstanceController(_dataContext);
            }

            [TearDown]
            public void TearDown()
            {
                _dataContext.Dispose();
            }

            [Test]
            public async Task GetCourseInstances_ReturnsEmptyList_WhenNoCourseInstancesExist()
            {
                // Arrange
                // No course instances added to the in-memory database

                // Act
                var result = await _controller.GetCourseInstances();

                // Assert
                Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
                var notFoundResult = result.Result as NotFoundObjectResult;
                Assert.That(notFoundResult.Value, Is.EqualTo("No course instances were found."));
            }

            [Test]
            public async Task GetCourseInstances_ReturnsListOfCourseInstances_WhenCourseInstancesExist()
            {
                // Arrange
                var courseInstances = new List<CourseInstance>
            {
                new CourseInstance { Id = 1, Course = new Course { Code = "C#" }, StartDate = new DateTime(2022, 1, 1) },
                new CourseInstance { Id = 2, Course = new Course { Code = "JAVA" }, StartDate = new DateTime(2022, 2, 1) },
            };
                _dataContext.CourseInstances.AddRange(courseInstances);
                await _dataContext.SaveChangesAsync();

                // Act
                var result = await _controller.GetCourseInstances();

                // Assert
                Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
                var okResult = result.Result as OkObjectResult;
                Assert.That(okResult.Value, Is.EqualTo(courseInstances));
            }

            [Test]
            public async Task CreateCourseInstance_ReturnsBadRequest_WhenCourseInstanceIsNull()
            {
                // Arrange
                CourseInstance courseInstance = null;

                // Act
                var result = await _controller.CreateCourseInstance(courseInstance);

                // Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
                var badRequestResult = result.Result as BadRequestObjectResult;
                Assert.That(badRequestResult.Value, Is.EqualTo("Course instance cannot be null."));
            }

            [Test]
            public async Task CreateCourseInstance_ReturnsBadRequest_WhenModelStateIsInvalid()
            {
                // Arrange
                var courseInstance = new CourseInstance();

                // Add invalid model state
                _controller.ModelState.AddModelError("StartDate", "Start date is required");

                // Act
                var result = await _controller.CreateCourseInstance(courseInstance);

                // Assert
                Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
                var badRequestResult = result.Result as BadRequestObjectResult;
                Assert.IsInstanceOf<SerializableError>(badRequestResult.Value);
                var serializableError = badRequestResult.Value as SerializableError;

            }
        }
    }
