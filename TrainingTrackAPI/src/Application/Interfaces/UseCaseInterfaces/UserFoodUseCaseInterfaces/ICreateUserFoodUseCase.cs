using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces
{
    public interface ICreateUserFoodUseCase
    {
        Task<CreateUserFoodResponse> CreateUserFoodAsync(CreateUserFoodDTO createUserFoodDTO);
    }
}
