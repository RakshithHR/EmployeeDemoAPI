using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using EmployeeAPIPOC.Models;
using EmployeeAPIPOC.Repository;
using EmployeeAPIPOC.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmployeeAPIPOC.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPIPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;
        //private EmployeeDbContext _employeeDbContext;
        private IEmployeeService _employeeData;
        //  EmployeeService empService = new EmployeeService();

        public EmployeeController(IJwtAuth jwtAuth, IEmployeeService employeeData)
        {
            this.jwtAuth = jwtAuth;
            _employeeData = employeeData;
        }

        public EmployeeController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }

        //[HttpGet]
        //[Route("Get1")]
        //public IActionResult Get1()
        //{
        //    List<employee> employeesList = _employeeDbContext.GetAll.ToList();
        //    //employeesList = null;
        //    if (employeesList.Count > 0)
        //    {
        //        return Ok(employeesList);
        //    }
        //    else
        //    {
        //        return NotFound("No employees exists");
        //    }
        //}

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            List<Employee> employeesList = _employeeData.Get();
            //employeesList = null;
            if (employeesList.Count > 0)
            {
                return Ok(employeesList);
            }
            else
            {
                return NotFound("No employees exists");
            }
        }

        [HttpGet]
        [Route("GetBy")]
        public IActionResult GetBy(int id)
        {
            Employee employee = _employeeData.GetBy(id);
            if(employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound("No employee exist with id " + id);
            }
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Employee employee)
        {
            if (!string.IsNullOrEmpty(employee.EmpName))
            {
                List<Employee> employeesList = _employeeData.Create(employee);
                return Ok(employeesList);
            }
            else
            {
                return BadRequest();
            }
            
        }

        // PUT api/<employeeController>/5
        [HttpPut]
        [Route("Update")]
        public IActionResult Update(Employee employee)
        {
            List<Employee> employeesList = _employeeData.Update(employee);
            if (employeesList != null)
            {
                return Ok(employeesList);
            }
            else
            {
                return NotFound("employee not found with id " + employee.EmpID);
            }
        }

        [HttpPatch]
        [Route("UpdatePatch")]
        public IActionResult UpdatePatch(int id, [FromBody] JsonPatchDocument<Employee> employee)
        {
            List<Employee> employeesList = _employeeData.Get();
            Employee result = _employeeData.UpdateByPatch(id, employee);
            if (result == null)
            {
                return NotFound("employee not found with id " + id);
            }
            else
            {
                return Ok(result);
            }
        }

        // DELETE api/<employeeController>/5
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            List<Employee> employeesList = _employeeData.Delete(id);
            if (employeesList == null)
            {
                return NotFound("employee not found with id " + id);
            }
            else
            {
                //return Ok(employeesList);
                return Ok(new { Message = "Deleted" , ResultsAfterDeletion = employeesList});
            }
        }

        [HttpGet]
        [Route("GetActiveOrDeactiveemployees")]
        public IActionResult GetActiveOrDeactiveemployees(bool status)
        {
            List<Employee> employeesList = _employeeData.GetActiveOrDeactiveemployees(status);
            if (employeesList == null)
            {
                return NotFound("employee not found with status " + status);
            }
            else
            {
                return Ok(employeesList);
            }
        }

        //[AllowAnonymous]
        //// POST api/<MembersController>
        //[HttpPost("authentication")]
        //public IActionResult Authentication(string userName, string passWord)
        //{
        //    var token = jwtAuth.Authentication(userName, passWord);
        //    if (token == null)
        //        return Unauthorized();
        //    return Ok(token);
        //}

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public IActionResult Authentication(LoginModel loginModel)
        {
            bool isValid = _employeeData.IsValidUserInformation(loginModel);
            if (isValid)
            {
                var token = jwtAuth.Authentication(loginModel.UserName, loginModel.Password);
                if (token == null)
                    return Unauthorized();
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
