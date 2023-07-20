namespace Organisation_Hierarchy_System.Core.ViewModels
{
    public class EmployeeVM
    {
        public int EmployeeNum { get; set; }
        public string ProfilePicture { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public int EmployeeRoleId { get; set; }
        public int? ManagerId { get; set; }
    }
}
