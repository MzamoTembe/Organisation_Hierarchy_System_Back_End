using Organisation_Hierarchy_System.Core.Models;
using Organisation_Hierarchy_System.Core.Specifications;

namespace Organisation_Hierarchy_System.Core.Repositories
{
    public interface IEmployeeRoleRepo
    {
        Task<EmployeeRole> GetEmployeeRoleByIdAsync(int id);
        Task<IReadOnlyList<EmployeeRole>> GetEmployeeRoleListAsync();
        Task<bool> AddEmployeeRoleAsync(EmployeeRole employeeRole);
        Task<IReadOnlyList<EmployeeRole>> GetEmployeeRoleListAsync(SearchParams searchParams);
        Task<bool> UpdateEmployeeRoleByAsync(EmployeeRole employeeRole);
        Task<bool> DeleteEmployeeRoleAsync(int id);
    }
}
