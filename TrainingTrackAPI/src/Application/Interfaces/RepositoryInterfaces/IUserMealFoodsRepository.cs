using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IUserMealFoodsRepository : IRepository<UserMealFood>
    {
        Task<UserMealFood> CheckIfUserMealFoodExist(int id);
        void UpdateUserMealFood(int id, decimal quantityInGrams);
        Task DeleteUserMealFood(int id);
        Task DeleteByMealId(int mealId);
        Task DeleteByUserFoodId(int userFoodId);
        Task<List<UserMealFood>> GetAllUserMealFoodsForMeal(int mealId);
    }
}
