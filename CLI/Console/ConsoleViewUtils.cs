using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzba.Console
{
    static class ConsoleViewUtils
    {
        public static int SafeInputInt(int input)
        {

            string rawInput = System.Console.ReadLine();

            while (!int.TryParse(rawInput, out input) || rawInput==null)
            {
                System.Console.WriteLine("Not a valid number, try again: ");

                rawInput = System.Console.ReadLine();
            }

            return input;
        }
        
        public static DateOnly SafeInputDateTime()
        {
            DateOnly input;

            string rawInput = System.Console.ReadLine() ?? string.Empty;

            while (!DateOnly.TryParse(rawInput, out input))
            {
                System.Console.WriteLine("Not a valid date, try again: ");

                rawInput = System.Console.ReadLine() ?? string.Empty;
            }

            return input;
        }
        /*public static string SafeInputString(string input)
        {
            string rawInput = System.Console.ReadLine();

            while(rawInput == null)
            {
                System.Console.WriteLine("Not a valid string, try again: ");
                rawInput = System.Console.ReadLine();
            }

            return input;
        }
        */
    }
}
