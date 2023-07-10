using EmployeeCRUD.Models;
using EmployeeCRUD.Repository;
using EmployeeCRUD.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EmployeeCRUD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository employeeRepository;

        public EmployeesController(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
            employeeRepository.jsonPath = "Data/employees.json";
        }

        /// <summary>
        /// To return all existing employee details
        /// </summary>
        /// <returns>All employee details</returns>
        [HttpGet(Name = "GetAllEmployees")]
        public ActionResult GetAllEmployees()
        {
            EmployeeEntity employees = employeeRepository.GetEmployee();
            if (employees == null)
            {
                return Ok("No data found");
            }
            return Ok(employees);
        }

        /// <summary>
        /// To get existing employee detail by employee code
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>Return specific employee detail if exist else error will be displayed</returns>
        [HttpGet(Name = "GetEmployeeById")]
        public ActionResult GetEmployeeById(string employeeCode)
        {
            EmployeeModel employee = employeeRepository.GetEmployeeById(employeeCode);
            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return Ok("Invalid employee code");
            }
        }

        /// <summary>
        /// To add new employee detail into our system
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>confirmation message to user whether the new employee detail has been added or not</returns>
        [HttpPost(Name = "AddEmployee")]
        public ActionResult AddEmployee([FromBody] EmployeeModel employee)
        {
            string returnMsg = employeeRepository.AddEmployee(employee);
            return Ok(returnMsg);
        }

        /// <summary>
        /// To delete exisitng employee detail by employee code
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>confimration message to user whether the employee detail is deleted or not.</returns>
        [HttpDelete(Name = "DeleteEmployee")]
        public ActionResult DeleteEmployee([FromBody] string employeeCode)
        {
            string returnMsg = employeeRepository.DeleteEmployee(employeeCode);            
            return Ok(returnMsg);
        }

        /// <summary>
        ///  To update exisitng employee detail by employee code
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>confimration message to user whether the updated is done or not.</returns>
        [HttpPut(Name = "UpdateEmployee")]
        public ActionResult UpdateEmployee([FromBody] EmployeeModel employee)
        {
            string returnMsg = employeeRepository.UpdateEmployee(employee);
            return Ok(returnMsg);
        }
    }
}
