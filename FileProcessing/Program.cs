
using System;

namespace FileProcess
{
    class Program
    {
       
        static void Main(string[] args)
        {
           
            string filnamn = args[0];
            string utfilpath = args[1];
            char c = '\\';
            string str = c.ToString();
            if (!(utfilpath.EndsWith(str)))
            {
                utfilpath = utfilpath + str;

            }



            DateTime refdatum = DateTime.Parse(args[2]);

                var readfileInstans = new ReadInFile();
                var resultread = readfileInstans.Readfile(filnamn, refdatum);
                if (resultread != "OK")
                    ConsoleWrite.Error(resultread);
                else
                {
                    var outfileInstans = new WriteFiles();
                    var resultwrite =
                        outfileInstans.WriteFile(utfilpath, readfileInstans.GetListTransactions());
                    if (resultwrite != "OK")
                        ConsoleWrite.Error(resultwrite);
                    else
                    {
                        ConsoleWrite.Success("Allt klart filerna är skapade");
                    }
                }
            }

        }

    }

      
    

