using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Infra.Data.Security
{
    public static class PasswordHasher
    {
        public static string Hash(string senha)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hash}";
        }

        public static bool Verificar(string senha, string senhaHashArmazenada)
        {
            var partes = senhaHashArmazenada.Split('.');
            if (partes.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(partes[0]);

            string hashTentativa = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashTentativa == partes[1];
        }
    }
}