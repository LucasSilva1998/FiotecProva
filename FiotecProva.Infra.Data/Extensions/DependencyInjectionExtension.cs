using FiotecProva.Domain.Interfaces.Core;
using FiotecProva.Domain.Interfaces.Repository;
using FiotecProva.Domain.Interfaces.Services;
using FiotecProva.Domain.Services;
using FiotecProva.Infra.Data.Repositories;
using FiotecProva.Infra.Data.Repositories.UoW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Infra.Data.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositórios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IHorarioAtendimentoRepository, HorarioAtendimentoRepository>();

            // Serviços de domínio
            services.AddScoped<IUsuarioDomainService, UsuarioDomainService>();
            services.AddScoped<IConsultaDomainService, ConsultaDomainService>();

            return services;
        }
    }
}

