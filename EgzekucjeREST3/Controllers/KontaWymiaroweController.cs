using Kszob.NET;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgzekucjeREST3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KontaWymiaroweController : ControllerBase
    {
        private KszobApplicationService kszobApplicationService;
        public KontaWymiaroweController(KszobApplicationService kszobApplicationService)
        {
            this.kszobApplicationService = kszobApplicationService;
        }

        [HttpGet("{idAdresu}")]
        public ActionResult<List<Kszob.NET.IdKontaWAdresu>> Get(long idAdresu)
        {
            return this.kszobApplicationService.PobierzListeIdKontWAdresu(idAdresu);
        }

    }
}
