using FiotecProva.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Entities
{
    public class Consulta
    {
        public int Id { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime DataHoraConsulta { get; set; }
        public StatusConsulta Status { get; set; }
        public string JustificativaCancelamento { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    }
}
