using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Dtos.Response
{
    public class MedicoResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int AnosExperiencia { get; set; }
        public string NumeroCRM { get; set; }
        public string Especialidade { get; set; }
        public List<string> HorariosAtendimento { get; set; }
    }
}
