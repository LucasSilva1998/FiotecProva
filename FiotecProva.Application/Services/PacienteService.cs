using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Application.Interfaces;
using FiotecProva.Application.Mappers;
using FiotecProva.Domain.Interfaces.Core;
using FiotecProva.Domain.Interfaces.Repository;
using FiotecProva.Domain.ValueObjects;
using FiotecProva.Infra.Data.ExternalServices.ViaCep.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IViaCepService _viaCepService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<PacienteService> _logger;

        public PacienteService(IPacienteRepository pacienteRepository, IViaCepService viaCepService, IUnitOfWork unitOfWork, IPerfilRepository perfilRepository, IUsuarioRepository usuarioRepository, ILogger<PacienteService> logger)
        {
            _pacienteRepository = pacienteRepository;
            _viaCepService = viaCepService;
            _unitOfWork = unitOfWork;
            _perfilRepository = perfilRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        public async Task AtualizarAsync(int id, PacienteRequest request)
        {
            _logger.LogInformation("Iniciando atualização de paciente. ID: {Id}, CPF: {Cpf}", id, request.Cpf);
            _logger.LogInformation("Atualizando paciente. ID: {Id}", id);

            var paciente = await _pacienteRepository.ObterPorIdAsync(id);
            if (paciente == null)
            {
                _logger.LogWarning("Paciente não encontrado para atualização. ID: {Id}", id);
                throw new Exception("Paciente não encontrado.");
            }

            if (!string.Equals(paciente.Cpf.Numero, request.Cpf, StringComparison.OrdinalIgnoreCase))
            {
                var outro = await _pacienteRepository.ObterPorCpfAsync(request.Cpf);
                if (outro != null && outro.Id != id)
                {
                    _logger.LogWarning("Tentativa de atualizar CPF duplicado. Novo CPF: {Cpf}", request.Cpf);
                    throw new Exception("Já existe outro paciente com esse CPF.");
                }
            }

            var enderecoResponse = await _viaCepService.BuscarEnderecoPorCepAsync(request.Cep);
            if (enderecoResponse == null)
            {
                _logger.LogWarning("CEP inválido ao atualizar paciente. CEP: {Cep}", request.Cep);
                throw new Exception("CEP inválido.");
            }

            var endereco = EnderecoMapper.MapFrom(enderecoResponse, request.Numero);

            paciente.Nome = request.Nome;
            paciente.DataNascimento = request.DataNascimento;
            paciente.Telefone = request.Telefone;
            paciente.Cpf = new Cpf(request.Cpf);
            paciente.Endereco = endereco;

            _pacienteRepository.Atualizar(paciente);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Paciente atualizado com sucesso. ID: {Id}", id);
            _logger.LogInformation("Serviço de atualização de paciente finalizado.");

        }

        public async Task<int> CadastrarAsync(PacienteRequest request)
        {
            _logger.LogInformation("Iniciando cadastro de paciente. CPF: {Cpf}", request.Cpf);

            var existente = await _pacienteRepository.ObterPorCpfAsync(request.Cpf);
            if (existente != null)
            {
                _logger.LogWarning("Cadastro de paciente abortado. Já existe paciente com CPF: {Cpf}", request.Cpf);
                throw new Exception("Já existe um paciente com esse CPF.");
            }

            var enderecoResponse = await _viaCepService.BuscarEnderecoPorCepAsync(request.Cep);
            if (enderecoResponse == null)
            {
                _logger.LogWarning("CEP inválido informado: {Cep}", request.Cep);
                throw new Exception("CEP inválido.");
            }

            var perfil = await _perfilRepository.ObterPorNomeAsync("Paciente");
            if (perfil == null)
            {
                _logger.LogError("Perfil 'Paciente' não encontrado no banco.");
                throw new Exception("Perfil 'Paciente' não encontrado.");
            }

            var endereco = EnderecoMapper.MapFrom(enderecoResponse, request.Numero);
            var usuario = UsuarioMapper.MapFrom(request.Usuario, perfil);
            var paciente = PacienteMapper.MapFrom(request, endereco);

            paciente.Usuario = usuario;

            await _pacienteRepository.AdicionarAsync(paciente);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Paciente cadastrado com sucesso. ID: {PacienteId}", paciente.Id);
            _logger.LogInformation("Serviço de criação para cadastro de médico finalizado.");


            return paciente.Id;

        }

        public async Task<IEnumerable<PacienteResponse>> ListarAsync()
        {
            _logger.LogInformation("Iniciando listagem de pacientes.");
            _logger.LogInformation("Listando todos os pacientes.");
            var pacientes = await _pacienteRepository.ObterTodosAsync();
            return pacientes.Select(PacienteMapper.MapToResponse);
        }

        public async Task<PacienteResponse> ObterPorIdAsync(int id)
        {
            _logger.LogInformation("Iniciando serviço para busca de paciente.");
            _logger.LogInformation("Buscando paciente por ID: {Id}", id);

            var paciente = await _pacienteRepository.ObterPorIdAsync(id);
            if (paciente == null)
            {
                _logger.LogWarning("Paciente não encontrado. ID: {Id}", id);
                throw new Exception("Paciente não encontrado.");
            }

            _logger.LogInformation("Serviço para localizar médico finalizado.");

            return PacienteMapper.MapToResponse(paciente);
        }

        public async Task RemoverAsync(int id)
        {
            _logger.LogInformation("Iniciando serviço de remoção para cadastro de médico.");
            _logger.LogInformation("Removendo paciente. ID: {Id}", id);

            var paciente = await _pacienteRepository.ObterPorIdAsync(id);
            if (paciente == null)
            {
                _logger.LogWarning("Tentativa de remover paciente não encontrado. ID: {Id}", id);
                throw new Exception("Paciente não encontrado.");
            }

            _pacienteRepository.Remover(paciente);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Paciente removido com sucesso. ID: {Id}", id);
            _logger.LogInformation("Serviço de remoção para cadastro de médico finalizado.");

        }
    }
}