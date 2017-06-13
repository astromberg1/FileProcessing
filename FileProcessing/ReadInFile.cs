using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcess
{

    public class ReadInFile
    {
        private List<Transaction> translista = new List<Transaction>();

        public string Readfile(string infil, DateTime refdatum)
        {
            string MessageResult = "OK";
            try
            {
                using (var fileReader = File.OpenText(infil))
                {


                    using (var csv = new CsvHelper.CsvReader(fileReader))
                    {
                        csv.Configuration.Delimiter = ";";

                        while (csv.Read())
                        {

                            string strKonto = csv.GetField<string>("Konto");
                            string strBelopp = csv.GetField<string>("Belopp");
                            if (!(decimal.TryParse(strBelopp, out decimal decBelopp)))
                            {
                                throw new ImportFileFormatException();
                            }
                            DateTime dtDatum = csv.GetField<DateTime>("Datum");
                            string strBank = csv.GetField<string>("Bank");
                            Transaction trans = new Transaction(strBank, strKonto, dtDatum, strBelopp, refdatum);
                            translista.Add(trans);
                        }
                    }
                }

            }
            catch (IOException e)
            {
                MessageResult = "Ett fel förekom när filen skulle läsas in." + e.Message;
            }

            catch (ImportFileFormatException)
            {
                MessageResult = "Ett fel förekom när filen skulle läsas in, beloppfältet har fel format.";
            }

            catch (Exception e)
            {
                MessageResult = e.Message;
            }


            return MessageResult;

        }

        public List<Transaction> GetListTransactions()
        {
            return translista;
        }

    }

}
