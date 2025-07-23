using FiotecProva.Domain.Interfaces.Core;
using FiotecProva.Domain.Interfaces.Repository;
using FiotecProva.Infra.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiotecProva.Infra.Data.Repositories.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IDbContextTransaction _transaction;



        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public IConsultaRepository ConsultaRepository => new ConsultaRepository(_context);

        public IHorarioAtendimentoRepository HorarioAtendimentoRepository => new HorarioAtendimentoRepository(_context);

        public IMedicoRepository MedicoRepository => new MedicoRepository(_context);

        public IPacienteRepository PacienteRepository => new PacienteRepository(_context);

        public IUsuarioRepository UsuarioRepository => new UsuarioRepository(_context);

        public IPerfilRepository PerfilRepository => new PerfilRepository(_context);


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
