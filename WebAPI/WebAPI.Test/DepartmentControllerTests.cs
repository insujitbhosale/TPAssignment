using DataEntities.Models;
using DataEntities.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;

namespace Tests
{

    public class Tests
    {
        public DepartmentsController departmentController { get; set; }
        private Mock<IDepartmentRepository<Department>> mocRepo;

        [SetUp]
        public void Setup()
        {
            mocRepo = new Mock<IDepartmentRepository<Department>>();
            departmentController = new DepartmentsController(mocRepo.Object);
        }

        [Test]
        public void GetDepartment_OK()
        {
            var result = departmentController.Get();
            var okResult = result as OkObjectResult;
            Assert.IsInstanceOf(typeof(OkObjectResult), okResult);
        }
    }
}