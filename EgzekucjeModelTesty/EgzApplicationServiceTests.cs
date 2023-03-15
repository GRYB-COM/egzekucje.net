using Egzekucje.NET;
using Egzekucje.NET.Adapters;
using Egzekucje.NET.Kszob;
using InfoSystem.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Egzekucje.NET.Tests
{
    [TestClass()]
    public class EgzApplicationServiceTests
    {
        public bool DEBUG = true;
        private const long ID_OSOBY = 1009;
        private UpomnieniaRepository upomnieniaRepository = new EfUpomnieniaRepository();
        private ZaleglosciRepository zaleglosciRepository = new EfZaleglosciRepository();
        private EgzApplicationService egzApplicationService = new EgzApplicationService();
        [TestMethod]
        public void PobierzDaneAdresataTest()
        {

            var adresat = egzApplicationService.PobierzDaneAdresata(1009, 1009);

            Assert.IsNotNull(adresat);
            Assert.AreEqual(1009, adresat.IdAdresu);
            Assert.AreEqual(1009, adresat.IdAdresu);
            Assert.AreEqual("NOWAK", adresat.Nazwisko);
        }
        [TestMethod]
        public void PobierzAdresyAdresataTest()
        {
            var adresy = egzApplicationService.PobierzAdresyAdresata(1009);
            Assert.AreNotEqual(0, adresy.Count);
            Assert.AreEqual(1009, adresy[0].IdAdresu);
            Assert.AreEqual("KOLBUSZOWA", adresy[0].Miejscowosc);
            Assert.AreEqual("Plac Wolności", adresy[0].Ulica);
        }

        [TestMethod]
        public void WystawPojedynczeUpomnienie()
        {
            upomnieniaRepository.UsunPoIdOsoby(ID_OSOBY);

            Upomnienie upomnienie = egzApplicationService.WystawPojedynczeUpomnienie(ID_OSOBY, ID_OSOBY, new List<Zaleglosc>(), DateTime.Now);

            Upomnienie zBazy = upomnieniaRepository.Pobierz(upomnienie.IdUpomnienia);

            Assert.IsNotNull(zBazy);
            Assert.AreEqual(zBazy.Adresat.IdAdresu, 1009);
            Assert.AreEqual(zBazy.Adresat.IdOsoby, 1009);
            Assert.AreEqual(zBazy.KosztUpomnienia, 8.7m);
        }

        [TestMethod]
        public void WyciagnijNaleznosciIZamienNaZaleglosci()
        {
            zaleglosciRepository.UsunPoIdOsoby(ID_OSOBY);

            egzApplicationService.PrzeksztalcNaleznosciNaZaleglosci(ID_OSOBY);

            var zaleglosci = egzApplicationService.PobierzZaleglosci(ID_OSOBY);
            Assert.AreEqual(4, zaleglosci.Count);
            Assert.AreEqual(1009, zaleglosci[0].IdOsoby);
        }

        [TestMethod]
        public void GenerujDokumentUpomnienia()
        {
            upomnieniaRepository.UsunPoIdOsoby(ID_OSOBY);
            zaleglosciRepository.UsunPoIdOsoby(ID_OSOBY);

            egzApplicationService.PrzeksztalcNaleznosciNaZaleglosci(ID_OSOBY);
            List<Zaleglosc> zaleglosci = egzApplicationService.PobierzZaleglosci(ID_OSOBY);
            Upomnienie upomnienie = egzApplicationService.WystawPojedynczeUpomnienie(ID_OSOBY, ID_OSOBY, zaleglosci, DateTime.Now);

            string dokument = egzApplicationService.PobierzDokumentUpomnienia(upomnienie.IdUpomnienia);

            new List<string>
            {
                "@uNumer",
                "@uRok",
                "@uData",
                "@uNazwa",
                "@uUlica",
                "@uMiasto",
                "@uPesReg",
                "@uPozycjeWgWzorca",
                "@uStawkaOds"
            }
            .ForEach(m => Assert.IsFalse(dokument.Contains(m), "Macro not repaced: " + m));

            new List<string>
            {
                upomnienie.DataUpomnienia.Year.ToString(),
                "NOWAK MAŁGORZATA",
                "Plac Wolności",
                "5607040011",
                "KOLBUSZOWA",
                "8,70"
            }
            .ForEach(m => Assert.IsTrue(dokument.Contains(m), "Replacement value not found: " + m));

            if (DEBUG)
            {
                string nazwaPliku = "upomnienie_wypelnione.rtf";
                File.WriteAllText(nazwaPliku, dokument, Encoding.Default);
            }
        }

        [TestMethod]
        public void GenerujTabele()
        {
            upomnieniaRepository.UsunPoIdOsoby(ID_OSOBY);
            zaleglosciRepository.UsunPoIdOsoby(ID_OSOBY);

            egzApplicationService.PrzeksztalcNaleznosciNaZaleglosci(ID_OSOBY);
            List<Zaleglosc> zaleglosci = egzApplicationService.PobierzZaleglosci(ID_OSOBY);
            Upomnienie upomnienie = egzApplicationService.WystawPojedynczeUpomnienie(ID_OSOBY, ID_OSOBY, zaleglosci, DateTime.Now);

            var szablonZaleglosci = new RtfTemplate<Zaleglosc>()
                .Macro("@uLP", (z, p) => "1", "Lp")
                .Macro("@uRataRokObr", (z, p) => z.TerminPlatnosci.Year.ToString(), "Rok obrotowy")
                .Macro("@uKwotaPoz", (z, p) => z.KwotaZaleglosci.ToString("n2"), "Kwota zaległości")
                .Macro("@uDataOds", (z, p) => z.TerminPlatnosci.ToString("dd-MM-yyyy"), "Termin płatności")
                .Macro("@uOdsPoz", (z, p) => z.KwotaOdsetek.ToString("n2"), "Kwota odseteK")
                .Macro("@uFullNazZob", (z, p) => "Nazwa zob.", "Nazwa zobowiązania")
                .Macro("@uPdstPrw", (z, p) => "Podst. prawna", "Podstwa prawna")
                .Macro("@uStawkaVAT", (z, p) => "23%", "Stawka VAT");

            string rtfTable = RtfTemplate<Zaleglosc>.GenerujTabele(upomnienie.Zaleglosci, szablonZaleglosci, "upr.in");

            if (DEBUG)
            {
                string nazwaPliku = "Tabela.rtf";
                File.WriteAllText(nazwaPliku, rtfTable, Encoding.Default);
            }

            // sprawdzamy poczatek tabeli (piersze 6 znakow)
            Assert.AreEqual(@"\pard", rtfTable.Substring(0, 5));
            // i koniec tabeli
            Assert.AreEqual(@"\par", rtfTable.Substring(rtfTable.Length - 4, 4));
        }


        [TestMethod()]
        public void PobierzListeKontDlaAdresuTest()
        {
            Assert.AreEqual(10204, egzApplicationService.PobierzListeKontDlaAdresu(ID_OSOBY)[0].IdKontaW);
        }

        [TestMethod()]
        public void PobierzSumyZaleglosciOsobNaDzienBiezacyTest()
        {
            var zaleglosciOsoby = egzApplicationService.PobierzSumyZaleglosciOsobNaDzienBiezacy();
            Assert.AreNotEqual(0, zaleglosciOsoby.Count);
        }

      }

}