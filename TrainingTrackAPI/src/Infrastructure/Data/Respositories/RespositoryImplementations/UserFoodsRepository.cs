using Microsoft.EntityFrameworkCore;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Domain.Entities;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations
{
    public class UserFoodsRepository : Repository<UserFood>, IUserFoodsRepository
    {
        public UserFoodsRepository(TrainingTrackDbContext context) : base(context) 
        {
        
        
        }

        public async Task<UserFood> CheckIfUserFoodExists(int id)
        {
            return await _context.UserFoods.FindAsync(id);
        }

        public async Task DeleteUserFood(int id)
        {
            var userFood = await this.CheckIfUserFoodExists(id);

            _context.UserFoods.Remove(userFood);
        }

        public async Task<List<UserFood>> GetAllUserFoodsForUser(int userId)
        {
            var result = await _context.UserFoods.Where(uf => uf.UserId == userId).ToListAsync();

            if (result is null || result.Count == 0)
                return null;

            return result;
        }

        public async void UpdateUserFood(int id, string foodName, decimal caloriesPer100g, decimal proteinPer100g, decimal carbsPer100g, decimal fatPer100g)
        {
            var userFood = await this.CheckIfUserFoodExists(id);

            userFood.FoodName = foodName;
            userFood.CaloriesPer100g = caloriesPer100g;
            userFood.ProteinPer100g = proteinPer100g;
            userFood.CarbsPer100g = carbsPer100g;
            userFood.FatPer100g = fatPer100g;


        }
    }
}
