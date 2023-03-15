using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EgzekucjeModel.CodeGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EgzekucjeModelTesty.CodeGenerator
{
    [TestClass]
    public class JsonAdapterGeneratorTest
    {
        [TestMethod]
        public void ShouldGenerateJson()
        {
            var generator = new JsonAdapterGenerator();
            Assert.AreEqual("", generator.Generate());
        }
    }
}
