using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Reflection;
using WebAPI.Midleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Aplicacion.Seguridad;
using NLog.Web;
using NLog;
using Dominio.Models;
using AutoMapper;
using Aplicacion.MappingProfile;
using Aplicacion.Contratos;
using Seguridad.TokenSeguridad;
using Persistencia.Contex;
using Persistencia.Datos;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
    {

    logger.Debug("Aplication Started...");

    builder.Services.AddMediatR(typeof(login).Assembly);

    builder.Services.AddControllers();
    builder.Services.AddFluentValidationAutoValidation();

    builder.Services.AddIdentityCore<Usuarios>();

    DotNetEnv.Env.Load();

    builder.Services.AddSwaggerGen();

    var connectionstring = Environment.GetEnvironmentVariable("CONNECTION_STRING");

    builder.Services.AddDbContext<LoginContext>(options =>
                           options.UseSqlServer(connectionstring),
                ServiceLifetime.Transient);

    //builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblies());
    var builder_2 = builder.Services.AddIdentityCore<Usuarios>();
    var identitybuilder = new IdentityBuilder(builder_2.UserType, builder.Services);
    identitybuilder.AddEntityFrameworkStores<LoginContext>();
    identitybuilder.AddSignInManager<SignInManager<Usuarios>>();
    builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
    builder.Services.AddScoped<IJWTgenerador, JWTgenerador>();



    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://example.com", "http://www.contoso.com");
                          });
    });



    builder.Services.ConfigureApplicationCookie(identityOptionsCookies =>
    {
        identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });


    var app = builder.Build();

    app.UseMiddleware<ManejadorErroresMidleware>();


    if (app.Environment.IsDevelopment())
        {
        app.UseSwagger();
        app.UseSwaggerUI();



        using (var scope = app.Services.CreateScope())
            {
            var services = scope.ServiceProvider;

            try
                {
                var userManager = services.GetRequiredService<UserManager<Usuarios>>();
                var context = services.GetRequiredService<LoginContext>();
                await context.Database.MigrateAsync();
                await DataPrueba.InsertDada(context, userManager);
                }
            catch (Exception e)
                {
                var loggin = services.GetRequiredService<ILogger<Program>>();
                loggin.LogError(e, "Ocurrio un eror en la migración");
                }
            }
        }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    }
    catch (Exception ex)
    {
    logger.Error(ex, "Excepción durante la ejecución");
    throw;
    }
    finally
    {
    NLog.LogManager.Shutdown();
    }














