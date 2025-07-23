using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> ObterPorEmailAsync(string email);
        Task<bool> ExisteEmailAsync(string email);
        Task<IEnumerable<Usuario>> ObterTodosComPerfisAsync();
    }
}

