using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiSeguridad.Modelos.Dto;
using WebApiSeguridad.Models;
using WebApiSeguridad.Repositorio.Repositorio;

namespace WebApiSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private APIResponse _response;
        public UsuarioController(IUsuarioRepositorio usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
            _response = new();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequestDTO modelo)
        {
            var loginResponse = _usuarioRepo.Login(modelo);

            if (loginResponse.Usuario == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsExitoso = false;
                _response.ErrorMessages.Add("UserName o Password son Incorrectos");
                return BadRequest(_response);
            }
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Resultado = loginResponse;

            return Ok(_response);
        }
    }
}
