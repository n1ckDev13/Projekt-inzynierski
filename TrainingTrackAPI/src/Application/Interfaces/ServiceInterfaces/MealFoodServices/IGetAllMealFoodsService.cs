using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.MealFoodServices
{
    public interface IGetAllMealFoodsService
    {
        Task<GetAllMealFoodsResponse> GetAllMealFoodsAsync(int mealId);
    }
}
