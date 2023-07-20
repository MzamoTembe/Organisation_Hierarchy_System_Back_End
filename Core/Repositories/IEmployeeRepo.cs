using Organisation_Hierarchy_System.Core.Models;
using Organisation_Hierarchy_System.Core.Specifications;

namespace Organisation_Hierarchy_System.Core.Repositories
{
    public interface IEmployeeRepo
    {
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IReadOnlyList<Employee>> GetEmployeeListAsync(SearchParams searchParams);
        Task<IReadOnlyList<Employee>> GetEmployeeListAsync();
        //Task<IReadOnlyList<TreeNode>> GetTreeHierarchy();
        Task<bool> AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeByAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
