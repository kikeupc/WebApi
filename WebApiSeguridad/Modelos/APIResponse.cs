using System.Net;

namespace WebApiSeguridad.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsExitoso { get; set; }
        public List<string> ErrorMessages { get; set; }
        public Object Resultado { get; set; }
    }
}
