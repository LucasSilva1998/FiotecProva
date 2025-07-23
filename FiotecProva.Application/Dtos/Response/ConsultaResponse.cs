using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Dtos.Response
{
    public class ConsultaResponse
    {
        public int Id { get; set; }
        public string NomeMedico { get; set; }
        public string Especialidade { get; set; }
        public string NomePaciente { get; set; }
        public DateTime DataHoraConsulta { get; set; }
        public string Status { get; set; }
    }
}
