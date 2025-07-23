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
    public class ConsultaDomainService : IConsultaDomainService
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConsultaDomainService(IConsultaRepository consultaRepository, IMedicoRepository medicoRepository, IUnitOfWork unitOfWork) 
        {
            _consultaRepository = consultaRepository;
            _medicoRepository = medicoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AgendarConsultaAsync(Consulta consulta)
        {
            var medicoDisponivel = await _medicoRepository.MedicoDisponivelAsync(consulta.MedicoId, consulta.DataHoraConsulta);
            if (!medicoDisponivel)
                throw new Exception("O médico não está disponível neste horário.");

            var conflito = await _consultaRepository.ExisteConsultaNoHorarioAsync(consulta.MedicoId, consulta.DataHoraConsulta);
            if (conflito)
                throw new Exception("Já existe uma consulta agendada neste horário com este médico.");

            await _consultaRepository.AdicionarAsync(consulta);
            await _unitOfWork.CommitAsync();
        }
    }
}
