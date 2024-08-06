using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices
{
    public interface ICreateUserFoodService
    {
        Task<CreateUserFoodResponse> CreateUserFoodAsync(CreateUserFoodDTO createUserFoodDTO);
    }
}
