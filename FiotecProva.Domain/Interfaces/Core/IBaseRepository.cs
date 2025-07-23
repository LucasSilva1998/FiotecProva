using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Interfaces.Core
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> ObterPorIdAsync(int id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task AdicionarAsync(T entity);
        void Atualizar(T entity);
        void Remover(T entity);
    }
}
