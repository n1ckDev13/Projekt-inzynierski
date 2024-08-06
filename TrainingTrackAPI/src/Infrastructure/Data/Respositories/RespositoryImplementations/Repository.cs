using Microsoft.EntityFrameworkCore;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        
        protected readonly TrainingTrackDbContext _context;

        public Repository(TrainingTrackDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var result = await _context.Set<TEntity>().ToListAsync();

            if (result is null || result.Count == 0)
                return null;

            return result;
        }

        public Task<TEntity> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
