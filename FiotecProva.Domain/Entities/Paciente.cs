using FiotecProva.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Entities
{
    public class Paciente
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public Cpf Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        public string Telefone { get; set; }

        public Endereco Endereco { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }

}