using ClassLibrary.Responses.MealFood;
using TrainingTrackAPI.Application.UseCases.MealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.MealFoodsUseCaseInterfaces
{
    public interface IGetAllMealFoodsUseCase
    {
        Task<GetAllMealFoodsResponse> GetAllMealFoodsAsync(int mealId);
    }
}
