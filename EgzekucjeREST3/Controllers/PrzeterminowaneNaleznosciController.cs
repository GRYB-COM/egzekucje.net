using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egzekucje.NET;
using Kszob.NET;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgzekucjeREST3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrzeterminowaneNaleznosciController : ControllerBase
    {
        KszobApplicationService applicationService;
        public PrzeterminowaneNaleznosciController(KszobApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpGet]
        public ActionResult<List<Kszob.NET.Naleznosc>> PobierzPrzeterminowaneNaleznosciNaBiezacyDzien()
        {
            return this.applicationService.PobierzPrzeterminowaneNaleznosciNaBiezacyDzien();
        }
    }
}
