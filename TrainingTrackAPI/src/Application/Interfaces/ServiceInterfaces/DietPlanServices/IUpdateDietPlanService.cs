using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices
{
    public interface IUpdateDietPlanService
    {
        Task<UpdateDietPlanResponse> UpdateDietPlanAsync(UpdateDietPlanDTO updateDietPlanDTO);
    }
}
