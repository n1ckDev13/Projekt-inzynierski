using Microsoft.EntityFrameworkCore.Storage;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrainingTrackDbContext _context;
        
        

        public UnitOfWork(TrainingTrackDbContext context)
        {
            _context = context;
            
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.RollbackTransactionAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
