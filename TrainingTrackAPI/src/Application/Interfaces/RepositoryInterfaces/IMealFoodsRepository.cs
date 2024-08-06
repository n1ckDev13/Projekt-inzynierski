using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IMealFoodsRepository : IRepository<MealFood>
    {
        Task DeleteByMealId(int mealId);
        Task<MealFood> CheckIfMealFoodExist(int id);
        void UpdateMealFood(int id, decimal quantityInGrams);
        Task DeleteMealFood(int id);
        Task<List<MealFood>> GetAllMealFoodsForMeal(int mealId);
    }
}
