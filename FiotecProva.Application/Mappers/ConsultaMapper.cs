using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Mappers
{
    public static class ConsultaMapper
    {
        public static Consulta MapFrom(ConsultaRequest request)
        {
            return new Consulta
            {
                MedicoId = request.MedicoId,
                PacienteId = request.PacienteId,
                DataHoraConsulta = request.DataHoraConsulta,
                Status = StatusConsulta.Agendada,
                CriadoEm = DateTime.UtcNow
            };
        }

        public static ConsultaResponse MapFrom(Consulta consulta)
        {
            return new ConsultaResponse
            {
                Id = consulta.Id,
                NomeMedico = consulta.Medico?.Nome,
                Especialidade = consulta.Medico?.Especialidade.ToString(),
                NomePaciente = consulta.Paciente?.Nome,
                DataHoraConsulta = consulta.DataHoraConsulta,
                Status = consulta.Status.ToString(),
            };
        }

        public static void AplicarCancelamento(Consulta consulta, ConsultaCancelamentoRequest cancelamentoRequest)
        {
            consulta.Status = StatusConsulta.Cancelada;
            consulta.JustificativaCancelamento = cancelamentoRequest.Justificativa;
        }
    }
}
