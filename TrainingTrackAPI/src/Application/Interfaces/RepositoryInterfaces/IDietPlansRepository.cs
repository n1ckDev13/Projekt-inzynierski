using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces
{
    public interface IDietPlansRepository :IRepository<DietPlan>
    {
        Task<DietPlan> CheckIfPlanExists(int id);
        void UpdateDietPlan(int id, string planName, decimal calories, decimal protein,
            decimal carbs, decimal fat);

        void DisableDietPlan(int id);
        Task<List<DietPlan>> GetAllDietPlans(int userId);
    }
}
