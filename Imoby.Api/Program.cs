using AutoMapper;
using Imoby.Api.Token;
using Imoby.Domain.Interface.Repository;
using Imoby.Domain.Interface.Service;
using Imoby.Domain.Service;
using Imoby.Entities.Entitie.Models;
using Imoby.Infra.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//CONFIG SINGLETON no start do Sistema
builder.Services.AddSingleton(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
//Service 
builder.Services.AddSingleton<IServiceUsuario, ServiceUsuario>();
//Repository
builder.Services.AddSingleton<IRepositoryUsuario, RepositoryUsuario>();



// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Teste.Securiry.Bearer",
            ValidAudience = "Teste.Securiry.Bearer",
            IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
        };

        option.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                return Task.CompletedTask;
            }
        };
    });



//AUTOMAPPER CONFIG
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<UsuarioViewModel, Usuario>().ReverseMap();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);




var app = builder.Build();

// Configure Swagger para rodar em Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//CORS CONFIG
var devClient = "http://localhost:3000";
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader().WithOrigins(devClient));



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
