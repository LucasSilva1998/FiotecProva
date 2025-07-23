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
    public class HorarioAtendimentoRepository : IBaseRepository<HorarioAtendimento>, IHorarioAtendimentoRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<HorarioAtendimento> _dbSet;

        public HorarioAtendimentoRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<HorarioAtendimento>();
        }

        public async Task AdicionarAsync(HorarioAtendimento entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Atualizar(HorarioAtendimento entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<HorarioAtendimento> ObterPorIdAsync(int id)
        {
            return await _dbSet
                .Include(h => h.Medico)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<HorarioAtendimento>> ObterPorMedicoIdAsync(int medicoId)
        {
            return await _dbSet
                 .Where(h => h.MedicoId == medicoId)
                 .ToListAsync();
        }

        public async Task<IEnumerable<HorarioAtendimento>> ObterTodosAsync()
        {
            return await _dbSet
                 .Include(h => h.Medico)
                 .ToListAsync();
        }

        public void Remover(HorarioAtendimento entity)
        {
            _dbSet.Remove(entity);
        }
    }
}