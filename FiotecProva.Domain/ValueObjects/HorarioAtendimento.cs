using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.ValueObjects
{
    public class HorarioAtendimento
    {
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fim { get; set; }

        public HorarioAtendimento(TimeSpan inicio, TimeSpan fim)
        {
            if (fim <= inicio)
                throw new ArgumentException("Horário de término deve ser maior que horário de início.");

            Inicio = inicio;
            Fim = fim;
        }
    }
}