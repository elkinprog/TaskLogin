using AutoMapper;
using Dominio.Models;
using Dominio.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.MappingProfile
    {
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuarios,UsuariosDTO>().ReverseMap();
        }

    }
}
