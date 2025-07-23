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
    public static class MedicoMapper
    {
        public static Medico MapFrom(UsuarioRequest request, EspecialidadeMedica especialidade)
        {
            return new Medico
            {
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                NumeroCRM = request.NumeroCRM,
                AnosExperiencia = request.AnosExperiencia,
                Especialidade = especialidade
            };
        }

        public static Medico MapFrom(MedicoRequest request, EspecialidadeMedica especialidade)
        {
            return new Medico
            {
                Nome = request.Nome,
                DataNascimento = request.DataNascimento,
                NumeroCRM = request.NumeroCRM,
                AnosExperiencia = request.AnosExperiencia,
                Especialidade = especialidade,
                HorariosAtendimento = request.HorariosAtendimento.Select(h => new HorarioAtendimento
                {
                    HoraInicio = TimeSpan.Parse(h),
                    HoraFim = TimeSpan.Parse(h).Add(TimeSpan.FromHours(1))
                }).ToList()
            };
        }

        public static MedicoResponse MapFrom(Medico medico)
        {
            return new MedicoResponse
            {
                Id = medico.Id,
                Nome = medico.Nome,
                DataNascimento = medico.DataNascimento,
                NumeroCRM = medico.NumeroCRM,
                AnosExperiencia = medico.AnosExperiencia,
                Especialidade = medico.Especialidade.ToString(),
                HorariosAtendimento = medico.HorariosAtendimento?
                    .OrderBy(h => h.HoraInicio)
                    .Select(h => h.HoraInicio.ToString(@"hh\:mm"))
                    .ToList()
            };
        }

    }
}
