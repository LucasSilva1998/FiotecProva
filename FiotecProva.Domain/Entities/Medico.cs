using FiotecProva.Domain.Enums;
using FiotecProva.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Entities
{
    public class Medico
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public int AnosExperiencia { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCRM { get; set; }

        public List<HorarioAtendimento> HorariosAtendimento { get; set; } = new();

        public EspecialidadeMedica Especialidade { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}