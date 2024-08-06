using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices
{
    public interface IDeleteUserMealFoodService
    {
        Task<DeleteUserMealFoodResponse> DeleteUserMealFoodAsync(int id);
    }
}
