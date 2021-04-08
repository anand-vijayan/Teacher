using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MenuTreeDemo.Models;
using MenuTreeDemo.Middleware;

namespace MenuTreeDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MenuTree()
        {
            Helper helperObj = new Helper();

            if (helperObj.menuItems != null)
            {
                List<TreeNode> treeNodes = helperObj.GetTreeNodes(helperObj.menuItems);

                if (treeNodes != null)
                {
                    helperObj.CreateMenuTree(treeNodes);

                    if(helperObj.MenuHtmlText != null)
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

            return View();
        }
    }
}
