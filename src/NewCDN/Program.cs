using AgileContent.BussinessLogic.Interface;
using System;

namespace NewCDN
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("New CDN App#\r");
            Console.WriteLine("Convert MINHA CDN to Agora#\r");
            Console.WriteLine("------------------------\n");
            INewCDN newCDN = new AgileContent.BussinessLogic.NewCDN();
            do
            {
                Console.WriteLine("Type a command to convert MINHA CDN To Agora, and then press Enter\n");
                Console.WriteLine("Command format: convert {sourceUrl} {outputPatH}\n");
                var input = Console.ReadLine();
                if (ValidCommand(input, out string sourceUrl, out string outputPath))
                {
                    newCDN.ValidOutPutPath(outputPath);
                    newCDN.ValidUrl(sourceUrl);
                    if (!newCDN.HasErrors)
                        newCDN.ConvertMyCdnToNow(sourceUrl, outputPath);
                    else
                        foreach (var item in newCDN.Errors)
                            Console.WriteLine($"ERROR: {item.ErrorMessage}");
                }
                else
                    Console.WriteLine("ERROR: Invalid Command\n");
                Console.WriteLine("(Press Esc to Stop)");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        static bool ValidCommand(string input, out string sourceUrl, out string outputPath)
        {
            bool validCommand = false;
            sourceUrl = string.Empty;
            outputPath = string.Empty;
            if (input.StartsWith("convert"))
            {
                var commandParams = input.Split(' ');
                if (commandParams.Length == 3)
                {
                    validCommand = !string.IsNullOrEmpty(commandParams[1]) && !string.IsNullOrEmpty(commandParams[2]);
                    if (validCommand)
                    {
                        sourceUrl = commandParams[1];
                        outputPath = commandParams[2];
                    }
                }
            }
            return validCommand;
        }
    }
}