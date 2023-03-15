using Microsoft.VisualStudio.TestTools.UnitTesting;
using Egzekucje.NET.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egzekucje.NET.Tests
{
    [TestClass()]
    public class EgzAdapterJsonTests
    {
        EgzApplicationService egzApplicationService;
        const long ID_ADRESU = 1009;
        const long ID_OSOBY = 1009;
        public EgzAdapterJsonTests()
        {
            egzApplicationService = new EgzApplicationService();
            EgzekucjeAdapterJson.EgzApplicationService = egzApplicationService;
        }
        [TestMethod()]
        public void PobierzListeKontDlaAdresuTest()
        {

            var listaKontJSON = EgzekucjeAdapterJson.PobierzListeKontDlaAdresu(ID_ADRESU);
            Assert.AreEqual("[{\"ID_SYSTEMU\":4096,\"ID_KONTA_W\":10204,\"ID_ADRESU\":1009}]", listaKontJSON);
        }
        [TestMethod()]
        public void PobierzSumeZaleglosciOsobNaDzienBiezacyTest()
        {

            string listaKontJSON = EgzekucjeAdapterJson.PobierzSumyZaleglosciOsobNaDzienBiezacy();
            Assert.IsFalse(listaKontJSON.Contains("System.Data"));
        }

        [TestMethod()]
        public void PobierzAdresyAdresataTest()
        {
            string adresyJSON = EgzekucjeAdapterJson.PobierzAdresyAdresata(ID_OSOBY);
            Assert.IsFalse(adresyJSON.Contains("System.Data"));
        }
    }
}