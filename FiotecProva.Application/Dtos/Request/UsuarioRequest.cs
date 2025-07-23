using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Dtos.Request
{
    public class UsuarioRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }


        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Perfil { get; set; }


        // Campos para o cadastro de paciente (será opcional)
        public string Cpf { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }


        // Campos para o cadastro de médicos (será opcional)
        public string NumeroCRM { get; set; }
        public int AnosExperiencia { get; set; }
        public string Especialidade { get; set; }
    }
}

