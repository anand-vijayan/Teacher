using System;
namespace N_aryTreeDataStructure
{
    public class MenuItem
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public int? ParentMenuId { get; set; }
    }
}
