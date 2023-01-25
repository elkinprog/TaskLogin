using Dominio.Models;
using Microsoft.AspNetCore.Identity;
using Persistencia.Contex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Datos
    {
        public  class DataPrueba
        {
              public static async Task InsertDada(LoginContext context, UserManager<Usuarios> userManager)
              {
                if (!userManager.Users.Any())
                {
                var usuario = new Usuarios { Nombre = "Elkin", UserName = "Elkinprog", Email = "Elkinprog@gmail.com" };
                await userManager.CreateAsync(usuario, "Qa@12345_79913");

                }

              }


        }

    }
