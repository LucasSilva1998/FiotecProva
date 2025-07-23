using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Interfaces.Repository
{
    public interface IMedicoRepository : IBaseRepository<Medico>
    {
        Task<Medico> ObterPorCRMAsync(string numeroCRM);
        Task<bool> MedicoDisponivelAsync(int medicoId, DateTime dataHora);
        Task<IEnumerable<Medico>> ObterTodosComConsultasAsync();
    }
}
