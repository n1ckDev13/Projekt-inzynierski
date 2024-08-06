using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.MealUseCases
{
    public class GetMealUseCase : IGetMealUseCase
    {
        private readonly IMealsRepository _mealsRepository;

        public GetMealUseCase(IMealsRepository mealsRepository)
        {
            _mealsRepository = mealsRepository;
        }

        public async Task<GetMealResponse> GetMealAsync(int id)
        {
            try
            {
                var meal = await _mealsRepository.CheckIfMealExists(id);

                if (meal is null)
                    return new GetMealResponse(false, "Meal does not exist.", null, null);

                var mealResult = new GetAllMealsDTO();
                mealResult.Id = meal.Id;
                mealResult.DietPlanId = meal.DietPlanId;
                mealResult.TimeOfEating = meal.TimeOfEating;
                mealResult.MealName = meal.MealName;

                return new GetMealResponse(true, "Data returned.", mealResult, null);
            }
            catch(Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetMealResponse(false, "Database error.", null, errors);
            }
        }
    }
}
