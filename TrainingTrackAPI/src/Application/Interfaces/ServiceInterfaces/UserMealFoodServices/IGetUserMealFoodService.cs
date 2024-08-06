using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices
{
    public interface IGetUserMealFoodService
    {
        Task<GetUserMealFoodResponse> GetUserMealFoodAsync(int id);
    }
}
