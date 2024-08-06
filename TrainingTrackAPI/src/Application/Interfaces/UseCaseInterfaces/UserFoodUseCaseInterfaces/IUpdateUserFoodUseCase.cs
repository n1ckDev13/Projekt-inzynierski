using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces
{
    public interface IUpdateUserFoodUseCase
    {
        Task<UpdateUserFoodResponse> UpdateUserFoodAsync(UpdateUserFoodDTO updateUserFoodDTO);
    }
}
