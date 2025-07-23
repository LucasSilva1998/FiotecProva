using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }

        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }

        public Medico Medico { get; set; }

        public Paciente Paciente { get; set; }

    }
}