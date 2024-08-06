using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices
{
    public interface ILoginUserService
    {
        Task<LoginUserResponse> LoginUserAsync(LoginUserDTO loginRequestDTO);
    }
}
