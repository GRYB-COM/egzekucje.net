using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egzekucje.NET;
using Egzekucje.NET.Adapters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EgzekucjeREST3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpomnieniaController : ControllerBase
    {
        EgzApplicationService applicationService;
        public UpomnieniaController(EgzApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }
        [HttpPost("")]
        public ActionResult<Upomnienie> Post([FromBody] WystawUpomnienieRequest upomnienie)
        {
            return applicationService.WystawPojedynczeUpomnienie(upomnienie.IdOsoby, upomnienie.IdAdresu, upomnienie.Zaleglosci, upomnienie.DataUpomnienia);
        }

        [HttpDelete("{idOsoby}")]
        public void Delete(int idOsoby)
        {
            throw new NotImplementedException();
        }
    }
}
