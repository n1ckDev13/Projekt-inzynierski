using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class DeactivateUserService : IDeactivateUserService
    {
        private readonly IDeactivateUserUseCase _deactivateUserUseCase;

        public DeactivateUserService(IDeactivateUserUseCase deactivateUserUseCase)
        {
            _deactivateUserUseCase = deactivateUserUseCase;
        }

        public async Task<DeactivateUserResponse> DeactivateUserAsync(DeactivateUserDTO deactivateUserDTO)
        {
            return await _deactivateUserUseCase.DeactivateUserAsync(deactivateUserDTO);
        }
    }
}
