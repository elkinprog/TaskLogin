using Aplicacion.Contratos;
using Dominio.Models;
using Dominio.ModelsDTO;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Reflection.Metadata;

namespace Aplicacion.Seguridad
    {
    public class login
    {
        public class GetLoginRequest : IRequest<UsuariosDTO>
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        public class GetLoginValidation: AbstractValidator<GetLoginRequest>
        {
            public GetLoginValidation()
            {
                RuleFor(x => x.Email).NotEmpty().WithMessage("Este campo no debe estar vacio");
                RuleFor(x => x.Password).NotEmpty().WithMessage("Este campo no debe estar vacio");        
            }

        }
       
        public class GetLoginHandler : IRequestHandler<GetLoginRequest, UsuariosDTO>
        {
            private readonly UserManager<Usuarios>   _clientManager;
            private readonly SignInManager<Usuarios> _signInManager;
            private readonly IJWTgenerador           _jwtGenerador;

            public GetLoginHandler(UserManager<Usuarios> clientManager, SignInManager<Usuarios> signInManager, IJWTgenerador jwtGenerador)
            {
            this._clientManager = clientManager;
            this._signInManager = signInManager;
            this._jwtGenerador  = jwtGenerador;   
            }

            public async Task<UsuariosDTO> Handle(GetLoginRequest request, CancellationToken cancellationToken)
            {
       
                var email = await _clientManager.FindByEmailAsync(request.Email);

                if (email == null)
                {
                    throw new Exception(HttpStatusCode.Unauthorized + "Email es null");
                }

                var password =  await _signInManager.CheckPasswordSignInAsync(email , request.Password,false);

                if (password.Succeeded){
                    return new UsuariosDTO
                    {
                        Email = email.Email,
                        Password    = email.PasswordHash,
                        UserName    = email.UserName,
                        Tokent      = _jwtGenerador.CrearToken(email),
                        };
                }
                throw new Exception(HttpStatusCode.Unauthorized + "" + " Email es null");
            }
        }

    }
}
