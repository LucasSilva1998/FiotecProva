using FiotecProva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Interfaces.Services
{
    public interface IConsultaDomainService
    {
        Task AgendarConsultaAsync(Consulta consulta);
    }
}

