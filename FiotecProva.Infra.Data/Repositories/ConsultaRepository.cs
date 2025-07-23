using FiotecProva.Domain.Entities;
using FiotecProva.Domain.Interfaces.Core;
using FiotecProva.Domain.Interfaces.Repository;
using FiotecProva.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Infra.Data.Repositories
{
    public class ConsultaRepository : IBaseRepository<Consulta>, IConsultaRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Consulta> _dbSet;

        public ConsultaRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<Consulta>();
        }

        public async Task AdicionarAsync(Consulta entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Atualizar(Consulta entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Consultas.CountAsync();
        }

        public async Task<bool> ExisteConsultaNoHorarioAsync(int medicoId, DateTime dataHora)
        {
            return await _dbSet.AnyAsync(c => c.MedicoId == medicoId && c.DataHoraConsulta == dataHora);
        }

        public async Task<IEnumerable<Consulta>> ListarPorMedicoAsync(int medicoId, int pagina, int tamanhoPagina)
        {
            return await _dbSet
                  .Where(c => c.MedicoId == medicoId)
                  .Include(c => c.Paciente)
                  .OrderBy(c => c.DataHoraConsulta)
                  .Skip((pagina - 1) * tamanhoPagina)
                  .Take(tamanhoPagina)
                  .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>> ListarPorPacienteAsync(int pacienteId, int pagina, int tamanhoPagina)
        {
            return await _dbSet
                  .Where(c => c.PacienteId == pacienteId)
                  .Include(c => c.Medico)
                  .OrderBy(c => c.DataHoraConsulta)
                  .Skip((pagina - 1) * tamanhoPagina)
                  .Take(tamanhoPagina)
                  .ToListAsync();
        }

        public async Task<IEnumerable<Consulta>> ObterPaginadoAsync(int page, int pageSize)
        {
            return await _context.Consultas
                  .Include(c => c.Medico)
                  .Include(c => c.Paciente)
                  .OrderBy(c => c.DataHoraConsulta)
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
        }

        public async Task<Consulta> ObterPorIdAsync(int id)
        {
            return await _dbSet
                 .Include(c => c.Medico)
                 .Include(c => c.Paciente)
                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Consulta>> ObterTodosAsync()
        {
            return await _dbSet
                            .Include(c => c.Medico)
                            .Include(c => c.Paciente)
                            .ToListAsync();
        }

        public void Remover(Consulta entity)
        {
            _dbSet.Remove(entity);
        }
    }
}