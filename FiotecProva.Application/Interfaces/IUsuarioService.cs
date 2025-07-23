using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> ObterPorIdAsync(int id);
        Task<IEnumerable<UsuarioResponse>> ListarAsync();
        Task<UsuarioResponse> CriarAsync(UsuarioRequest request);
        Task AtualizarAsync(int id, UsuarioRequest request);
        Task ExcluirAsync(int id);
    }
}
