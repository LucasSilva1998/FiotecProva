using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Domain.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Interfaces
{
    public interface IConsultaService
    {
        Task<int> AgendarAsync(ConsultaRequest request);
        Task CancelarAsync(int id, ConsultaCancelamentoRequest request);
        Task<PagedList<ConsultaResponse>> ListarAsync(PaginationFilter filtro);
        Task<ConsultaResponse> ObterPorIdAsync(int id);
    }
}
