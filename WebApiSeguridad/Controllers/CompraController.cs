using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebApiSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController: ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Director")]
        public ActionResult<string> Registrar()
        {
            return "Se ha registrado la compra";
        }
    }
}
