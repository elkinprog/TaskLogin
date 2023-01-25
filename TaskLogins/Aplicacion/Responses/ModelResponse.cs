using Aplicacion.Seguridad;
using System.Net;

namespace Aplicacion.Responses
{
    public class ModelResponse<T> : GenericResponse
    {
        public T model { get; set; }

        public ModelResponse(HttpStatusCode codigo, string titulo, string mensaje, T model) : base(codigo, titulo, mensaje)
        {
            this.model = model;
        }

        public ModelResponse(HttpStatusCode codigo, string titulo, string mensaje, login.GetLoginRequest parametro) : base(codigo, titulo, mensaje)
            {
            }
        }

}
