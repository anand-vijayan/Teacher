using System;
using System.Collections.Generic;
using MenuTreeDemo.Middleware;
using MenuTreeDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MenuTreeDemo.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase()
        {
            GetMenu();
        }

        public string GetMenu()
        {
            Helper helperObj = new Helper();

            if (helperObj.menuItems != null)
            {
                List<TreeNode> treeNodes = helperObj.GetTreeNodes(helperObj.menuItems);

                if (treeNodes != null)
                {
                    helperObj.CreateMenuTree(treeNodes);

                    if (helperObj.MenuHtmlText != null)
                    {
                        return helperObj.MenuHtmlText.ToString();
                    }
                }
            }

            return string.Empty;
        }
    }
}
