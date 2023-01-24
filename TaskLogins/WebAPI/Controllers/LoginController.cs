using Aplicacion.DTO;
using Aplicacion.Seguridad;
using Dominio.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebAPI.Controllers
    {

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
        {

        private readonly IMediator _mediator;

        private readonly ILogger<LoginController> _logger;

        public LoginController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Usuarios>> Login(login.GetLoginRequest parametro)
        {
            return await _mediator.Send(parametro);

        }


    }
}
