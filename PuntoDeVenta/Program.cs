using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PuntoDeVenta.Common;
using PuntoDeVenta.Models;
using PuntoDeVenta.services.Autenticacion;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string configuracionCors = "cors";

// cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(configuracionCors,
        builder => builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

////////////

builder.Services.AddDbContext<PuntoDeVentaContext>();

builder.Services.AddSingleton<IJwtGeneratorToken, JwtGeneradorToken>();
builder.Services.AddScoped<IAutenticacionServicio, AutenticacionServicio>();


// aņadiendole autenticacion a nuestros endpoints por medio de JWT

var jwtSeccion = builder.Configuration.GetSection("JwtAjustes");

builder.Services.Configure<JwtAjustes>(jwtSeccion);

var token = jwtSeccion.Get<JwtAjustes>()!.Secreto;

var key = Encoding.ASCII.GetBytes(token);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(d =>
{
    d.RequireHttpsMetadata = false;
    d.SaveToken = true;
    d.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

////////////////////////////////////////////////////////////////////////

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(configuracionCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
