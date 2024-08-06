using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Services.DietPlanServices
{
    public class UpdateDietPlanService : IUpdateDietPlanService
    {
        private readonly IUpdateDietPlanUseCase _updateDietPlanUseCase;

        public UpdateDietPlanService(IUpdateDietPlanUseCase updateDietPlanService)
        {
            _updateDietPlanUseCase = updateDietPlanService;
        }

        public async Task<UpdateDietPlanResponse> UpdateDietPlanAsync(UpdateDietPlanDTO updateDietPlanDTO)
        {
            return await _updateDietPlanUseCase.UpdateDietPlanAsync(updateDietPlanDTO);
        }
    }
}
