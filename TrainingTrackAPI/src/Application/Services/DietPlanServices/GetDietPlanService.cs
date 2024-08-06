using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Services.DietPlanServices
{
    public class GetDietPlanService : IGetDietPlanService
    {
        private readonly IGetDietPlanUseCase _getDietPlanUseCase;

        public GetDietPlanService(IGetDietPlanUseCase getDietPlanUseCase)
        {
            _getDietPlanUseCase = getDietPlanUseCase;
        }

        public async Task<GetDietPlanResponse> GetDietPlanAsync(int id)
        {
            return await _getDietPlanUseCase.GetDietPlanAsync(id);
        }
    }
}
