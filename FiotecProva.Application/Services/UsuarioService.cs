using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Application.Interfaces;
using FiotecProva.Application.Mappers;
using FiotecProva.Application.Utils;
using FiotecProva.Domain.Enums;
using FiotecProva.Domain.Interfaces.Core;
using FiotecProva.Domain.Interfaces.Repository;
using FiotecProva.Domain.Interfaces.Services;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IViaCepService _viaCepService;
        private readonly IPerfilRepository _perfilRepository;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IUsuarioDomainService usuarioDomainService, IViaCepService viaCepService, IPerfilRepository perfilRepository, ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _usuarioDomainService = usuarioDomainService;
            _viaCepService = viaCepService;
            _perfilRepository = perfilRepository;
            _logger = logger;
        }

        public async Task AtualizarAsync(int id, UsuarioRequest request)
        {
            _logger.LogInformation("Iniciando atualização de usuário com ID {UsuarioId} e e-mail {Email}.", id, request.Email);
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
            {
                _logger.LogWarning("Usuário com ID {UsuarioId} não encontrado para atualização.", id);
                throw new Exception("Usuário não encontrado.");
            }

            usuario.Email = request.Email;

            _usuarioRepository.Atualizar(usuario);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Usuário com ID {UsuarioId} atualizado com sucesso.", id);
            _logger.LogInformation("Serviço de atualização de usuário finalizado.");
        }

        public async Task<UsuarioResponse> CriarAsync(UsuarioRequest request)
        {
            _logger.LogInformation("Iniciando criação de usuário com e-mail {Email} e perfil {Perfil}.", request.Email, request.Perfil);

            var perfil = await _perfilRepository.ObterPorNomeAsync(request.Perfil);
            if (perfil == null)
            {
                _logger.LogWarning("Perfil inválido informado: {Perfil}", request.Perfil);
                throw new Exception("Perfil inválido. Deve ser 'Admin', 'Paciente' ou 'Medico'.");
            }

            var usuario = UsuarioMapper.MapFrom(request, perfil);

            if (request.Perfil.EhPaciente())
            {
                var viaCep = await _viaCepService.BuscarEnderecoPorCepAsync(request.Cep);
                if (viaCep == null)
                {
                    _logger.LogWarning("CEP inválido informado: {Cep}", request.Cep);
                    throw new Exception("CEP inválido.");
                }

                var endereco = EnderecoMapper.MapFrom(viaCep, request.Numero);
                var paciente = PacienteMapper.MapFrom(request, endereco);

                usuario.Paciente = paciente;

                await _usuarioDomainService.CadastrarPacienteAsync(usuario);

                _logger.LogInformation("Usuário paciente {Email} cadastrado com sucesso.", request.Email);
            }
            else if (request.Perfil.EhMedico())
            {
                if (!Enum.TryParse<EspecialidadeMedica>(request.Especialidade, true, out var especialidadeEnum))
                {
                    _logger.LogWarning("Especialidade médica inválida: {Especialidade}", request.Especialidade);
                    throw new Exception("Especialidade inválida.");
                }

                var medico = MedicoMapper.MapFrom(request, especialidadeEnum);
                usuario.Medico = medico;

                await _usuarioDomainService.CadastrarMedicoAsync(usuario);

                _logger.LogInformation("Usuário médico {Email} cadastrado com sucesso.", request.Email);
            }
            else if (request.Perfil.EhAdmin())
            {
                await _usuarioDomainService.CadastrarAdminAsync(usuario);

                _logger.LogInformation("Usuário admin {Email} cadastrado com sucesso.", request.Email);
            }
            else
            {
                _logger.LogWarning("Perfil inválido informado: {Perfil}", request.Perfil);
                throw new Exception("Perfil inválido. Deve ser 'Admin', 'Paciente' ou 'Medico'.");
            }

            _logger.LogInformation("Serviço de criação para cadastro de usuário finalizado.");

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Perfil = usuario.Perfil.Nome
            };
        }

        public async Task ExcluirAsync(int id)
        {
            _logger.LogInformation("Iniciando exclusão de usuário com ID {UsuarioId}.", id);
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
            {
                _logger.LogWarning("Usuário com ID {UsuarioId} não encontrado para exclusão.", id);
                throw new Exception("Usuário não encontrado.");
            }

            _usuarioRepository.Remover(usuario);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Usuário com ID {UsuarioId} excluído com sucesso.", id);
            _logger.LogInformation("Serviço de exclusão para cadastro de usuário finalizado.");
        }

        public async Task<IEnumerable<UsuarioResponse>> ListarAsync()
        {
            _logger.LogInformation("Iniciando listagem de usuários.");
            var usuarios = await _usuarioRepository.ObterTodosAsync();

            _logger.LogInformation("Total de usuários.", usuarios.Count());

            var listaResponse = new List<UsuarioResponse>();
            foreach (var usuario in usuarios)
            {
                listaResponse.Add(new UsuarioResponse
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                });
            }

            _logger.LogInformation("Serviço para listagem de usuários finalizado.");
            return listaResponse;
        }

        public async Task<UsuarioResponse> ObterPorIdAsync(int id)
        {
            _logger.LogInformation("Iniciando obtenção de usuário com ID {UsuarioId}.", id);
            var usuario = await _usuarioRepository.ObterPorIdAsync(id);
            if (usuario == null)
            {
                _logger.LogWarning("Usuário com ID {UsuarioId} não encontrado.", id);
                throw new Exception("Usuário não encontrado.");
            }

            _logger.LogInformation("Usuário com ID {UsuarioId} obtido com sucesso.", id);
            _logger.LogInformation("Serviço para obter cadastro de usuário finalizado.");

            return new UsuarioResponse
            {
                Id = usuario.Id,
                Email = usuario.Email,
            };
        }
    }
}