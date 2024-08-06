using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserFoodsRepository : IRepository<UserFood>
    {
        Task<UserFood> CheckIfUserFoodExists(int id);
        void UpdateUserFood(int id, string foodName, decimal caloriesPer100g, decimal proteinPer100g,
            decimal carbsPer100g, decimal fatPer100g);
        Task DeleteUserFood(int id);
        Task<List<UserFood>> GetAllUserFoodsForUser(int userId);
    }
}
