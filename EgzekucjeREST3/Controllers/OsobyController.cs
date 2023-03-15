using System.Collections.Generic;
using BOS.NET;
using Microsoft.AspNetCore.Mvc;

namespace EgzekucjeREST3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsobyController : ControllerBase
    {
        private BosApplicationService bosApplicationService;
        public OsobyController(BosApplicationService bosApplicationService)
        {
            this.bosApplicationService = bosApplicationService;
        }

        [HttpGet("{idOsoby}/adresy/{idAdresu}")]
        public ActionResult<BOS.NET.Osoba> PobierzDaneOsoby(long idOsoby, long idAdresu)
        {
            return this.bosApplicationService.PobierzDaneOsoby(idOsoby, idAdresu);
        }
        [HttpGet("{idOsoby}/adresy")]
        public ActionResult<List<BOS.NET.Adres>> PobierzAdresyOsoby(long idOsoby)
        {
            return this.bosApplicationService.PobierzAdresyOsoby(idOsoby);
        }

        [HttpGet]
        public ActionResult<List<BOS.NET.Osoba>> PobierzWszystkieOsoby()
        {
            return this.bosApplicationService.PobierzWszystkieOsoby();
        }
    }
}
