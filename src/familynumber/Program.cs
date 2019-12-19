using AgileContent.BussinessLogic;
using AgileContent.BussinessLogic.Interface;
using System;

namespace familynumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Family Number App#\r");
            Console.WriteLine("------------------------\n");
            do
            {
                Console.WriteLine("Type a number, and then press Enter");
                var input = Console.ReadLine();
                if (long.TryParse(input, out long number))
                {
                    IFamilyNumber familyNumber = new FamilyNumber();
                    var result = familyNumber.GetLargestFamilyNumber(number);
                    if (familyNumber.HasErrors)
                        foreach (var item in familyNumber.Errors)
                            Console.WriteLine($"ERROR: {item.ErrorMessage}");
                    else
                        Console.WriteLine($"Your result: {result}");
                }
                else
                    Console.WriteLine($"ERROR: Invalid input.");
                Console.WriteLine("(Press Esc to Stop)");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}