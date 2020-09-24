using System;

namespace Teacher
{
    class Program
    {
        static void Main(string[] args)
        {
            int value1, value2;
            char operationSelector;
            
            if(args == null)
            {
                Console.WriteLine("Expecting 3 parameters, intigerValue1 integerValue2 operationSymbol");
            }
            else if(args.Length != 3)
            {
                Console.WriteLine("Expecting 3 parameters, intigerValue1 integerValue2 operationSymbol");
            }
            else
            {
                if(!Int32.TryParse(args[0], out value1))
                {
                    Console.WriteLine($"{args[0]} must be integer.");
                }

                if (!Int32.TryParse(args[1], out value2))
                {
                    Console.WriteLine($"{args[1]} must be integer.");
                }

                if(!char.TryParse(args[2], out operationSelector))
                {
                    Console.WriteLine($"{args[2]} symbol not have any corresponding operation. It must be + or - or * or /");
                }

                Helper helperObj = new Helper(value1, value2);

                if (helperObj.dictionaryObj.ContainsKey(operationSelector))
                {
                    Action operationToBeExecuted = helperObj.dictionaryObj[operationSelector];
                    operationToBeExecuted.Invoke();
                }
                else
                {
                    Console.WriteLine($"{args[2]} symbol not have any corresponding operation. It must be + or - or * or /");
                }
            }

            Console.ReadLine();
        }
    }
}
