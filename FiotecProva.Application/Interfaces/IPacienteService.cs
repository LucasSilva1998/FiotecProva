using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<int> CadastrarAsync(PacienteRequest request);
        Task AtualizarAsync(int id, PacienteRequest request);
        Task<IEnumerable<PacienteResponse>> ListarAsync();
        Task<PacienteResponse> ObterPorIdAsync(int id);
        Task RemoverAsync(int id);
    }
}