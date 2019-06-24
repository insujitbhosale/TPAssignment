using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataEntities;
using DataEntities.Models;
using DataEntities.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Models;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository<Department> departmentRepository;

        public DepartmentsController(IDepartmentRepository<Department> deptRepo)
        {
            departmentRepository = deptRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Department> departments = departmentRepository.GetAll();
            return Ok(new Result(HttpStatusCode.OK, departments));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!departmentRepository.IsExists(id))
            {
                return NotFound(new Result(HttpStatusCode.NotFound, null, Constants.EntityNotFound));
            }

            Department department = departmentRepository.Get(id);
            return Ok(new Result(HttpStatusCode.OK, department));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Department department)
        {
            Department result = departmentRepository.Add(department);
            return Ok(new Result(HttpStatusCode.OK, result));
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody]Department department)
        {
            if (!departmentRepository.IsExists(id))
            {
                return NotFound(new Result(HttpStatusCode.NotFound, null, Constants.EntityNotFound));
            }

            Department result = departmentRepository.Update(id, department);
            return Ok(new Result(HttpStatusCode.OK, result));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!departmentRepository.IsExists(id))
            {
                return NotFound(new Result(HttpStatusCode.NotFound, null, Constants.EntityNotFound));
            }

            bool returnCode = departmentRepository.Delete(id);
            return Ok(new Result(HttpStatusCode.OK, returnCode));
        }
    }
}
