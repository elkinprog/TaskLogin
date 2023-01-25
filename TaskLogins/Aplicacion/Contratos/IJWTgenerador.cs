using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Contratos
    {
        public interface IJWTgenerador
        {
        string CrearToken(Usuarios usuario);

        }
    }
