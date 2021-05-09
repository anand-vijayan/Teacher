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

        private void GetMenu()
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
                        ViewBag.MenuHtmlText = helperObj.MenuHtmlText;
                    }
                    else
                    {
                        ViewBag.MenuHtmlText = string.Empty;
                    }
                }
                else
                {
                    ViewBag.MenuHtmlText = string.Empty;
                }
            }
            else
            {
                ViewBag.MenuHtmlText = string.Empty;
            }
        }
    }
}
