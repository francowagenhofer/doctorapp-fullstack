using API.Extensiones;
using Data;
using Data.Interfaces;
using Data.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// --  Extensiones de servicios ---
// Sirve para agregar los servicios de la aplicación definidos en la extensión
builder.Services.AgregarServiciosAplicacion(builder.Configuration); 

// Sirve para agregar los servicios de identidad definidos en la extensión
builder.Services.AgregarServiciosIdentidad(builder.Configuration);
// -- Fin extensiones de servicios ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sirve para que el API pueda ser consumido desde el front-end en Angular
app.UseCors(x =>
{
    x.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
