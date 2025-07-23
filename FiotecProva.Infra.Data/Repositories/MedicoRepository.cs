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
    public class MedicoRepository : IBaseRepository<Medico>, IMedicoRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Medico> _dbSet;

        public MedicoRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<Medico>();
        }

        public async Task AdicionarAsync(Medico entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Atualizar(Medico entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<bool> MedicoDisponivelAsync(int medicoId, DateTime dataHora)
        {
            return !await _context.Consultas
                .AnyAsync(c => c.MedicoId == medicoId && c.DataHoraConsulta == dataHora && c.Status != Domain.Enums.StatusConsulta.Cancelada);
        }

        public async Task<Medico> ObterPorCRMAsync(string numeroCRM)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.NumeroCRM == numeroCRM);
        }

        public async Task<Medico> ObterPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Medico>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Medico>> ObterTodosComConsultasAsync()
        {
            return await _dbSet.Include(m => m.Consultas).ToListAsync();
        }

        public void Remover(Medico entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
