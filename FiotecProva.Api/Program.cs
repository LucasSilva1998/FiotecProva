using FiotecProva.Api.Extensions;
using FiotecProva.Infra.Data.Extensions;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Interface;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Infra Services
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddInfrastructureServices();

// HTTP externo (ViaCep)
builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

// Swagger (centralizado na extensão)
builder.Services.AddSwaggerDocumentation();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
