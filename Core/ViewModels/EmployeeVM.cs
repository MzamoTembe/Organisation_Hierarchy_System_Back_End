using System.ComponentModel.DataAnnotations;

namespace Organisation_Hierarchy_System.Core.ViewModels
{
    public class EmployeeVM
    {
        [Required]
        public int EmployeeNum { get; set; }
        [Required]
        public string ProfilePicture { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public int EmployeeRoleId { get; set; }
        public int? ManagerId { get; set; }
    }
}
