using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IMealsRepository : IRepository<Meal>
    {
        Task<Meal> CheckIfMealExists(int id);
        void UpdateMeal(int id, TimeOnly timeOfEating, string mealName);
        Task DeleteMeal(int id);
        Task<List<Meal>> GetAllMealsForDietPlan(int dietPlanId);

    }
}
