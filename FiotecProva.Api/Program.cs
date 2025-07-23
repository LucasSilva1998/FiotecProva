using FiotecProva.Api.Extensions;
using FiotecProva.Application.Interfaces;
using FiotecProva.Application.Services;
using FiotecProva.Domain.Interfaces.Services;
using FiotecProva.Domain.Services;
using FiotecProva.Infra.Data.Extensions;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Interface;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog - Gravar os logs 
Directory.CreateDirectory(@"C:\FiotecProva\Logs");

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

// Application Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IConsultaService, ConsultaService>();

// Domain Services
builder.Services.AddScoped<IUsuarioDomainService, UsuarioDomainService>();
builder.Services.AddScoped<IConsultaDomainService, ConsultaDomainService>();

// Infra Services
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddInfrastructureServices();

// Infra HTTP externo (ViaCep)
builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

// Swagger (centralizado na extensão)
builder.Services.AddSwaggerDocumentation();

builder.Services.AddOpenApi();

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
