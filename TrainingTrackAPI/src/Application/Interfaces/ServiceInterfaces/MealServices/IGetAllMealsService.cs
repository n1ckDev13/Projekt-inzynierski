using ClassLibrary.Responses.Meal;
using TrainingTrackAPI.Application.UseCases.MealUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealServices
{
    public interface IGetAllMealsService
    {
        Task<GetAllMealsResponse> GetAllMealsAsync(int dietPlanId);
    }
}
