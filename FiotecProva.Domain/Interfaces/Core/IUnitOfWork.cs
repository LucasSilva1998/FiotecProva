using FiotecProva.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Interfaces.Core
{
    public interface IUnitOfWork : IDisposable
    {        
        Task<bool> CommitAsync();

        Task RollbackAsync();

        #region

        IConsultaRepository ConsultaRepository { get; }
        IHorarioAtendimentoRepository HorarioAtendimentoRepository { get; }
        IMedicoRepository MedicoRepository { get; }
        IPacienteRepository PacienteRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        IPerfilRepository PerfilRepository { get; }

        #endregion

    }
}