using Microsoft.EntityFrameworkCore;
using Organisation_Hierarchy_System.Core.Models;
using Organisation_Hierarchy_System.Core.Specifications;
using Organisation_Hierarchy_System.Core.ViewModels;
using System.Linq;

namespace Organisation_Hierarchy_System.Core.Repositories
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly SystemContext _systemContext;

        public EmployeeRepo(SystemContext systemContext)
        {
            _systemContext = systemContext;
        }
        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            await _systemContext.Employees.AddAsync(employee);
            return await _systemContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRole = await _systemContext.Employees.FindAsync(id);
            _systemContext.Employees.Remove(employeeRole);
            return await _systemContext.SaveChangesAsync() > 0;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _systemContext.Employees.Where(x => x.Id == id).Include(x => x.EmployeeRole).Include(x => x.Manager).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Employee>> GetEmployeeListAsync(SearchParams searchParams)
        {
            var result = await _systemContext.Employees.Include(p => p.EmployeeRole).Include(x => x.Manager).Where(x => (string.IsNullOrEmpty(searchParams.Search) || x.Name.ToLower().Contains(searchParams.Search.ToLower())) &&
            (!searchParams.EmployeeRoleId.HasValue || x.EmployeeRoleId == searchParams.EmployeeRoleId) &&
            (!searchParams.ManagerId.HasValue || x.ManagerId == searchParams.ManagerId)).ToListAsync();
            switch (searchParams.sort)
            {
                case "nameAsc":
                    result = result.OrderBy(p => p.Name).ToList();
                    break;
                case "nameDesc":
                    result = result.OrderByDescending(p => p.Name).ToList();
                    break;
                case "salaryAsc":
                    result = result.OrderBy(p => p.Salary).ToList();
                    break;
                case "salaryDesc":
                    result = result.OrderByDescending(p => p.Salary).ToList();
                    break;
                case "surnameAsc":
                    result = result.OrderBy(p => p.Surname).ToList();
                    break;
                case "surnameDesc":
                    result = result.OrderByDescending(p => p.Surname).ToList();
                    break;
                case "birthAsc":
                    result = result.OrderBy(p => p.BirthDate).ToList();
                    break;
                case "birthDesc":
                    result = result.OrderByDescending(p => p.BirthDate).ToList();
                    break;
                default:
                    result = result.OrderBy(n => n.Name).ToList();
                    break;
            }
            return result;
        }

        public async Task<IReadOnlyList<Employee>> GetEmployeeListAsync()
        {
            return await _systemContext.Employees.Include(p => p.EmployeeRole).ToListAsync();
        }

        public async Task<bool> UpdateEmployeeByAsync(Employee employee)
        {
            _systemContext.Employees.Update(employee);
            return await _systemContext.SaveChangesAsync() > 0;
        }

        //public async Task<IReadOnlyList<TreeNode>> GetTreeHierarchy()
        //{
        //    List<TreeNode> result = new List<TreeNode>();

        //    // Recieve the employees withou an manager
        //    var rootEmployees = await _systemContext.Employees.Include(x => x.Manager).Where(e => e.ManagerId == null).ToListAsync();

        //    // Build the tree
        //    foreach (var rootEmployee in rootEmployees)
        //    {
        //        result.Add(BuildHierarchy(rootEmployee));
        //    }
        //    return result;
        //}

        //private TreeNode BuildHierarchy(Employee employee)
        //{
        //    var treeNode = new TreeNode { ManagerName = $"{employee.Name} {employee.Surname}" };

        //    //Add employees recursively
        //    treeNode.Employees = BuildHierarchy(Employee employee);

        //    return treeNode;
        //}
    }
}
