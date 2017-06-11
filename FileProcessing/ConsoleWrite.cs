using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessing
{
    public static class ConsoleWrite
    {
        public static void Success(string input)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(input);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string input)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(input);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Heading(string input)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(input);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void MainMenu()
        {
            var menuString =
                $@"
1. Ange referensdatum
2. Ange sökväg och filnamn på filen som ska läsas in
3. Ange sökvägen där resultatfilerna ska hamna

";
            var menuString2 =
                $@"   
4. Generera bankfiler


5. EXIT
";
            ConsoleWrite.Heading("START MENY");
            Console.WriteLine(Environment.NewLine);
            ConsoleWrite.Heading("Input sektion");
            Console.WriteLine(menuString);
            ConsoleWrite.Heading("Exekverings sektion");
            Console.WriteLine(menuString2);
        }

        internal static void ErrorInput()
        {
            ConsoleWrite.Error("ERROR. fel input.");
        }
    }

}
