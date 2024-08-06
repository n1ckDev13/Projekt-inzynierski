using Microsoft.EntityFrameworkCore;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Domain.Entities;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations
{
    public class MealsRepository : Repository<Meal>, IMealsRepository
    {
        public MealsRepository(TrainingTrackDbContext context) : base(context) 
        {
        
        
        }

        public async Task<Meal> CheckIfMealExists(int id)
        {
            return await _context.Meals.FindAsync(id);
        }

        public async Task DeleteMeal(int id)
        {
            var meal = await this.CheckIfMealExists(id);

            _context.Meals.Remove(meal);
        }

        public async Task<List<Meal>> GetAllMealsForDietPlan(int dietPlanId)
        {
            var result = await _context.Meals.Where(m => m.DietPlanId == dietPlanId).ToListAsync();

            if (result is null || result.Count == 0)
                return null;

            return result;
        }

        public async void UpdateMeal(int id, TimeOnly timeOfEating, string mealName)
        {
            var meal = await this.CheckIfMealExists(id);

            meal.TimeOfEating = timeOfEating;
            meal.MealName = mealName;
        }
    }
}
