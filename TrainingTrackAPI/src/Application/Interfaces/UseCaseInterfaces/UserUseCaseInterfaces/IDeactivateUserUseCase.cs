using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces
{
    public interface IDeactivateUserUseCase
    {
        Task<DeactivateUserResponse> DeactivateUserAsync(DeactivateUserDTO deactivateUserDTO);
    }
}
