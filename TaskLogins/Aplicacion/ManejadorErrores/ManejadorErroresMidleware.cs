using Aplicacion.ManejadorErrores;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace WebAPI.Midleware
{
    public class ManejadorErroresMidleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErroresMidleware> _logger;
        public ManejadorErroresMidleware(RequestDelegate next, ILogger<ManejadorErroresMidleware> logger)
        {
            this._next   = next;
            this._logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
           try 
           {
                await _next(context);
           } 
           catch(Exception ex)
           {
                await ManejadorExcepcionAsincrono(context, ex, _logger);
           }

            async Task ManejadorExcepcionAsincrono(HttpContext context, Exception ex, ILogger<ManejadorErroresMidleware> logger)
            {
                object errores = null;
                switch (ex)
                {
                    case ExcepcionError me:
                        logger.LogError(ex, "manejador Error");
                        errores = me.Errores;
                        context.Response.StatusCode = (int)me.Codigo;
                        break;
                    case Exception e:
                        logger.LogError(ex, "Error de servidor");
                        errores= string.IsNullOrWhiteSpace(e.Message)? "Error": e.Message;
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                context.Response.ContentType = "aplication/json";
                if (errores!= null)
                {
                    var resultado= JsonConvert.SerializeObject(new { errores });
                    await context.Response.WriteAsync(resultado);
                }
            }


        }
    }
}
