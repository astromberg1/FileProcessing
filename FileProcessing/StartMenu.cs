using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcess
{
   public static class StartMenu
    {

        internal static bool FileOrDirectoryExists(string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }

        private static bool Abort(string input)
        {
            if (input.ToLower() == "exit")
                return true;
            else
                return false;
        }

        public static void StartMeny()
        {
            while (true)
            {
                ConsoleWrite.MainMenu();
                var key = new ConsoleKeyInfo();
                key = Console.ReadKey(true);
                var input = key.KeyChar;
                Console.Clear();
                switch (input)
                {
                    case '1':
                        AngeRefdatum();
                        break;
                    case '2':
                        AngeInfilepath();
                        break;
                    case '3':
                        AngeUtfilepath();
                        break;
                    case '4':
                        Genererafiler();
                        break;
                    case '5':
                        return;
                    default:
                        ErrorInput();
                        break;
                }
            }
        }

        public static void AngeRefdatum()
        {
            var flag = true;
            var input = "";
            while (flag)
            {
                var menuString = $@"
ANGE REFERENSDATUM   Enter EXIT to Abort.

Referensdatum(YYYY-MM-DD):
";
                Console.WriteLine(menuString);
                input = Console.ReadLine();
                if (input == "")
                {
                    Console.WriteLine(@" default referensdatum används 2014-05-29");
                    input = "2014-05-29";
                    Console.ReadLine();
                }
                else
                {
                    if (Abort(input))
                        Console.Clear();
                    return;
                }

                try
                {
                    InitValues.Referensdatum = DateTime.Parse(input);

                    flag = false;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }

            }
            Console.Clear();
            ConsoleWrite.Success("Referens datum angivet till: " + InitValues.Referensdatum.ToString("yyyy-MM-dd"));
        }


        private static void AngeInfilepath()
        {
            bool flag = true;
            string input1 = "";
            string input2 = "";
            string _filepath = "";

            while (flag)
            {
                var menuString = $@"
ANGE SÖKVÄG och filenamn  Enter EXIT to Abort.

Ange först sökväg till filen som ska läsas in, ex C:\Fileprocessing\data\
";
                Console.WriteLine(menuString);
                input1 = Console.ReadLine();
                if (input1 == "")
                {
                    Console.WriteLine(@" default sökvägen används C:\Fileprocessing\data\");
                    input1 = @"C:\Fileprocessing\data\";
                }

                if (Abort(input1))
                {
                    Console.Clear();
                    return;
                }


                Console.WriteLine("Ange filnamn exempel: in.txt");

                input2 = Console.ReadLine();
                if (input2 == "")
                {
                    Console.WriteLine(@" default filnamn används: in.txt");
                    input2 = @"in.txt";
                    Console.ReadLine();
                }


                try
                {
                    _filepath = input1 + input2;
                    if (FileOrDirectoryExists(_filepath))
                    {
                        flag = false;
                        InitValues.InfilSokvagOchNamn = _filepath;
                    }
                    else
                    {
                        ConsoleWrite.Error("Filnamnet och Sökvägen stämmer ej " + _filepath);
                    }


                }
                catch (Exception ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }

            }
            Console.Clear();


            ConsoleWrite.Success("Filnamnet och Sökvägen stämmer: " + _filepath);
        }

        private static void AngeUtfilepath()
        {
            bool flag = true;
            string input1 = "";
            string input2 = "";
            string _filepath = "";

            while (flag)
            {
                var menuString = $@"
ANGE SÖKVÄG där resultat filerna ska hamna,  Enter EXIT to Abort.

Ange sökvägen där resultatfilerna ska hamna, ex C:\temp\ut\
";
                Console.WriteLine(menuString);
                input1 = Console.ReadLine();
                if (input1 == "")
                {
                    Console.WriteLine(@" default sökvägen används C:\temp\ut\");
                    input1 = @"C:\temp\ut\";
                    Console.ReadLine();
                }
                if (Abort(input1))
                {
                    Console.Clear();
                    return;
                }



                try
                {
                    _filepath = input1;
                    if (FileOrDirectoryExists(_filepath))
                    {
                        flag = false;
                        InitValues.UtfilSokVag = _filepath;
                    }
                    else
                    {
                        ConsoleWrite.Error("Sökvägen stämmer ej " + _filepath);
                    }


                }
                catch (Exception ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }

            }
            Console.Clear();


            ConsoleWrite.Success("Sökvägen stämmer: " + _filepath);
        }

        private static void Genererafiler()
        {


            if (!InitValues.CheckAllaVardenSatta())
            {
                ConsoleWrite.Error("Alla input värden är inte satta, " +
                                   InitValues.InfilSokvagOchNamn + " " +
                                   InitValues.UtfilSokVag + " " +
                                   InitValues.Referensdatum.ToString("yyyy-MM-dd"));
            }
            else
            {
                var readfileInstans = new ReadInFile();
                var resultread = readfileInstans.Readfile(InitValues.InfilSokvagOchNamn, InitValues.Referensdatum);
                if (resultread != "OK")
                    ConsoleWrite.Error(resultread);
                else
                {
                    var outfileInstans = new WriteFiles();
                    var resultwrite =
                        outfileInstans.WriteFile(InitValues.UtfilSokVag, readfileInstans.GetListTransactions());
                    if (resultwrite != "OK")
                        ConsoleWrite.Error(resultwrite);
                    else
                    {
                        ConsoleWrite.Success("Allt klart filerna är skapade");
                    }
                }
            }
        }



        private static void ErrorInput()
        {
            var menuString = $@"";

            Console.WriteLine(menuString);
        }


    }
}
