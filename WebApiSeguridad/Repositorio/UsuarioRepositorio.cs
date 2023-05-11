using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiSeguridad.Modelos.Dto;
using WebApiSeguridad.Models;
using WebApiSeguridad.Repositorio.Repositorio;

namespace WebApiSeguridad.Repositorio
{
    public class UsuarioRepositorio: IUsuarioRepositorio
    {
        private string secretKey;
        public UsuarioRepositorio(IConfiguration configuration)
        {
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IusuarioUnico(string userName)
        {
            var lstUsuario = ObtenerUsuarios();

            var usuario = lstUsuario.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());

            if (usuario == null)
            {
                return true;
            }

            return false;
        }

        public LoginResponseDTO Login(LoginRequestDTO loginRequestDTO)
        {
            var lstUsuario = ObtenerUsuarios();

            var usuario = lstUsuario.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower() &&
                                                         x.Password == loginRequestDTO.Password);

            if (usuario == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    Usuario = null
                };
            }

            // Si usuario existe generamos el JW Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Rol),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(4).ToString())
                }),                
                Expires= DateTime.UtcNow.AddMinutes(4),
                
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario = usuario
            };

            return loginResponseDTO;
        }

        //public Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO)
        //{
        //    throw new NotImplementedException();
        //}

        private List<Usuario> ObtenerUsuarios()
        {
            var usuario = new Usuario();
            var lstUsuario = new List<Usuario>() {
                new Usuario(){ Id=1, Nombres="Enrique", Password="123456", Rol="Director", UserName="ecmb"}
            };

            return lstUsuario;
        }
    }
}
