using Aplicacion.DTO;
using Dominio.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Aplicacion.Seguridad
    {
    public class login
    {
        public class GetLoginRequest : IRequest<Usuarios>
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
       
        public class GetLoginHandler : IRequestHandler<GetLoginRequest, Usuarios>
        {
            
            private readonly UserManager<Usuarios> _clientManager;
            private readonly SignInManager<Usuarios> _signInManager;

            public GetLoginHandler(UserManager<Usuarios> clientManager, SignInManager<Usuarios> signInManager)
            {
                this._clientManager = clientManager;
                this._signInManager = signInManager;
            }
            public async Task<Usuarios> Handle(GetLoginRequest request, CancellationToken cancellationToken)
            {
                var email = await _clientManager.FindByEmailAsync(request.Email);
                if (email == null)
                {
                    throw new Exception(HttpStatusCode.Unauthorized + "Email es null");
                }
               var result =  await _signInManager.CheckPasswordSignInAsync(email , request.Password,false);
                if (result.Succeeded)
                {
                    return email;
                        
                }
                throw new Exception(HttpStatusCode.Unauthorized + "" + " Email es null");
            }
        }

    }
}
