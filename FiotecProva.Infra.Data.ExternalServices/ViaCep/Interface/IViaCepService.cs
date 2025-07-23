using FiotecProva.Infra.Data.ExternalServices.ViaCep.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Infra.Data.ExternalServices.ViaCep.Interface
{
    public interface IViaCepService
    {
        Task<ViaCepResponse> BuscarEnderecoPorCepAsync(string cep);
    }
}

