namespace Organisation_Hierarchy_System.Core.Models
{
    public class TreeNode
    {
        public string ManagerName { get; set; }
        public List<TreeNode> Employees { get; set; } = new List<TreeNode>();
    }
}
