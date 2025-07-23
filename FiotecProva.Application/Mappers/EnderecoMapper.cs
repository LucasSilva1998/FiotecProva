using FiotecProva.Domain.ValueObjects;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Mappers
{
    public static class EnderecoMapper
    {
        public static Endereco MapFrom(ViaCepResponse viaCep, string numero)
        {
            return new Endereco(
                logradouro: viaCep.Logradouro,
                numero: numero,
                cep: viaCep.Cep,
                bairro: viaCep.Bairro,
                municipio: viaCep.Localidade,
                uf: viaCep.Uf
            );
        }
    }
}