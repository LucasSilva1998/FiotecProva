using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Application.Interfaces;
using FiotecProva.Application.Mappers;
using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Enums;
using FiotecProva.Domain.Interfaces.Core;
using FiotecProva.Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly ILogger<MedicoService> _logger;

        public MedicoService(IMedicoRepository medicoRepository, IUnitOfWork unitOfWork, IUsuarioRepository usuarioRepository, IPerfilRepository perfilRepository, ILogger<MedicoService> logger)
        {
            _medicoRepository = medicoRepository;
            _unitOfWork = unitOfWork;
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _logger = logger;
        }

        public async Task AtualizarAsync(int id, MedicoRequest request)
        {
            _logger.LogInformation("Iniciando atualização do médico com ID: {MedicoId}", id);
            _logger.LogInformation("Atualizando médico com ID: {MedicoId}", id);

            var medico = await _medicoRepository.ObterPorIdAsync(id);
            if (medico == null)
            {
                _logger.LogWarning("Médico com ID {MedicoId} não encontrado para atualização.", id);
                throw new Exception("Médico não encontrado.");
            }

            if (!string.Equals(medico.NumeroCRM, request.NumeroCRM, StringComparison.OrdinalIgnoreCase))
            {
                var outro = await _medicoRepository.ObterPorCRMAsync(request.NumeroCRM);
                if (outro != null && outro.Id != id)
                {
                    _logger.LogWarning("Já existe outro médico com o CRM {CRM}.", request.NumeroCRM);
                    throw new Exception("Já existe outro médico com esse CRM.");
                }
            }

            if (!Enum.TryParse<EspecialidadeMedica>(request.Especialidade, true, out var especialidadeEnum))
            {
                _logger.LogWarning("Especialidade inválida na atualização: {Especialidade}", request.Especialidade);
                throw new Exception("Especialidade inválida.");
            }

            medico.Nome = request.Nome;
            medico.DataNascimento = request.DataNascimento;
            medico.NumeroCRM = request.NumeroCRM;
            medico.AnosExperiencia = request.AnosExperiencia;
            medico.Especialidade = especialidadeEnum;

            medico.HorariosAtendimento = request.HorariosAtendimento.Select(h => new HorarioAtendimento
            {
                HoraInicio = TimeSpan.Parse(h),
                HoraFim = TimeSpan.Parse(h).Add(TimeSpan.FromHours(1)),
                MedicoId = medico.Id
            }).ToList();

            _medicoRepository.Atualizar(medico);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Médico com ID {MedicoId} atualizado com sucesso.", id);
            _logger.LogInformation("Serviço de atualização de médico finalizado.");

        }

        public async Task<int> CriarAsync(MedicoRequest request)
        {
            _logger.LogInformation("Iniciando criação do médico com CRM: {CRM}", request.NumeroCRM);

            var existente = await _medicoRepository.ObterPorCRMAsync(request.NumeroCRM);
            if (existente != null)
            {
                _logger.LogWarning("Tentativa de criação de médico com CRM já existente: {CRM}", request.NumeroCRM);
                throw new Exception("Já existe um médico cadastrado com esse CRM.");
            }

            if (!Enum.TryParse<EspecialidadeMedica>(request.Especialidade, true, out var especialidadeEnum))
            {
                _logger.LogWarning("Especialidade inválida informada: {Especialidade}", request.Especialidade);
                throw new Exception("Especialidade inválida.");
            }

            var perfil = await _perfilRepository.ObterPorNomeAsync("Medico");
            if (perfil == null)
            {
                _logger.LogError("Perfil 'Medico' não encontrado.");
                throw new Exception("Perfil 'Medico' não encontrado.");
            }

            var usuario = UsuarioMapper.MapFrom(request.Usuario, perfil);

            var medico = MedicoMapper.MapFrom(request, especialidadeEnum);

            medico.Usuario = usuario;

            await _medicoRepository.AdicionarAsync(medico);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Médico criado com sucesso. ID: {MedicoId}", medico.Id);
            _logger.LogInformation("Serviço de criação para cadastro de médico finalizado.");

            return medico.Id;
        }

        public async Task<IEnumerable<MedicoResponse>> ListarAsync()
        {
            _logger.LogInformation("Iniciando serviço de listagem de médicos.");
            _logger.LogInformation("Listando todos os médicos.");

            var medicos = await _medicoRepository.ObterTodosAsync();

            _logger.LogInformation("Serviço de listagem de médico finalizado.");

            return medicos.Select(MedicoMapper.MapFrom);
        }

        public async Task<MedicoResponse> ObterPorIdAsync(int id)
        {
            _logger.LogInformation("Iniciando serviço de busca de médico por ID.");
            _logger.LogInformation("Buscando médico por ID: {MedicoId}", id);

            var medico = await _medicoRepository.ObterPorIdAsync(id);
            if (medico == null)
            {
                _logger.LogWarning("Médico com ID {MedicoId} não encontrado.", id);
                throw new Exception("Médico não encontrado.");
            }

            _logger.LogInformation("Serviço de busca de médico finalizado.");

            return MedicoMapper.MapFrom(medico);
        }

        public async Task RemoverAsync(int id)
        {
            _logger.LogInformation("Iniciando serviço de remoção de médico.");
            _logger.LogInformation("Removendo médico com ID: {MedicoId}", id);

            var medico = await _medicoRepository.ObterPorIdAsync(id);
            if (medico == null)
            {
                _logger.LogWarning("Médico com ID {MedicoId} não encontrado para remoção.", id);
                throw new Exception("Médico não encontrado.");
            }

            _medicoRepository.Remover(medico);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Médico com ID {MedicoId} removido com sucesso.", id);
            _logger.LogInformation("Serviço de remoção para cadastro de médico finalizado.");

        }
    }
}