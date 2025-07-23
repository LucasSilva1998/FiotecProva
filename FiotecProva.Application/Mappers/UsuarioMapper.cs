using FiotecProva.Application.Dtos.Request;
using FiotecProva.Domain.Entities;
using FiotecProva.Infra.Data.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Mappers
{
    public static class UsuarioMapper
    {
        public static Usuario MapFrom(UsuarioRequest request, Perfil perfil)
        {
            return new Usuario
            {
                Email = request.Email,
                SenhaHash = PasswordHasher.Hash(request.Senha),
                PerfilId = perfil.Id,
                Perfil = perfil
            };
        }
    }
}
