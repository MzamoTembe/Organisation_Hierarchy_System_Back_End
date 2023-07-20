using Microsoft.EntityFrameworkCore;
using Organisation_Hierarchy_System.Core.Models;
using Organisation_Hierarchy_System.Core.Specifications;
using Organisation_Hierarchy_System.Core.ViewModels;

namespace Organisation_Hierarchy_System.Core.Repositories
{
    public class EmployeeRoleRepo : IEmployeeRoleRepo
    {
        private readonly SystemContext _systemContext;

        public EmployeeRoleRepo(SystemContext systemContext)
        {
            _systemContext = systemContext;
        }

        public async Task<bool> AddEmployeeRoleAsync(EmployeeRole employeeRole)
        {
            await _systemContext.EmployeeRoles.AddAsync(employeeRole);
            return await _systemContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEmployeeRoleAsync(int id)
        {
            var result = await _systemContext.Employees.Where(x => x.EmployeeRoleId == id).ToListAsync();
            if (result.Count > 0) return false;

            var employeeRole = await _systemContext.EmployeeRoles.FindAsync(id);
            _systemContext.EmployeeRoles.Remove(employeeRole);
            return await _systemContext.SaveChangesAsync() > 0;
        }

        public async Task<EmployeeRole> GetEmployeeRoleByIdAsync(int id)
        {
            return await _systemContext.EmployeeRoles.FindAsync(id);
        }

        public async Task<IReadOnlyList<EmployeeRole>> GetEmployeeRoleListAsync()
        {
            return await _systemContext.EmployeeRoles.ToListAsync();
        }


        public async Task<IReadOnlyList<EmployeeRole>> GetEmployeeRoleListAsync(SearchParams searchParams)
        {
            var result = await _systemContext.EmployeeRoles.Where(x => string.IsNullOrEmpty(searchParams.Search) || x.Name.ToLower().Contains(searchParams.Search.ToLower()) || x.Description.ToLower().Contains(searchParams.Search.ToLower())).ToListAsync();
            switch (searchParams.sort)
            {
                case "nameAsc":
                    result = result.OrderBy(p => p.Name).ToList();
                    break;
                case "nameDesc":
                    result = result.OrderByDescending(p => p.Name).ToList();
                    break;
                default:
                    result = result.OrderBy(n => n.Name).ToList();
                    break;
            }
            return result;
        }

        public async Task<bool> UpdateEmployeeRoleByAsync(EmployeeRole employeeRole)
        {
            _systemContext.EmployeeRoles.Update(employeeRole);
            return await _systemContext.SaveChangesAsync() > 0;
        }
    }
}
