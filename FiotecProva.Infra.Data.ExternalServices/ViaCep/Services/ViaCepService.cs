using FiotecProva.Infra.Data.ExternalServices.ViaCep.Dto;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FiotecProva.Infra.Data.ExternalServices.ViaCep.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        public async Task<ViaCepResponse> BuscarEnderecoPorCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"{cep}/json/");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ViaCepResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}

