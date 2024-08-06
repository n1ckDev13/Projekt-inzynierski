using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices
{
    public interface IGetDietPlanService
    {
        Task<GetDietPlanResponse> GetDietPlanAsync(int id);
    }
}
