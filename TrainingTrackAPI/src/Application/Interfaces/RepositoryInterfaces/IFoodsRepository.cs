using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IFoodsRepository : IReadOnlyRepository<Food>
    {
        Task<Food> CheckIfFoodExists(int id);
    }
}
