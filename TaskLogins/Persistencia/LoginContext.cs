using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dominio.Models;

namespace Persistencia
    {

    public class LoginContext: IdentityDbContext<Usuarios>
    {

        public LoginContext(DbContextOptions options) : base(options) { }

        public LoginContext()
            {
            }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
            public DbSet<Usuarios>? Usuarios { get; set; }

        }
}
