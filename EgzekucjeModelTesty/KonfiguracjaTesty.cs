using System;
using System.Collections.Generic;
using Egzekucje.NET;
using InfoSystem.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EgzekucjeModelTesty
{
    [TestClass]
    public class KonfiguracjaTesty
    {
        [TestMethod]
        public void PowinnaPobracKosztyUpomnienia() 
        {
            Assert.AreEqual(
                8.70m, 
                new Upomnienie(new Adresat(), new List<Zaleglosc>(), DateTime.Now).KosztUpomnienia);
        }

        [TestMethod]
        public void PowinnaWczytacSzczegolyTabeliUpomnienia()
        {
            var opisTabeli = OpisTabeli.WczytajOpisTabeli("upr.in");

            Assert.AreEqual(2, opisTabeli.IloscStopek);
            Assert.AreEqual(1, opisTabeli.OpisNaglowka.StylCzcionki);
            Assert.AreEqual(14, opisTabeli.OpisNaglowka.KolorTla);
            Assert.AreEqual(0, opisTabeli.OpisWiersza.StylCzcionki);
            Assert.AreEqual(12, opisTabeli.OpisWiersza.KolorTla);
            Assert.AreEqual(1, opisTabeli.OpisStopki.StylCzcionki);
            Assert.AreEqual(14, opisTabeli.OpisStopki.KolorTla);

            Assert.AreEqual("HEADER_LP", opisTabeli.KolumnyNaglowka["LP"].Nazwa);
            Assert.AreEqual(400, opisTabeli.KolumnyNaglowka["LP"].SzerokoscKolumny);
            Assert.AreEqual("<br><br>Lp.", opisTabeli.KolumnyNaglowka["LP"].Tresc);
            Assert.AreEqual(1, opisTabeli.KolumnyNaglowka["LP"].WyrownaniePoziome);

            Assert.AreEqual("ROW_NAZ_ZOB", opisTabeli.KolumnyWiersza["NAZ_ZOB"].Nazwa);
            Assert.AreEqual(3800, opisTabeli.KolumnyWiersza["NAZ_ZOB"].SzerokoscKolumny);
            Assert.AreEqual("@uFullNazZob()/@uPdstPrw() /@uStawkaVAT()", opisTabeli.KolumnyWiersza["NAZ_ZOB"].Tresc);
            Assert.AreEqual(3, opisTabeli.KolumnyWiersza["NAZ_ZOB"].WyrownaniePoziome);

            Assert.AreEqual("FOOTER_KWOTA_ODS", opisTabeli.KolumnyStopki["KWOTA_ODS"].Nazwa);
            Assert.AreEqual(1600, opisTabeli.KolumnyStopki["KWOTA_ODS"].SzerokoscKolumny);
            Assert.AreEqual("@uSumaZal()<br>@uRazemOds()<br>@uKoszty()", opisTabeli.KolumnyStopki["KWOTA_ODS"].Tresc);
            Assert.AreEqual(2, opisTabeli.KolumnyStopki["KWOTA_ODS"].WyrownaniePoziome);
        }
    }
}
