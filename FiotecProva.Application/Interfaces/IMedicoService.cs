using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Interfaces
{
    public interface IMedicoService
    {
        Task<int> CriarAsync(MedicoRequest request);
        Task<MedicoResponse> ObterPorIdAsync(int id);
        Task<IEnumerable<MedicoResponse>> ListarAsync();
        Task AtualizarAsync(int id, MedicoRequest request);
        Task RemoverAsync(int id);
    }
}

