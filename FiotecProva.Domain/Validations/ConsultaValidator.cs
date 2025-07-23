using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Validations
{
    public class ConsultaValidator : AbstractValidator<Consulta>
    {
        public ConsultaValidator()
        {
            RuleFor(c => c.MedicoId)
                .NotEmpty().WithMessage("O médico é obrigatório.");

            RuleFor(c => c.PacienteId)
                .NotEmpty().WithMessage("O paciente é obrigatório.");

            RuleFor(c => c.DataHoraConsulta)
                .GreaterThan(DateTime.Now).WithMessage("A consulta deve estar agendada para o futuro.");

            RuleFor(c => c.Status)
                .IsInEnum().WithMessage("Status da consulta inválido.");

            When(c => c.Status == StatusConsulta.Cancelada, () =>
            {
                RuleFor(c => c.JustificativaCancelamento)
                    .NotEmpty().WithMessage("A justificativa é obrigatória para consultas canceladas.");
            });
        }
    }
}
