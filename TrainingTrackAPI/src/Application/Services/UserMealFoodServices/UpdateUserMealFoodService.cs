using ClassLibrary.DTOs.UserMealFoodDTOs;
using ClassLibrary.Responses.UserMealFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserMealFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserMealFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserMealFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserMealFoodServices
{
    public class UpdateUserMealFoodService : IUpdateUserMealFoodService
    {
        private readonly IUpdateUserMealFoodUseCase _updateUserMealFoodUseCase;

        public UpdateUserMealFoodService(IUpdateUserMealFoodUseCase updateUserMealFoodUseCase)
        {
            _updateUserMealFoodUseCase = updateUserMealFoodUseCase;
        }

        public async Task<UpdateUserMealFoodResponse> UpdateMealFoodAsync(UpdateUserMealFoodDTO updateUserMealFoodDTO)
        {
            return await _updateUserMealFoodUseCase.UpdateUserMealFoodAsync(updateUserMealFoodDTO);
        }
    }
}
