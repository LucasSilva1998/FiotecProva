using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Dtos.Request
{
    public class PacienteRequest
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Telefone { get; set; }

        public UsuarioRequest Usuario { get; set; }

    }
}

