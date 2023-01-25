using Aplicacion.Responses;
using Aplicacion.Seguridad;
using AutoMapper;
using Dominio.Models;
using Dominio.ModelsDTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
using System.Reflection.Metadata;


namespace WebAPI.Controllers
    {

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
        {

        private readonly IMediator _mediator;
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;

        public LoginController(IMediator mediator, ILogger<LoginController> logger, IMapper mapper)
            {
            this._mediator = mediator;
            this._logger = logger;
            this._mapper = mapper;
            }

        [HttpPost]
        public async Task<ActionResult<UsuariosDTO>> Login(login.GetLoginRequest parametro)
        {
            //var usuarioDTO = _mapper.Map<Usuarios>(parametro);
            var guardo = await _mediator.Send(parametro);
            return Ok(guardo);
        }
    }
}
