using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class LoginUserService : ILoginUserService
    {
        private readonly ILoginUserUseCase _loginUserUseCase;

        public LoginUserService(ILoginUserUseCase loginUserUseCase)
        {
            _loginUserUseCase = loginUserUseCase;
        }

        public async Task<LoginUserResponse> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            return await _loginUserUseCase.LoginUserAsync(loginUserDTO);
        }
    }
}
