using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Domain.Entities;
using FiotecProva.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Mappers
{
    public static class PacienteMapper
    {
        public static Paciente MapFrom(UsuarioRequest request, Endereco endereco)
        {
            return new Paciente
            {
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                Telefone = request.Telefone,
                Cpf = new Cpf(request.Cpf),
                Endereco = endereco
            };
        }

        public static Paciente MapFrom(PacienteRequest request, Endereco endereco)
        {
            return new Paciente
            {
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                Telefone = request.Telefone,
                Cpf = new Cpf(request.Cpf),
                Endereco = endereco
            };
        }

        public static PacienteResponse MapToResponse(Paciente paciente)
        {
            return new PacienteResponse
            {
                Id = paciente.Id,
                Nome = paciente.Nome,
                DataNascimento = paciente.DataNascimento,
                Cpf = paciente.Cpf.Numero,
                Telefone = paciente.Telefone,
                Logradouro = paciente.Endereco?.Logradouro,
                Numero = paciente.Endereco?.Numero,
                Bairro = paciente.Endereco?.Bairro,
                Uf = paciente.Endereco?.Uf,
                Cep = paciente.Endereco?.Cep
            };
        }
    }
}