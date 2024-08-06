using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces
{
    public interface ICreateUserMealFoodUseCase
    {
        Task<CreateUserMealFoodResponse> CreateUserMealFoodAsync(CreateUserMealFoodDTO createUserMealFoodDTO);
    }
}
