using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices
{
    public interface IDeleteMealFoodService
    {
        Task<DeleteMealFoodResponse> DeleteMealFoodAsync(int id);
    }
}
