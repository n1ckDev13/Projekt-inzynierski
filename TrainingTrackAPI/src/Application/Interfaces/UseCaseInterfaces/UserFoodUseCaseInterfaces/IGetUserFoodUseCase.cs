using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces
{
    public interface IGetUserFoodUseCase
    {
        Task<GetUserFoodResponse> GetUserFoodAsync(int id);
    }
}
