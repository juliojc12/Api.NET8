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


builder.Host.UseSerilog( ( context, services, configuration ) =>
    configuration
        .ReadFrom.Configuration( context.Configuration )
        .ReadFrom.Services( services )
        .WriteTo.Console()
        .WriteTo.File( "logs/log-.txt", rollingInterval: RollingInterval.Day )
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithProcessId()
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc( "v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API para cadastro de clientes",
        Version = "v1",
        Description = "Um simples API para gerenciar clientes e seus endereços",
    } );
} );

builder.Services.AddDbContext<AppDbContext>( opt =>
    opt.UseInMemoryDatabase( "ClienteDb" ) );

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddAutoMapper( typeof( ClienteProfile ).Assembly );


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( c =>
    {
        c.SwaggerEndpoint( "/swagger/v1/swagger.json", "API para cadastro de clientes v1" );
        c.RoutePrefix = string.Empty;
    } );

}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
