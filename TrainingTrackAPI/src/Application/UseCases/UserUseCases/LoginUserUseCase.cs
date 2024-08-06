using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;

namespace TrainingTrackAPI.Application.UseCases.UserUseCases
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IEncryptionService _encryptionService;
        private readonly IVerifyPasswordService _verifyPasswordService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUsersRepository _repo;


        public LoginUserUseCase(IEncryptionService encryptionService, IVerifyPasswordService verifyPasswordService,
            IJwtTokenService jwtTokenService,IUsersRepository repo)
        {
            _encryptionService = encryptionService;
            _verifyPasswordService = verifyPasswordService;
            _jwtTokenService = jwtTokenService;
            _repo = repo;
        }

        public async Task<LoginUserResponse> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _repo.CheckIfAccountExists(loginUserDTO.UserMail);

            if (user is null)
                return new LoginUserResponse(false, "Nieprawidłowy email lub hasło!", null, null);

            

            var passwordToCheck = loginUserDTO.Password;

            var verificationResult = _verifyPasswordService.VerifyPassword(passwordToCheck, user.Password);

            if (!verificationResult)
                return new LoginUserResponse(false, "Nieprawidłowy email lub hasło!", null, null);

            if (user.IsActive == false)
                return new LoginUserResponse(false, "To konto jest dezaktywowane.", null, null);


            var generatedToken = _jwtTokenService.GenerateToken((user.Id).ToString(), user.UserName, user.UserMail);

            return new LoginUserResponse(true, "Login successful", generatedToken, null);
        }
    }
}
