using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces
{
    public interface IDeleteUserMealFoodUseCase
    {
        Task<DeleteUserMealFoodResponse> DeleteUserMealFoodAsync(int id);
    }
}
