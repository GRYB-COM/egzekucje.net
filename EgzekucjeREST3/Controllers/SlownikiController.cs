using Kszob.NET;
using KszobModel;
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
    public class SlownikiController : ControllerBase
    {
        private KszobApplicationService kszobApplicationService;
        public SlownikiController(KszobApplicationService kszobApplicationService)
        {
            this.kszobApplicationService = kszobApplicationService;
        }

        [HttpGet("systemyWymiarowe")]
        public ActionResult<List<SystemWymiarowy>> PobierzSystemyWymiarowe()
        {
            return this.kszobApplicationService.PobierzSystemyWymiarowe();
        }

        [HttpGet("typyZobowiazan")]
        public ActionResult<List<TypZobowiazania>> PobierzTypyZobowiazan()
        {
            return this.kszobApplicationService.PobierzTypyZobowiazan();
        }
    }
}
