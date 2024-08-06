using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces
{
    public interface IUpdateDietPlanUseCase
    {
        Task<UpdateDietPlanResponse> UpdateDietPlanAsync(UpdateDietPlanDTO updateDietPlanDTO);
    }
}
