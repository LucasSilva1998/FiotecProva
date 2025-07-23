using FiotecProva.Infra.Data.ExternalServices.ViaCep.Dto;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Interface;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Infra.Data.Tests.ViaCep
{
    public class ViaCepIntegrationTests
    {
        private readonly IViaCepService _viaCepService;

        public ViaCepIntegrationTests()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://viacep.com.br/ws/")
            };

            _viaCepService = new ViaCepService(httpClient);
        }

        [Fact(DisplayName = "Deve retornar endereço válido ao consultar um CEP existente")]
        public async Task Consultar_Cep_Valido_DeveRetornarEndereco()
        {
            var cep = "21040361"; // Cep da Fiotec

            ViaCepResponse result = await _viaCepService.BuscarEnderecoPorCepAsync(cep);

            result.Should().NotBeNull();
            result.Cep.Should().Be("21040-361");
            result.Localidade.Should().Be("Rio de Janeiro");
            result.Uf.Should().Be("RJ");
        }

        [Fact(DisplayName = "Deve retornar nulo ao consultar um CEP inexistente")]
        public async Task Consultar_Cep_Invalido_DeveRetornarNulo()
        {
            var cepInvalido = "00000000";

            var result = await _viaCepService.BuscarEnderecoPorCepAsync(cepInvalido);

            result.Should().BeNull();
        }
    }
}