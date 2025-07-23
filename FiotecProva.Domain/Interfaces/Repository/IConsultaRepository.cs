using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Interfaces.Repository
{
    public interface IConsultaRepository : IBaseRepository<Consulta>
    {
        Task<IEnumerable<Consulta>> ListarPorMedicoAsync(int medicoId);
        Task<IEnumerable<Consulta>> ListarPorPacienteAsync(int pacienteId);
        Task<bool> ExisteConsultaNoHorarioAsync(int medicoId, DateTime dataHora); 
        void Atualizar(Consulta consulta);

    }
}

