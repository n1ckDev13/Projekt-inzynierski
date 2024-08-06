using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.UseCases.UserUseCases;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IRegisterUserUseCase _registerUserUseCase;

        public RegisterUserService(IRegisterUserUseCase registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            return await _registerUserUseCase.RegisterUserAsync(registerUserDTO);

            
        }
    }
}
