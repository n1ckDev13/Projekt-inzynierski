using ClassLibrary.DTOs.UserFoodDTOs;
using ClassLibrary.Responses.UserFood;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserFoodServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserFoodUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserFoodUseCases;

namespace TrainingTrackAPI.Application.Services.UserFoodServices
{
    public class UpdateUserFoodService : IUpdateUserFoodService
    {
        private readonly IUpdateUserFoodUseCase _updateUserFoodUseCase; 

        public UpdateUserFoodService(IUpdateUserFoodUseCase updateUserFoodUseCase)
        {
            _updateUserFoodUseCase = updateUserFoodUseCase;
        }

        public async Task<UpdateUserFoodResponse> UpdateUserFoodAsync(UpdateUserFoodDTO updateUserFoodDTO)
        {
            return await _updateUserFoodUseCase.UpdateUserFoodAsync(updateUserFoodDTO);
        }
    }
}
