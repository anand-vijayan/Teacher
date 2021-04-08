using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MenuTreeDemo.Models;

namespace MenuTreeDemo.Middleware
{
    public class Helper
    {
        public int ItemLevel { get; set; } = 0;
        public StringBuilder MenuHtmlText { get; set; } = new StringBuilder();

        public List<MenuItem> menuItems;
        private string menuItemHtmlText = string.Empty;

        private const string END_TAGS = @"</ul></li>";

        public Helper()
        {
            PopulateMenuItems();
        }

        private void PopulateMenuItems()
        {
            menuItems = new List<MenuItem>
            {
                new MenuItem { MenuId = 1, MenuName = "Main Group 1", MenuLink = string.Empty, ParentMenuId = null },
                new MenuItem { MenuId = 4, MenuName = "Sub Group 3", MenuLink = string.Empty, ParentMenuId = 1 },
                new MenuItem { MenuId = 3, MenuName = "Sub Group 2", MenuLink = string.Empty, ParentMenuId = 1 },
                new MenuItem { MenuId = 2, MenuName = "Sub Group 1", MenuLink = string.Empty, ParentMenuId = 1 },
                new MenuItem { MenuId = 6, MenuName = "Feature 2", MenuLink = "/main1/sub1/feature2", ParentMenuId = 2 },
                new MenuItem { MenuId = 5, MenuName = "Feature 1", MenuLink = "/main1/sub1/feature1", ParentMenuId = 2 },
                new MenuItem { MenuId = 7, MenuName = "Feature 3", MenuLink = "/main1/sub2/feature3", ParentMenuId = 3 },
                new MenuItem { MenuId = 8, MenuName = "Feature 4", MenuLink = "/main1/sub2/feature4", ParentMenuId = 3 },
                new MenuItem { MenuId = 9, MenuName = "Feature 5", MenuLink = "/main1/sub3/feature5", ParentMenuId = 4 },
                new MenuItem { MenuId = 10, MenuName = "Feature 6", MenuLink = "/main1/sub3/feature6", ParentMenuId = 4 },
                new MenuItem { MenuId = 13, MenuName = "Sub Group 2", MenuLink = string.Empty, ParentMenuId = 11 },
                new MenuItem { MenuId = 12, MenuName = "Sub Group 1", MenuLink = string.Empty, ParentMenuId = 11 },
                new MenuItem { MenuId = 14, MenuName = "Sub Group 3", MenuLink = string.Empty, ParentMenuId = 11 },
                new MenuItem { MenuId = 11, MenuName = "Main Group 2", MenuLink = string.Empty, ParentMenuId = null },
                new MenuItem { MenuId = 16, MenuName = "Feature 2", MenuLink = "/main2/sub1/feature2", ParentMenuId = 12 },
                new MenuItem { MenuId = 15, MenuName = "Feature 1", MenuLink = "/main2/sub1/feature1", ParentMenuId = 12 },
                new MenuItem { MenuId = 17, MenuName = "Feature 3", MenuLink = "/main2/sub2/feature3", ParentMenuId = 13 },
                new MenuItem { MenuId = 18, MenuName = "Feature 4", MenuLink = "/main2/sub2/feature4", ParentMenuId = 13 },
                new MenuItem { MenuId = 19, MenuName = "Feature 5", MenuLink = "/main2/sub3/feature5", ParentMenuId = 14 },
                new MenuItem { MenuId = 20, MenuName = "Feature 6", MenuLink = "/main2/sub3/feature6", ParentMenuId = 14 }
            };
        }

        private TreeNode GetParentNode(List<TreeNode> treeNodes, int? parentMenuId)
        {
            ItemLevel++;

            if (treeNodes != null)
            {
                foreach (var treeItem in treeNodes)
                {
                    if (treeItem.MenuId == parentMenuId)
                    {
                        return treeItem;
                    }
                    else if (treeItem.ChildrenNodes != null)
                    {
                        TreeNode tempTreeNode = GetParentNode(treeItem.ChildrenNodes, parentMenuId);

                        if (tempTreeNode != null)
                        {
                            return tempTreeNode;
                        }
                    }
                }
            }

            //if control reaches here then we have an un-successful search
            ItemLevel--;
            return null;
        }

        private bool AddChild(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode != null && childNode != null)
            {
                if (parentNode.ChildrenNodes == null) parentNode.ChildrenNodes = new List<TreeNode>();

                childNode.ItemLevel = ItemLevel;
                parentNode.ChildrenNodes.Add(childNode);
                childNode.ParentNode = parentNode;

                return true;
            }

            return false;
        }

        public List<TreeNode> GetTreeNodes(List<MenuItem> menuItems)
        {
            List<TreeNode> treeNodes = new List<TreeNode>();
            TreeNode currentNode, parentTreeNode;

            foreach (var menuItem in menuItems.OrderBy(m => m.MenuId).OrderBy(m => m.ParentMenuId))
            {
                currentNode = new TreeNode { MenuId = menuItem.MenuId, MenuName = menuItem.MenuName, MenuLink = menuItem.MenuLink, ParentNode = null };

                if (treeNodes != null && menuItem.ParentMenuId == null)
                {
                    currentNode.ItemLevel = 0;
                    treeNodes.Add(currentNode);
                }
                else if (treeNodes != null && treeNodes.Count > 0)
                {
                    ItemLevel = 0;

                    parentTreeNode = GetParentNode(treeNodes, menuItem.ParentMenuId);

                    if (parentTreeNode != null)
                    {
                        AddChild(parentTreeNode, currentNode);
                    }
                    else
                    {
                        throw new Exception("Menu Items doesn't sorted based on MenuId and then by ParentMenuId");
                    }
                }
                else
                {
                    throw new Exception("Menu Items doesn't have a root menu (with ParentMenuId NULL)");
                }
            }

            return treeNodes;
        }

        public void CreateMenuTree(List<TreeNode> treeNodes)
        {
            if (treeNodes != null)
            {
                foreach (var treeNodeItem in treeNodes)
                {
                    if(treeNodeItem.ChildrenNodes != null && treeNodeItem.ChildrenNodes.Count > 0)
                    {
                        menuItemHtmlText = $"<li><span class='caret'>{treeNodeItem.MenuName}</span><ul class='nested'>";
                        MenuHtmlText.Append(menuItemHtmlText);

                        CreateMenuTree(treeNodeItem.ChildrenNodes);

                        MenuHtmlText.Append(END_TAGS);
                    }
                    else
                    {
                        menuItemHtmlText = $"<li><a href='{treeNodeItem.MenuLink}'>{treeNodeItem.MenuName}</a></li>";
                        MenuHtmlText.Append(menuItemHtmlText);
                    }
                }
            }
        }
    }
}
