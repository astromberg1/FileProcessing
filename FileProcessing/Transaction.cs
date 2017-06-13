using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcess
{
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


