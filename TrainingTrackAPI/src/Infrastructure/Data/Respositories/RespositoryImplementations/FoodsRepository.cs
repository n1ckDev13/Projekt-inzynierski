using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Domain.Entities;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations
{
    public class FoodsRepository : ReadOnlyRepository<Food>, IFoodsRepository 
    {
        public FoodsRepository(TrainingTrackDbContext context) : base(context) 
        {
        
        }

        public async Task<Food> CheckIfFoodExists(int id)
        {
            return await _context.Foods.FindAsync(id);
        }
    }
}
