using System.Collections.Generic;

namespace MenuTreeDemo.Models
{
    public class TreeNode
    {
        //Values
        public int ItemLevel { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }

        //Pointer to Parent
        public TreeNode ParentNode { get; set; }

        //Pointers to children
        public List<TreeNode> ChildrenNodes { get; set; }
    }
}
