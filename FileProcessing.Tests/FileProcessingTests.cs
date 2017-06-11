using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace FileProcessing.Tests
{
    [TestFixture]
    public class FileProcessingTests
    {
        private Transaction sut;


        [Test]
        [Category("Class Transaction Test")]
        public void TransactionShouldHaveTheCorrectValues()
        {
        sut = new Transaction("SEB", "2348023", new DateTime(2014,05,29), "10000", new DateTime(2014,05,29));
        Assert.That(sut.Bank,Is.EqualTo("SEB"));
        Assert.That(sut.Konto, Is.EqualTo("2348023"));
        Assert.That(sut.Datum, Is.EqualTo(new DateTime(2014, 05, 29)));
        Assert.That(sut.Refdatum, Is.EqualTo(new DateTime(2014, 05, 29)));
        Assert.That(sut.Typ, Is.EqualTo("ACTIVE"));
        }


    }
}
