using Microsoft.OpenApi.Models;
using Mohtawa.Services.API.Extensions;
using Mohtawa.Services.API.Middlewares;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//serilog configuration
builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

//entity frame work configuration
builder.ConfigureEntityFrameWork();

builder.ConfigureServices();

//configure automapper
builder.Services.ConfigureAutoMapper();
//swagger configuration
builder.Services.ConfigureSwaggerFeature();

//configure authentication
builder.ConfigureJwtAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
