using Egzekucje.NET;
using Egzekucje.NET.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EgzekucjeModelTesty
{
    [TestClass]
    public class UpomnienieRepositoryTesty
    {
        [TestMethod]
        public void PowinnoZapisacUpomnienie()
        {
            EfUpomnieniaRepository repository = new EfUpomnieniaRepository();
            Upomnienie upomnienie = repository.Zapisz(
                new Upomnienie(new Adresat(1, 1), new List<Zaleglosc>(), DateTime.Now));

            Upomnienie upomnienieZBazy = repository.Pobierz(upomnienie.IdUpomnienia);

            Assert.AreEqual(upomnienie.IdUpomnienia, upomnienieZBazy.IdUpomnienia);
            Assert.AreEqual(8.70m, upomnienieZBazy.KosztUpomnienia);
        }
    }
}
