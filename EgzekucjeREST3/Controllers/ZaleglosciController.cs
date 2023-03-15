using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egzekucje.NET;
using Microsoft.AspNetCore.Mvc;

namespace EgzekucjeREST3.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ZaleglosciController : ControllerBase
    {
        EgzApplicationService applicationService;
        public ZaleglosciController(EgzApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpGet("{idOsoby}")]
        public ActionResult<List<Egzekucje.NET.Zaleglosc>> PobierzZaleglosciOsoby(long idOsoby)
        {
            return this.applicationService.PobierzZaleglosci(idOsoby);
        }

        [HttpGet]
        public ActionResult<List<Egzekucje.NET.Zaleglosc>> PobierzWszystkieZaleglosci()
        {
            return this.applicationService.PobierzWszystkieZaleglosci();
        }
    }
}
