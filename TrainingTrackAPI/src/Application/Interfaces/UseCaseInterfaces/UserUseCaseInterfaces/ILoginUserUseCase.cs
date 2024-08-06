using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces
{
    public interface ILoginUserUseCase
    {
        Task<LoginUserResponse> LoginUserAsync(LoginUserDTO loginUserDTO);
    }
}
