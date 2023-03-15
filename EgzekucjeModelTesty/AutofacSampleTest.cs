using Autofac;
using Autofac.Core;
using Egzekucje.NET;
using Egzekucje.NET.Adapters;
using Egzekucje.NET.Bos;
using Egzekucje.NET.Kszob;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgzekucjeModelTesty
{

    [TestClass]
    public class AutofacSampleTest
    {
        [TestMethod]
        public void ShouldBuildContainerAndGetAppService() 
        {
            var autofac = new ContainerBuilder();
            autofac.RegisterType<EfUpomnieniaRepository>().As<UpomnieniaRepository>();
            autofac.RegisterType<EfZaleglosciRepository>().As<ZaleglosciRepository>();
            autofac.RegisterType<LocalKszobService>().As<KszobService>();
            autofac.RegisterType<LocalBosService>().As<BosService>();
            autofac.RegisterType<Egzekucje.NET.EgzApplicationService>();
            var container = autofac.Build();

            using (var scope = container.BeginLifetimeScope()) 
            {
                var service = container.Resolve<Egzekucje.NET.EgzApplicationService>();
            }
        }
    }
}
