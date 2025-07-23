using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Interfaces.Core;
using FiotecProva.Domain.Interfaces.Repository;
using FiotecProva.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IPacienteRepository pacienteRepository, IMedicoRepository medicoRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _pacienteRepository = pacienteRepository;
            _medicoRepository = medicoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CadastrarPacienteAsync(Usuario usuario)
        {
            if (await _usuarioRepository.ExisteEmailAsync(usuario.Email))
                throw new Exception("E-mail já cadastrado.");

            if (await _pacienteRepository.ExisteCpfAsync(usuario.Paciente.Cpf.Numero))
                throw new Exception("CPF já cadastrado para um paciente.");

            await _usuarioRepository.AdicionarAsync(usuario);
            await _unitOfWork.CommitAsync();
        }

        public async Task CadastrarMedicoAsync(Usuario usuario)
        {
            if (await _usuarioRepository.ExisteEmailAsync(usuario.Email))
                throw new Exception("E-mail já cadastrado.");

            var crmExiste = await _medicoRepository.ObterPorCRMAsync(usuario.Medico.NumeroCRM);
            if (crmExiste != null)
                throw new Exception("CRM já cadastrado para um médico.");

            await _usuarioRepository.AdicionarAsync(usuario);
            await _unitOfWork.CommitAsync();
        }

        public async Task CadastrarAdminAsync(Usuario usuario)
        {
            if (await _usuarioRepository.ExisteEmailAsync(usuario.Email))
                throw new Exception("E-mail já cadastrado.");

            await _usuarioRepository.AdicionarAsync(usuario);
            await _unitOfWork.CommitAsync();
        }
    }
}