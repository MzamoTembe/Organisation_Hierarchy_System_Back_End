namespace Organisation_Hierarchy_System.Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string ProfilePicture { get; set; }
        public int EmployeeNum { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public int EmployeeRoleId { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
    }
}
