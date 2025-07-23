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
    public class PerfilRepository : IBaseRepository<Perfil>, IPerfilRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Perfil> _dbSet;

        public PerfilRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<Perfil>();
        }

        public async Task AdicionarAsync(Perfil entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Atualizar(Perfil entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<Perfil> ObterPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Perfil> ObterPorNomeAsync(string nome)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Nome == nome);
        }

        public async Task<IEnumerable<Perfil>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remover(Perfil entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
