using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

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

";
            var menuString2 =
                $@"   
3. Generera bankfiler
4. Kolla inputfilen
5. Kolla output


6. EXIT
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



class Program
{
    private DateTime referensdatum;
   private  string filepath;
    private string filename;
    internal static bool FileOrDirectoryExists(string name)
    {
        return (Directory.Exists(name) || File.Exists(name));
    }

        public bool Abort(string input)
    {
        if (input.ToLower() == "exit")
            return true;
        else
            return false;
    }
        public void StartMenu()
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
                        Angefilepath();
                        break;
                    case '3':
                        Genererafiler();
                        break;
                    case '4':
                        KollaInput();
                        break;
                    case '5':
                        KollaOutput();
                        break;
                    case '6':
                        return;
                    default:
                        ErrorInput();
                        break;
                }
            }
        }

        public void AngeRefdatum()
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
                }

                if (Abort(input = Console.ReadLine()))
                { Console.Clear();
                    return; }

                try
                {
                    referensdatum = DateTime.Parse(input);
                    flag = false;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    ConsoleWrite.Error(ex.Message.ToString());
                }
                
            }
            Console.Clear();


            ConsoleWrite.Success("Referens datum angivet till: " + referensdatum.ToShortDateString());
        }


    private void Angefilepath()
    {


        bool flag = true;
        string input1 = "";
        string input2 = "";


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
                Console.WriteLine(@" default sökvägen används C:\Fileprocessing\data");
                input1 = @"C:\Fileprocessing\data\";
            }
            if (Abort(input1))
                { Console.Clear();
                    return;
                }

            Console.WriteLine("Ange filnamn exempel: in.txt");
            filepath = input2;


                try
            {
                
                        
                flag = false;
            }
            catch (Exception ex)
            {
                Console.Clear();
                ConsoleWrite.Error(ex.Message.ToString());
            }

        }
        Console.Clear();


        ConsoleWrite.Success("Referens datum angivet: " + referensdatum.ToShortDateString());
    }













        private void ErrorInput()
        {
            var menuString = $@"";

            Console.WriteLine(menuString);
        }

        static void Main(string[] args)
        {



            var translista = new List<Transaction>();
            DateTime RefDatum = new DateTime(2014, 05, 29);
            string textFile = @"C:\Fileprocessing\data\in.txt";
            string outPath = @"C:\temp\";
            using (var fileReader = File.OpenText(textFile))
            {
                using (var csv = new CsvHelper.CsvReader(fileReader))
                {
                    csv.Configuration.Delimiter = ";";

                    while (csv.Read())
                    {

                        var strKonto = csv.GetField<string>("Konto");
                        var strBelopp = csv.GetField<string>("Belopp");
                        var dtDatum = csv.GetField<DateTime>("Datum");
                        var strBank = csv.GetField<string>("Bank");

                        

                        var trans = new Transaction(strBank, strKonto, dtDatum, strBelopp, RefDatum);
                        translista.Add(trans);



                    }
                }
                fileReader.Close();
            }

            var banklista = translista.GroupBy(x => x.Bank).ToList();

            string[] arrayHeader = new string[] {"Konto", "Belopp", "Datum", "Typ"};
            foreach (var bank in banklista)
            {
                using (var fileWriter = File.CreateText(outPath+bank.Key + ".txt"))
                {
                    using (var csv = new CsvHelper.CsvWriter(fileWriter))
                    {
                        csv.Configuration.Delimiter = ";";


                        foreach (string t in arrayHeader)
                        {
                            csv.WriteField(t);
                        }
                        csv.NextRecord();
                        foreach (var trans in translista.Where(x => x.Bank == bank.Key))
                        {
                            csv.WriteField(trans.Konto);
                            csv.WriteField(trans.BeloppformatOn
                                ? trans.Belopp.ToString("+#;-#;0")
                                : trans.Belopp.ToString("0"));

                            csv.WriteField(trans.Datum.ToString("yyyy-MM-dd"));
                            csv.WriteField(trans.Typ);
                            
                            csv.NextRecord();
                          
                        }

                        fileWriter.Close();
                    }



                }




            }
        }

        public class Transaction
        {
            public string Bank { get; set; }

            public string Konto { get; set; }

            public DateTime Datum { get; set; }

            public Decimal Belopp { get; set; }

            public string Typ { get; set; }
            public DateTime Refdatum { get; set; }

            public bool BeloppformatOn { get; set; }
            public Transaction(string bank, string konto, DateTime datum, string belopp, DateTime refdatum)
            {
                this.Bank = bank;
                this.Konto = konto;
                this.Datum = datum;
                this.Belopp = decimal.Parse(belopp);
                if ((belopp.IndexOf('-') > -1) || (belopp.IndexOf('+') > -1))
                {
                    BeloppformatOn = true;
                }
                else
                {
                    BeloppformatOn = false;
                }

                this.Refdatum = refdatum;

                if (refdatum == datum)
                    this.Typ = "ACTIVE";
                else if (refdatum > datum)
                    this.Typ = "OLD";
                else
                    this.Typ = "FUTURE";


            }







        }

        }

    }

