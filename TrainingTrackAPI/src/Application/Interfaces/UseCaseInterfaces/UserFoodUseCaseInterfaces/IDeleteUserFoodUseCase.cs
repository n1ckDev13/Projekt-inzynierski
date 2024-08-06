using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces
{
    public interface IDeleteUserFoodUseCase
    {
        Task<DeleteUserFoodResponse> DeleteUserFoodAsync(int id);
    }
}
