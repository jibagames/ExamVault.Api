using ExamVault.Api.Infraestructura.Datos;
using ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Administracion.Aplicacion.Servicios;
using ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Autenticacion.Aplicacion.Servicios;
using ExamVault.Api.Modulos.Autenticacion.Infraestructura.Seguridad;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Interfaces;
using ExamVault.Api.Modulos.Repositorio.Aplicacion.Servicios;
using ExamVault.Api.Modulos.Repositorio.Infraestructura.Almacenamiento;
using ExamVault.Api.Modulos.Repositorio.Infraestructura.Persistencia.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

using AutenticacionInstitucionRepositorio = ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Repositorios.InstitucionRepositorio;
using AdministracionInstitucionRepositorio = ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Repositorios.InstitucionRepositorio;

using IAutenticacionInstitucionRepositorio = ExamVault.Api.Modulos.Autenticacion.Aplicacion.Interfaces.IInstitucionRepositorio;
using IAdmininstracionInstitucionRepositorio = ExamVault.Api.Modulos.Administracion.Aplicacion.Interfaces.IInstitucionRepositorio;
using ExamVault.Api.Modulos.Administracion.Infraestructura.Persistencia.Repositorios;
using ExamVault.Api.Modulos.Autenticacion.Infraestructura.Persistencia.Repositorios;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IAutenticacionInstitucionRepositorio, AutenticacionInstitucionRepositorio>(); 
builder.Services.AddScoped<IEncriptadorServicio, EncriptadorServicio>();
builder.Services.AddScoped<ITokenServicio, TokenServicio>();
builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();

builder.Services.AddScoped<IMaterialRepositorio, MaterialRepositorio>();
builder.Services.AddScoped<IServicioAlmacenamiento, CloudflareR2Servicio>();
builder.Services.AddScoped<IMaterialServicio, MaterialServicio>();

builder.Services.AddScoped<IAdmininstracionInstitucionRepositorio, AdministracionInstitucionRepositorio>(); 
builder.Services.AddScoped<IAdministracionServicio, AdministracionServicio>();
builder.Services.AddScoped<IAcademicoRepositorio, AcademicoRepositorio>();
builder.Services.AddScoped<IAcademicoServicio, AcademicoServicio>();

builder.Services.AddScoped<IComercialRepositorio, ComercialRepositorio>();
builder.Services.AddScoped<IComercialServicio, ComercialServicio>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor(); 

var logtailToken = builder.Configuration["Logtail:SourceToken"];

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.BetterStack(sourceToken: logtailToken)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddInterceptors(new AuditoriaInterceptor(serviceProvider.GetRequiredService<IHttpContextAccessor>()));
});

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("La clave JWT no está configurada");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.WebHost.UseSentry(o => {
    o.Dsn = builder.Configuration["Sentry:Dsn"];
    o.Debug = true;
});

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();