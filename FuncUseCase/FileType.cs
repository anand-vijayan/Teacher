using System;

namespace FuncUseCase
{
    public class FileType
    {
        public string FileTypeDescription { get; set; }
        public string SearchPattern { get; set; }
        public string SearchDirectory { get; set; }
        public Func<string, DateTime, bool> ValidationMethod { get; set; }
    }
}
