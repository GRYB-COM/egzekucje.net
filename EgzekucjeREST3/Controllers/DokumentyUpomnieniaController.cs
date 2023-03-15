using Microsoft.AspNetCore.Mvc;

namespace EgzekucjeREST3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentyUpomnieniaController : ControllerBase
    {
        [HttpGet("html/{idUpomnienia}")]
        public ActionResult PobierzUpomnienieWHtml(long idUpomnienia)
        {
            // REFACTOR make it independent from absolute path
            var htmlLocation = @"C:\Users\cez\Desktop\upomnienie_wypelnione.html";
            var htmlBytes = System.IO.File.ReadAllBytes(htmlLocation);
            return File(htmlBytes, "text/html");
        }
        [HttpGet("pdf/{idUpomnienia}")]
        public ActionResult PobierzUpomnienieWPdf(long idUpomnienia)
        {
            // REFACTOR make it independent from absolute path
            var pdfLocation = @"C:\Users\cez\Desktop\upomnienie_wypelnione.pdf";
            var pdfBytes = System.IO.File.ReadAllBytes(pdfLocation);
            return File(pdfBytes, "application/pdf");
        }
        [HttpGet("rtf/{idUpomnienia}")]
        public ActionResult PobierzUpomnienieWRtf(long idUpomnienia)
        {
            var rtfString = new Egzekucje.NET.EgzApplicationService().PobierzDokumentUpomnienia(idUpomnienia);
            return File(System.Text.Encoding.UTF8.GetBytes(rtfString), "application/rtf");
        }
    }
}
