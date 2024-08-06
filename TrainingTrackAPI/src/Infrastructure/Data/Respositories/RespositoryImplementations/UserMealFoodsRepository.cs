using Microsoft.EntityFrameworkCore;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Domain.Entities;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations
{
    public class UserMealFoodsRepository : Repository<UserMealFood>, IUserMealFoodsRepository
    {
        public UserMealFoodsRepository(TrainingTrackDbContext context) : base(context) 
        {
        
        
        }

        public async Task<UserMealFood> CheckIfUserMealFoodExist(int id)
        {
            return await _context.UserMealFoods.FindAsync(id);
        }

        public async Task DeleteByMealId(int mealId)
        {
            var userMealFoods = await _context.UserMealFoods.Where(usf => usf.MealId == mealId).ToListAsync();
            _context.UserMealFoods.RemoveRange(userMealFoods);
        }

        public async Task DeleteByUserFoodId(int userFoodId)
        {
            var userMealFoods = await _context.UserMealFoods.Where(usf => usf.UserFoodId == userFoodId).ToListAsync();
            _context.UserMealFoods.RemoveRange(userMealFoods);
        }

        public async Task DeleteUserMealFood(int id)
        {
            var userMealFood = await this.CheckIfUserMealFoodExist(id);

            _context.UserMealFoods.Remove(userMealFood);
        }

        public async Task<List<UserMealFood>> GetAllUserMealFoodsForMeal(int mealId)
        {
            var result = await _context.UserMealFoods.Where(umf => umf.MealId == mealId).ToListAsync();

            if (result is null || result.Count == 0)
                return null;

            return result;
        }

        public async void UpdateUserMealFood(int id, decimal quantityInGrams)
        {
            var userMealFood = await this.CheckIfUserMealFoodExist(id);

            userMealFood.QuantityInGrams = quantityInGrams;
        }
    }
}
