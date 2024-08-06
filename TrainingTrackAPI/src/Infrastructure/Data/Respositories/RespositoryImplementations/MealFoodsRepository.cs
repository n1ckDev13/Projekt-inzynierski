using Microsoft.EntityFrameworkCore;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Domain.Entities;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations
{
    public class MealFoodsRepository : Repository<MealFood>, IMealFoodsRepository
    {

        public MealFoodsRepository(TrainingTrackDbContext context) : base(context) 
        {
        
        
        }

        public async Task<MealFood> CheckIfMealFoodExist(int id)
        {
            return await _context.MealFoods.FindAsync(id);
        }

        public async Task DeleteByMealId(int mealId)
        {
            var mealFoods = await _context.MealFoods.Where(mf => mf.MealId == mealId).ToListAsync();
            _context.MealFoods.RemoveRange(mealFoods);
        }

        public async Task DeleteMealFood(int id)
        {
            var mealFood = await this.CheckIfMealFoodExist(id);

            _context.MealFoods.Remove(mealFood);
        }

        public async Task<List<MealFood>> GetAllMealFoodsForMeal(int mealId)
        {
            var result = await _context.MealFoods.Where(mf => mf.MealId == mealId).ToListAsync();

            if (result is null || result.Count == 0)
                return null;

            return result;
        }

        public async void UpdateMealFood(int id, decimal quantityInGrams)
        {
            var mealFood = await this.CheckIfMealFoodExist(id);

            mealFood.QuantityInGrams = quantityInGrams;
        }
    }
}
