using WebApiSeguridad.Models;

namespace WebApiSeguridad.Modelos.Dto
{
    public class LoginResponseDTO
    {
        public Usuario Usuario { get; set; }
        public string Token { get; set; }
    }
}
