using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FuncUseCase
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper helperObj = new Helper();
            DateTime generatedDate = DateTime.Now.AddDays(-1);

            if(helperObj.FileTypes != null && helperObj.FileTypes.Count > 0)
            {
                foreach (var fileType in helperObj.FileTypes)
                {
                    if(Directory.Exists(fileType.SearchDirectory))
                    {
                        Console.WriteLine(helperObj.CheckAllFilesToFindValidFile(fileType.SearchDirectory, fileType.SearchPattern, generatedDate, fileType.ValidationMethod));
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
