using System.ComponentModel.DataAnnotations;

namespace Organisation_Hierarchy_System.Core.ViewModels
{
    public class EmployeeRoleVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
