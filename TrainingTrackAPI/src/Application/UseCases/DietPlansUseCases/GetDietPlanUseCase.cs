using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.DietPlansUseCases
{
    public class GetDietPlanUseCase : IGetDietPlanUseCase
    {
        private readonly IDietPlansRepository _dietPlansRepository;

        public GetDietPlanUseCase(IDietPlansRepository dietPlansRepository)
        {
            _dietPlansRepository = dietPlansRepository;
        }

        public async Task<GetDietPlanResponse> GetDietPlanAsync(int id)
        {
            try
            {

                var dietPlan = await _dietPlansRepository.CheckIfPlanExists(id);

                if (dietPlan is null)
                    return new GetDietPlanResponse(false, "Diet plan does not exist.", null, null);

                var dietPlanResult = new GetAllDietPlansDTO();
                dietPlanResult.Id = dietPlan.Id;
                dietPlanResult.UserId = dietPlan.UserId;
                dietPlanResult.PlanName = dietPlan.PlanName;
                dietPlanResult.IsDisabled = dietPlan.IsDisabled;
                dietPlanResult.Calories = dietPlan.Calories;
                dietPlanResult.Protein = dietPlan.Protein;
                dietPlanResult.Carbs = dietPlan.Carbs;
                dietPlanResult.Fat = dietPlan.Fat;

                return new GetDietPlanResponse(true, "Data returned.", dietPlanResult, null);

            }
            catch(Exception e)
            {
                var errors = new List<string>();
                errors.Add(e.Message);
                return new GetDietPlanResponse(false, "Database error.", null, errors);
            }
        }
    }
}
