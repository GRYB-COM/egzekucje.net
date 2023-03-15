using Egzekucje.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoSystem.Templates
{
    [TestClass]
    public class RtfTemplateTest
    {
        [TestMethod]
        public void ShouldGenerateTemplatedRtf()
        {
            Dictionary<string, object> context = new Dictionary<string, object>();

            String rtf = new RtfTemplate<Upomnienie>(@"Zasoby\upomnienie.rtf")
                .Context(new Upomnienie())
                .Macro("@uNumer", (c, p) => "=============", "Opis")
                .Macro("@uData", (c, p) => "-------------", "Inny opis")
                .Macro("@uPozycjeWgWzorca", (c, p) => "############", "Inny opis")
                // ...
                .Parse();

            Assert.IsTrue(rtf.Contains("============="));
            Assert.IsTrue(rtf.Contains("-------------"));
            Assert.IsTrue(rtf.Contains("############"));
            Assert.IsFalse(rtf.Contains("@uPozycjeWgWzorca"));
        }

        public string SampleMacro(object context, string param)
        {
            return string.Empty;
        }
    }
}
