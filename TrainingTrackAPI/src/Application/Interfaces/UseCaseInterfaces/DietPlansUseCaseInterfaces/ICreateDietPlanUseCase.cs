using ClassLibrary.DTOs.DietPlanDTO;
using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces
{
    public interface ICreateDietPlanUseCase
    {
        Task<CreateDietPlanResponse> CreateDietPlanAsync(CreateDietPlanDTO createDietPlanDTO);
    }
}
