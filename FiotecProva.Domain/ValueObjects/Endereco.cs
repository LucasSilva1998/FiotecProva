using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.ValueObjects
{
    public class Endereco
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Cep { get; private set; }
        public string Bairro { get; private set; }
        public string Municipio { get; private set; }
        public string Uf { get; private set; }

        protected Endereco() { }

        public Endereco(string logradouro, string numero, string cep, string bairro, string municipio, string uf)
        {
            if (string.IsNullOrWhiteSpace(cep)) throw new ArgumentException("CEP é obrigatório.");
            if (string.IsNullOrWhiteSpace(logradouro)) throw new ArgumentException("Logradouro é obrigatório.");
            if (string.IsNullOrWhiteSpace(numero)) throw new ArgumentException("Número é obrigatório.");
            if (string.IsNullOrWhiteSpace(municipio)) throw new ArgumentException("Município é obrigatório.");
            if (string.IsNullOrWhiteSpace(uf)) throw new ArgumentException("UF é obrigatória.");

            Logradouro = logradouro;
            Numero = numero;
            Cep = cep;
            Bairro = bairro;
            Municipio = municipio;
            Uf = uf;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Endereco other) return false;

            return Logradouro == other.Logradouro &&
                   Numero == other.Numero &&
                   Cep == other.Cep &&
                   Bairro == other.Bairro &&
                   Municipio == other.Municipio &&
                   Uf == other.Uf;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Logradouro, Numero, Cep, Bairro, Municipio, Uf);
        }

        public override string ToString()
        {
            return $"{Logradouro}, {Numero} - {Bairro}, {Municipio} - {Uf}, {Cep}";
        }
    }
}
