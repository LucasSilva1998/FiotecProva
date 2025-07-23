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
    public class UsuarioRepository : IBaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Usuario> _dbSet;

        public UsuarioRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<Usuario>();
        }

        public async Task AdicionarAsync(Usuario entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Atualizar(Usuario entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<Usuario> ObterPorEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ObterTodosComPerfisAsync()
        {
            return await _dbSet.Include(u => u.Perfil).ToListAsync();
        }

        public void Remover(Usuario entity)
        {
            _dbSet.Remove(entity);
        }
    }
}