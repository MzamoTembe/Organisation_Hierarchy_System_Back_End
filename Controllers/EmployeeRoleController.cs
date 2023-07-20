using Microsoft.AspNetCore.Mvc;
using Organisation_Hierarchy_System.Core.Models;
using Organisation_Hierarchy_System.Core.Repositories;
using Organisation_Hierarchy_System.Core.Specifications;
using Organisation_Hierarchy_System.Core.ViewModels;

namespace Organisation_Hierarchy_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeRoleController : ControllerBase
    {
        private readonly IEmployeeRoleRepo _employeeRole;

        public EmployeeRoleController(IEmployeeRoleRepo employeeRole)
        {
            _employeeRole = employeeRole;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<EmployeeRole>>> GetEmployeeRoles([FromQuery] SearchParams specParams)
        {
            var employeeRoles = await _employeeRole.GetEmployeeRoleListAsync(specParams);
            var data = new List<EmployeeRole>(employeeRoles.Skip((specParams.PageIndex - 1) * specParams.PageSize).Take(specParams.PageSize).ToList());
            return Ok(new Pagination<EmployeeRole>(specParams.PageIndex, specParams.PageSize, employeeRoles.Count, data));
        }

        [HttpGet("roles")]
        public async Task<ActionResult<EmployeeRole>> GetEmployeeRoles()
        {
            var employeeRoles = await _employeeRole.GetEmployeeRoleListAsync();
            return Ok(employeeRoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeRole>> GetEmployeeRole(int id)
        {
            var employeeRole = await _employeeRole.GetEmployeeRoleByIdAsync(id);

            if (employeeRole == null) return NotFound();
            return Ok(employeeRole);
        }

        [HttpPost("add")]
        public async Task<ActionResult<EmployeeRole>> AddEmployeeRole(EmployeeRoleVM employeeRole)
        {
            var result = await _employeeRole.AddEmployeeRoleAsync(new EmployeeRole
            {
                Name = employeeRole.Name,
                Description = employeeRole.Description,
            });
            if (result == true) return Ok(result);
            return BadRequest();
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<EmployeeRole>> UpdateEmployeeRole(EmployeeRole newEmployeeRole, int id)
        {
            var employeeRole = await _employeeRole.GetEmployeeRoleByIdAsync(id);
            if (employeeRole == null) return NotFound();

            employeeRole.Name = newEmployeeRole.Name;
            employeeRole.Description = newEmployeeRole.Description;

            var result = await _employeeRole.UpdateEmployeeRoleByAsync(employeeRole);
            if (result == true) return Ok(employeeRole);
            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<EmployeeRole>> DeleteEmployeeRole(int id)
        {
            var result = await _employeeRole.DeleteEmployeeRoleAsync(id);
            if (result == true) return Ok();
            return BadRequest();
        }
    }
}
