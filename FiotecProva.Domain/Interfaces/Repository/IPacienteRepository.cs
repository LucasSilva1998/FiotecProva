using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Interfaces.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Interfaces.Repository
{
    public interface IPacienteRepository : IBaseRepository<Paciente>
    {
        Task<Paciente> ObterPorCpfAsync(string cpf);
        Task<bool> ExisteCpfAsync(string cpf);
    }
}

