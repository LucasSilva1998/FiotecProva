using FiotecProva.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Validations
{
    public class MedicoValidator : AbstractValidator<Medico>
    {
        public MedicoValidator()
        {
            RuleFor(m => m.Nome)
                .NotEmpty().WithMessage("O nome do médico é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

            RuleFor(m => m.NumeroCRM)
                .NotEmpty().WithMessage("O número do CRM é obrigatório.");

            RuleFor(m => m.AnosExperiencia)
                .GreaterThanOrEqualTo(0).WithMessage("Anos de experiência deve ser um número positivo.");

            RuleFor(m => m.DataNascimento)
                .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser anterior à data atual.");
        }
    }
}
