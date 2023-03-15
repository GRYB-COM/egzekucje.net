using Egzekucje.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgzekucjeModelTesty.Skrypty
{
    [TestClass]
    public class PrzeksztalcNaleznosciWZaleglosci
    {
        [TestMethod]
        [Ignore]
        public void SkryptPrzeksztalcNaleznosciWZaleglosci()
        {
            var appService = new EgzApplicationService();
            appService.ZaktualizujZaleglosciNaDanyDzien();
        }
    }
}
