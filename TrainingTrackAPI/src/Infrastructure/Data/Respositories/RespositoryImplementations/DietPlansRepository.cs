using Microsoft.EntityFrameworkCore;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Domain.Entities;
using TrainingTrackAPI.Infrastructure.DbContexts;

namespace TrainingTrackAPI.Infrastructure.Data.Respositories.RespositoryImplementations
{
    public class DietPlansRepository : Repository<DietPlan>, IDietPlansRepository
    {
        public DietPlansRepository(TrainingTrackDbContext context) : base(context) 
        {
        
        }

        public async Task<DietPlan> CheckIfPlanExists(int id)
        {
            return await _context.DietPlans.FindAsync(id);
        }

        public async void DisableDietPlan(int id)
        {
            var dietPlan = await this.CheckIfPlanExists(id);

            dietPlan.IsDisabled = true;
        }

        public async Task<List<DietPlan>> GetAllDietPlans(int userId)
        {
            var result = await _context.DietPlans.Where(dp => dp.UserId == userId 
            && dp.IsDisabled == false).ToListAsync();

            if (result is null || result.Count == 0)
                return null;

            return result;
        }

        public async void UpdateDietPlan(int id, string planName, decimal calories, decimal protein, decimal carbs,
            decimal fat)
        {
            var dietPlan = await this.CheckIfPlanExists(id);

            dietPlan.PlanName = planName;
            dietPlan.Calories = calories;
            dietPlan.Protein = protein;
            dietPlan.Carbs = carbs;
            dietPlan.Fat = fat;
        }
    }
}
