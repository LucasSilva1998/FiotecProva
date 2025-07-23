using FiotecProva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        Task CadastrarPacienteAsync(Usuario usuario);
        Task CadastrarMedicoAsync(Usuario usuario);
        Task CadastrarAdminAsync(Usuario usuario);
    }
}