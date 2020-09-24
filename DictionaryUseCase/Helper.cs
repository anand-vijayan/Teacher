using System;
using System.Collections.Generic;

namespace Teacher
{
    public class Helper
    {
        public Dictionary<char, Action> dictionaryObj;

        public int value1 { get; set; }
        public int value2 { get; set; }

        public Helper(int _val1, int _val2)
        {
            dictionaryObj = new Dictionary<char, Action>();
            PopulateDictionary();
            value1 = _val1;
            value2 = _val2;
        }

        private void PopulateDictionary()
        {
            dictionaryObj.Add('+', AddValues);
            dictionaryObj.Add('-', SubtractValues);
            dictionaryObj.Add('*', MultiplyValues);
            dictionaryObj.Add('/', DivideValues);
        }

        public void AddValues()
        {
            Console.WriteLine($"Output: {value1 + value2}");
        }

        public void SubtractValues()
        {
            Console.WriteLine($"Output: {value1 - value2}");
        }

        public void MultiplyValues()
        {
            Console.WriteLine($"Output: {value1 * value2}");
        }

        public void DivideValues()
        {
            Console.WriteLine($"Output: {value1 / value2}");
        }
    }
}
