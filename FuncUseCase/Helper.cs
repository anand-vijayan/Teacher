using System;
using System.Collections.Generic;
using System.IO;

namespace FuncUseCase
{
    public class Helper
    {
        public List<FileType> FileTypes { get; set; }

        public Helper()
        {
            PopulateExpectedFileTypeList();
        }

        public void PopulateExpectedFileTypeList()
        {
            string searchDirectory = @"/Users/anandvijayan/Projects/Teacher/FuncUseCase/Files";

            FileTypes = new List<FileType>
            {
                new FileType() { FileTypeDescription = "FILE_TYPE_1", SearchPattern = "FT1*", SearchDirectory = searchDirectory, ValidationMethod = IsValidFileType1 },
                new FileType() { FileTypeDescription = "FILE_TYPE_2", SearchPattern = "FT2*", SearchDirectory = searchDirectory, ValidationMethod = IsValidFileType2 }
            };
        }

        public bool IsValidFileType1(string availableFileName, DateTime generatedDate)
        {
            bool flag = false;

            if (File.Exists(availableFileName))
            {
                using(StreamReader fileType1Reader = File.OpenText(availableFileName))
                {
                    //Since only need to check the second line, i am not using any loop for reading the line.
                    string lineData = fileType1Reader.ReadLine();
                    lineData = fileType1Reader.ReadLine();

                    if(!lineData.StartsWith("02"))
                    {
                        Console.WriteLine($"Valid indicator didn't found for : {availableFileName}");
                    }
                    else
                    {
                        //The generated date will start from 3rd character, example: 0209242020
                        if (lineData.Length >= 8)
                        {
                            string generatedDateFromFile = lineData.Substring(2, 8);

                            if (!string.IsNullOrEmpty(generatedDateFromFile) && generatedDateFromFile.Equals(generatedDate.ToString("MMddyyyy")))
                            {
                                flag = true;
                            }
                            else
                            {
                                Console.WriteLine($"Second line don't have date in proper format MMddyyyy for : {availableFileName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Second line don't have date in proper format MMddyyyy for : {availableFileName}");
                        }
                    }
                }
            }

            return flag;
        }

        public bool IsValidFileType2(string availableFileName, DateTime generatedDate)
        {
            bool flag = false;

            if (File.Exists(availableFileName))
            {
                using (StreamReader fileType1Reader = File.OpenText(availableFileName))
                {
                    //Since only need to check the first line, i am not using any loop for reading the line.
                    string lineData = fileType1Reader.ReadLine();

                    //The generated date will be the first line, example: 09/24/2020
                    if (!string.IsNullOrEmpty(lineData) && lineData.Length == 10)
                    {
                        if (lineData.Equals(generatedDate.ToString("MM/dd/yyyy")))
                        {
                            flag = true;
                        }
                        else
                        {
                            Console.WriteLine($"First line have a different date than expected for : {availableFileName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"First line don't have date in proper format MM/dd/yyyy for : {availableFileName}");
                    }
                }
            }

            return flag;
        }

        public string CheckAllFilesToFindValidFile(string fileSourcePath, string searchPattern, DateTime generatedDate, Func<string, DateTime, bool> validationMethod)
        {
            string validFileFullName = string.Empty;
            string[] availableFiles = Directory.GetFiles(fileSourcePath, searchPattern);

            if(availableFiles != null && availableFiles.Length > 0)
            {
                foreach (var availableFile in availableFiles)
                {
                    if (validationMethod(availableFile, generatedDate))
                    {
                        validFileFullName = $"Valid file name is : {availableFile}";
                        break;
                    }
                }
            }

            return validFileFullName;
        }
    }
}
