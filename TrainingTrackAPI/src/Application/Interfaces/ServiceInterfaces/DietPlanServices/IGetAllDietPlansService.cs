using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.DietPlanServices
{
    public interface IGetAllDietPlansService
    {
        Task<GetAllDietPlansResponse> GetAllDietPlansAsync(int userId);
    }
}
