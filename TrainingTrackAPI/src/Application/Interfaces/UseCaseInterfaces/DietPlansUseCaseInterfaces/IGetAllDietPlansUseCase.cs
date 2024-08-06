using ClassLibrary.Responses.DietPlan;
using TrainingTrackAPI.Application.UseCases.DietPlansUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.DietPlansUseCaseInterfaces
{
    public interface IGetAllDietPlansUseCase
    {
        Task<GetAllDietPlansResponse> GetAllDietPlansAsync(int userId);
    }
}
