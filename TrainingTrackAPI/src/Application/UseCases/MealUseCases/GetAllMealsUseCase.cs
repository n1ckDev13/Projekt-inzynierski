using ClassLibrary.DTOs.MealDTO;
using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealsUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.MealUseCases
{
    public class GetAllMealsUseCase : IGetAllMealsUseCase
    {
        private readonly IDietPlansRepository _dietPlansRepository;
        private readonly IMealsRepository _mealsRepository;

        public GetAllMealsUseCase(IDietPlansRepository dietPlansRepository, IMealsRepository mealsRepository)
        {
            _dietPlansRepository = dietPlansRepository;
            _mealsRepository = mealsRepository;
        }

        public async Task<GetAllMealsResponse> GetAllMealsAsync(int dietPlanId)
        {
            try
            {
                var dietPlan = await _dietPlansRepository.CheckIfPlanExists(dietPlanId);

                if (dietPlan is null)
                    return new GetAllMealsResponse(false, "Diet plan does not exist.", null, null);

                var mealsList = await _mealsRepository.GetAllMealsForDietPlan(dietPlanId);

                if (mealsList is null)
                    return new GetAllMealsResponse(false, "No data returned.", null, null);

                var mealsDTOs = mealsList.Select(meal => new GetAllMealsDTO
                {
                    Id = meal.Id,
                    DietPlanId = meal.DietPlanId,
                    TimeOfEating = meal.TimeOfEating,
                    MealName = meal.MealName,
                }).ToList();

                return new GetAllMealsResponse(true, "Data returned.", mealsDTOs, null);
            }
            catch(Exception e)
            {

                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetAllMealsResponse(false, "Database error.", null, errors);

            }
        }
    }
}
