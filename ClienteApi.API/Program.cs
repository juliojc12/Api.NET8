using ClienteApi.Application.Interfaces;
using ClienteApi.Application.Mapping;
using ClienteApi.Application.Services;
using ClienteApi.Domain.Interfaces;
using ClienteApi.Infrastructure.Data;
using ClienteApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File( "logs/log-.txt", rollingInterval: RollingInterval.Day )
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProcessId()
    .CreateBootstrapLogger();





var builder = WebApplication.CreateBuilder( args );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>( opt =>
    opt.UseInMemoryDatabase( "ClienteDb" ) );

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddAutoMapper( typeof( ClienteProfile ).Assembly );


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
