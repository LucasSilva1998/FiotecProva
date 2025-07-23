using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Application.Interfaces;
using FiotecProva.Application.Mappers;
using FiotecProva.Domain.Enums;
using FiotecProva.Domain.Interfaces.Core;
using FiotecProva.Domain.Interfaces.Repository;
using FiotecProva.Domain.Paginations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConsultaService> _logger;

        public ConsultaService(IConsultaRepository consultaRepository, IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository, IUnitOfWork unitOfWork, ILogger<ConsultaService> logger)
        {
            _consultaRepository = consultaRepository;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<int> AgendarAsync(ConsultaRequest request)
        {
            _logger.LogInformation("Iniciando agendamento de consulta para MédicoId: {MedicoId}, PacienteId: {PacienteId}, DataHora: {DataHora}",
                            request.MedicoId, request.PacienteId, request.DataHoraConsulta);

            var medico = await _medicoRepository.ObterPorIdAsync(request.MedicoId);
            if (medico == null)
            {
                _logger.LogWarning("Médico com ID {MedicoId} não encontrado.", request.MedicoId);
                throw new Exception("Médico não encontrado.");
            }

            var paciente = await _pacienteRepository.ObterPorIdAsync(request.PacienteId);
            if (paciente == null)
            {
                _logger.LogWarning("Paciente com ID {PacienteId} não encontrado.", request.PacienteId);
                throw new Exception("Paciente não encontrado.");
            }

            var existe = await _consultaRepository.ExisteConsultaNoHorarioAsync(request.MedicoId, request.DataHoraConsulta);
            if (existe)
            {
                _logger.LogWarning("Consulta já existente para o MédicoId: {MedicoId} no horário {DataHora}", request.MedicoId, request.DataHoraConsulta);
                throw new Exception("Já existe uma consulta agendada para este médico neste horário.");
            }

            var consulta = ConsultaMapper.MapFrom(request);
            await _consultaRepository.AdicionarAsync(consulta);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Consulta agendada com sucesso. ConsultaId: {ConsultaId}", consulta.Id);
            _logger.LogInformation("Serviço de agendamento finalizado.");

            return consulta.Id;
        }

        public async Task CancelarAsync(int id, ConsultaCancelamentoRequest request)
        {
            _logger.LogInformation("Iniciando cancelamento da consulta {ConsultaId}", id);

            var consulta = await _consultaRepository.ObterPorIdAsync(id);
            if (consulta == null)
            {
                _logger.LogWarning("Consulta com ID {ConsultaId} não encontrada para cancelamento.", id);
                throw new Exception("Consulta não encontrada.");
            }

            if (consulta.Status != StatusConsulta.Agendada)
            {
                _logger.LogWarning("Consulta {ConsultaId} não está agendada. Status atual: {Status}", id, consulta.Status);
                throw new Exception("Apenas consultas agendadas podem ser canceladas.");
            }

            if (string.IsNullOrWhiteSpace(request.Justificativa))
            {
                _logger.LogWarning("Justificativa não informada para cancelamento da consulta {ConsultaId}", id);
                throw new Exception("Justificativa de cancelamento é obrigatória.");
            }

            ConsultaMapper.AplicarCancelamento(consulta, request);
            _consultaRepository.Atualizar(consulta);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Consulta {ConsultaId} cancelada com sucesso. Justificativa: {Justificativa}", id, request.Justificativa);
            _logger.LogInformation("Serviço de cancelamento de consulta finalizado.");

        }

        public async Task<PagedList<ConsultaResponse>> ListarAsync(PaginationFilter filtro)
        {
            _logger.LogInformation("Iniciando serviço de listagem de consultas.");
            _logger.LogInformation("Listando consultas - Página: {Page}, Tamanho: {PageSize}", filtro.Page, filtro.PageSize);

            var totalCount = await _consultaRepository.ContarAsync();
            var consultas = await _consultaRepository.ObterPaginadoAsync(filtro.Page, filtro.PageSize);
            var itens = consultas.Select(ConsultaMapper.MapFrom);

            _logger.LogInformation("Consultas listadas com sucesso. Total: {Total}", totalCount);
            _logger.LogInformation("Serviço de listagem de consulta finalizado.");

            return new PagedList<ConsultaResponse>(filtro.Page, filtro.PageSize, totalCount, itens);
        }

        public async Task<ConsultaResponse> ObterPorIdAsync(int id)
        {
            _logger.LogInformation("Iniciando serviço de busca de consulta por ID.");
            _logger.LogInformation("Buscando consulta por ID: {ConsultaId}", id);

            var consulta = await _consultaRepository.ObterPorIdAsync(id);
            if (consulta == null)
            {
                _logger.LogWarning("Consulta com ID {ConsultaId} não encontrada.", id);
                throw new Exception("Consulta não encontrada.");
            }

            _logger.LogInformation("Consulta {ConsultaId} encontrada com sucesso.", id);
            _logger.LogInformation("Serviço de busca de consulta finalizado.");

            return ConsultaMapper.MapFrom(consulta);
        }
    }
}
