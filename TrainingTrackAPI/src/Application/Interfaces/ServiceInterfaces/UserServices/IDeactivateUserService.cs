using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices
{
    public interface IDeactivateUserService
    {
        Task<DeactivateUserResponse> DeactivateUserAsync(DeactivateUserDTO deactivateUserDTO);
    }
}
