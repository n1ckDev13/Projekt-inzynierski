using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces
{
    public interface IGetAllUserMealFoodsUseCase
    {
        Task<GetAllUserMealFoodsResponse> GetAllUserMealFoodsAsync(int mealId);
    }
}
