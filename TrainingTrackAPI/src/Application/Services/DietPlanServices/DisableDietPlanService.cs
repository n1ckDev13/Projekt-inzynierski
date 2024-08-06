using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Services.DietPlanServices
{
    public class DisableDietPlanService : IDisableDietPlanService
    {
        private readonly IDisableDietPlanUseCase _disableDietPlanUseCase;

        public DisableDietPlanService(IDisableDietPlanUseCase disableDietPlanUseCase)
        {
            _disableDietPlanUseCase = disableDietPlanUseCase;
        }

        public async Task<DisableDietPlanResponse> DisableDietPlanAsync(int id)
        {
            return await _disableDietPlanUseCase.DisableDietPlanAsync(id);
        }
    }
}
