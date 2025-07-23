using FiotecProva.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Validations
{
    public class PacienteValidator : AbstractValidator<Paciente>
    {
        public PacienteValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome do paciente é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

            RuleFor(p => p.Cpf)
                .NotNull().WithMessage("O CPF é obrigatório.");

            RuleFor(p => p.DataNascimento)
                .LessThan(DateTime.Today).WithMessage("Data de nascimento deve ser anterior à data atual.");

            RuleFor(p => p.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .MinimumLength(10).WithMessage("O telefone deve ter ao menos 10 caracteres.");

            RuleFor(p => p.Endereco)
                .NotNull().WithMessage("O endereço é obrigatório.");
        }
    }
}
