using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessing
{
    public class WriteFiles
    {
        private string outPath;

        private List<Transaction> translista;

        public string WriteFile(string outpath, List<Transaction> inlista)
        {
            string MessageResult = "OK";

            try
            {
                translista = inlista;
                outPath = outpath;
              
                var banklista = translista.GroupBy(x => x.Bank).ToList();

                string[] arrayHeader = new string[] {"Konto", "Belopp", "Datum", "Typ"};
                foreach (var bank in banklista)
                {
                    using (var fileWriter = File.CreateText(outPath  + bank.Key + ".txt"))
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


                        }



                    }

                }

            }
            catch (Exception e)
            {
                MessageResult = e.Message;
            }


            return MessageResult;


        }
    }
}
