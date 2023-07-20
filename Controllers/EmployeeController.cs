using Microsoft.AspNetCore.Mvc;
using Organisation_Hierarchy_System.Core.Models;
using Organisation_Hierarchy_System.Core.Repositories;
using Organisation_Hierarchy_System.Core.Specifications;
using Organisation_Hierarchy_System.Core.ViewModels;

namespace Organisation_Hierarchy_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _employeeRepo;

        public EmployeeController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Employee>>> GetEmployees([FromQuery] SearchParams specParams)
        {
            var employees = await _employeeRepo.GetEmployeeListAsync(specParams);
            var data = new List<Employee>(employees.Skip((specParams.PageIndex - 1) * specParams.PageSize).Take(specParams.PageSize).ToList());
            return Ok(new Pagination<Employee>(specParams.PageIndex, specParams.PageSize, employees.Count, data));
        }

        [HttpGet("employees")]
        public async Task<ActionResult<EmployeeVM>> GetEmployees()
        {
            var employees = await _employeeRepo.GetEmployeeListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _employeeRepo.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Employee>> AddEmployee(EmployeeVM employee)
        {
            Employee newEmployee = new Employee
            {
                EmployeeNum = employee.EmployeeNum,
                ProfilePicture = employee.ProfilePicture,
                Name = employee.Name,
                Surname = employee.Surname,
                BirthDate = employee.BirthDate,
                Salary = employee.Salary,
                EmployeeRoleId = employee.EmployeeRoleId,
            };
            if (employee.ManagerId != 0 || employee.ManagerId != null)
            {
                newEmployee.ManagerId = employee.ManagerId;
            }

            var result = await _employeeRepo.AddEmployeeAsync(newEmployee);
            if (result) return Ok(result);
            return BadRequest();

        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, EmployeeVM newEmployee)
        {
            if (newEmployee.ManagerId == id) return BadRequest();
            var employee = await _employeeRepo.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();

            employee.Name = newEmployee.Name;
            employee.Surname = newEmployee.Surname;
            employee.Salary = newEmployee.Salary;
            employee.BirthDate = newEmployee.BirthDate;
            employee.ProfilePicture = newEmployee.ProfilePicture;
            employee.ManagerId = newEmployee.ManagerId;
            employee.EmployeeRoleId = newEmployee.EmployeeRoleId;

            var result = await _employeeRepo.UpdateEmployeeByAsync(employee);
            if (result == true) return Ok(employee);
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var result = await _employeeRepo.DeleteEmployeeAsync(id);
            if (result == true) return Ok();
            return BadRequest();
        }

        //[HttpGet("tree")]
        //public async Task<ActionResult<List<TreeNode>>> GetHierarchyTree()
        //{
        //    var result = await _employeeRepo.GetTreeHierarchy();
        //    if (result == null) return BadRequest();
        //    return Ok(result);
        //}
    }
}
