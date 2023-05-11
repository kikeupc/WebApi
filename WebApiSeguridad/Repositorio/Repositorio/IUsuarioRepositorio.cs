using WebApiSeguridad.Modelos.Dto;
using WebApiSeguridad.Models;

namespace WebApiSeguridad.Repositorio.Repositorio
{
    public interface IUsuarioRepositorio
    {
        bool IusuarioUnico(string userName);
        LoginResponseDTO Login(LoginRequestDTO loginRequestDTO);

        //Task<Usuario> Registrar(RegistroRequestDTO registroRequestDTO);
    }
}
