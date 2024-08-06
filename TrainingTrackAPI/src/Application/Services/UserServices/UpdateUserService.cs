using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IUpdateUserUseCase _updateUserUseCase;

        public UpdateUserService(IUpdateUserUseCase updateUserUseCase)
        {
            _updateUserUseCase = updateUserUseCase;
        }


        public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            return await _updateUserUseCase.UpdateUserAsync(updateUserDTO);
        }
    }
}
