using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices
{
    public interface IGetMealFoodService
    {
        Task<GetMealFoodResponse> GetMealFoodAsync(int id);
    }
}
