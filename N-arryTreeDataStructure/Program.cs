using System;
using System.Collections.Generic;

namespace N_arryTreeDataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper helperObj = new Helper();

            if(helperObj.menuItems != null)
            {
                List<TreeNode> treeNodes = helperObj.GetTreeNodes(helperObj.menuItems);

                if(treeNodes != null)
                {
                    helperObj.PrintTreeNodes(treeNodes);
                }

                Console.ReadLine();
            }
        }
    }
}
