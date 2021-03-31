using System;
namespace N_arryTreeDataStructure
{
    public class MenuItem
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        public int? ParentMenuId { get; set; }
    }
}
