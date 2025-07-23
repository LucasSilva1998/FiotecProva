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
        Task<IEnumerable<Consulta>> ListarPorMedicoAsync(int medicoId, int pagina, int tamanhoPagina);
        Task<IEnumerable<Consulta>> ListarPorPacienteAsync(int pacienteId, int pagina, int tamanhoPagina);
        Task<bool> ExisteConsultaNoHorarioAsync(int medicoId, DateTime dataHora); 
        void Atualizar(Consulta consulta);

        Task<int> ContarAsync();
        Task<IEnumerable<Consulta>> ObterPaginadoAsync(int page, int pageSize);

    }
}

