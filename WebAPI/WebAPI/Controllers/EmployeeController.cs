using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataEntities.Models;
using DataEntities.Repository.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository<Employee> employeeRepository;

        public EmployeeController(IEmployeeRepository<Employee> employeeRepo)
        {
            employeeRepository = employeeRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Employee> employees = employeeRepository.GetAll();
            return Ok(new Result(HttpStatusCode.OK, employees));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!employeeRepository.IsExists(id))
            {
                return NotFound(new Result(HttpStatusCode.NotFound, null, Constants.EntityNotFound));
            }

            Employee employee = employeeRepository.Get(id);
            return Ok(new Result(HttpStatusCode.OK, employee));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody]Employee employee)
        {
            Employee result = employeeRepository.Add(employee);
            return Ok(new Result(HttpStatusCode.OK, result));
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody]Employee employee)
        {
            if (!employeeRepository.IsExists(id))
            {
                return NotFound(new Result(HttpStatusCode.NotFound, null, Constants.EntityNotFound));
            }

            Employee result = employeeRepository.Update(id, employee);
            return Ok(new Result(HttpStatusCode.OK, result));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!employeeRepository.IsExists(id))
            {
                return NotFound(new Result(HttpStatusCode.NotFound, null, Constants.EntityNotFound));
            }

            bool returnCode = employeeRepository.Delete(id);
            return Ok(new Result(HttpStatusCode.OK, returnCode));
        }
    }
}