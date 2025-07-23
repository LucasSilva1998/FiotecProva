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
    public class PacienteRepository : IBaseRepository<Paciente>, IPacienteRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Paciente> _dbSet;

        public PacienteRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<Paciente>();
        }

        public async Task AdicionarAsync(Paciente entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Atualizar(Paciente entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<bool> ExisteCpfAsync(string cpf)
        {
            return await _dbSet.AnyAsync(p => p.Cpf.Numero == cpf);
        }

        public async Task<Paciente> ObterPorCpfAsync(string cpf)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Cpf.Numero == cpf);
        }

        public async Task<Paciente> ObterPorIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Paciente>> ObterTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remover(Paciente entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
