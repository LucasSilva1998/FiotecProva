using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Utils
{
    public static class StringUtils
    {
        public static bool EhPaciente(this string perfil) =>
            perfil.Equals("Paciente", StringComparison.OrdinalIgnoreCase);

        public static bool EhMedico(this string perfil) =>
            perfil.Equals("Medico", StringComparison.OrdinalIgnoreCase);

        public static bool EhAdmin(this string perfil) =>
            perfil.Equals("Admin", StringComparison.OrdinalIgnoreCase);
    }
}