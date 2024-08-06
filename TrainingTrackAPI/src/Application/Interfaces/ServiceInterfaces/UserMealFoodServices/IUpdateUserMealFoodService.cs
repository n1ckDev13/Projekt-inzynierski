using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices
{
    public interface IUpdateUserMealFoodService
    {
        Task<UpdateUserMealFoodResponse> UpdateMealFoodAsync(UpdateUserMealFoodDTO updateUserMealFoodDTO);
    }
}
