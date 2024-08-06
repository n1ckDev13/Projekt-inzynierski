using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices
{
    public interface IGetAllUserMealFoodsService
    {
        Task<GetAllUserMealFoodsResponse> GetAllUserMealFoodAsync(int mealId);
    }
}
