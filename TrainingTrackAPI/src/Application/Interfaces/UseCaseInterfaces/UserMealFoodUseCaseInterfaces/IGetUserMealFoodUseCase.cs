using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces
{
    public interface IGetUserMealFoodUseCase
    {
        Task<GetUserMealFoodResponse> GetUserMealFoodAsync(int id);
    }
}
