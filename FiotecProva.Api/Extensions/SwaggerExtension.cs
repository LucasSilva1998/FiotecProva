using Microsoft.OpenApi.Models;

namespace FiotecProva.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FiotecProva API - Gerenciamento de Consultas Médicas",
                    Version = "v1",
                    Description = "API para gerenciamento de pacientes, médicos e agendamento de consultas.",
                    Contact = new OpenApiContact
                    {
                        Name = "Lucas Pereira",
                        Email = "lucassilva@fiotec.fiocruz.com",
                        Url = new Uri("https://github.com/LucasSilva1998/FiotecProva")
                    }
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Informe o token JWT no formato: Bearer {seu_token}",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            return services;
        }
    }
}

