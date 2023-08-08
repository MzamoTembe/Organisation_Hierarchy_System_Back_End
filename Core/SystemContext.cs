using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection;
using Organisation_Hierarchy_System.Core.Models;

namespace Organisation_Hierarchy_System.Core
{
    public class SystemContext: DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    }
}
