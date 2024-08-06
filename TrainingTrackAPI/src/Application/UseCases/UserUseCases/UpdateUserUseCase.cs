using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.UserServices;

namespace TrainingTrackAPI.Application.UseCases.UserUseCases
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IEncryptionService _encryptionService;
        private readonly UpdateUserValidationService _updateUserValidationService;
        private readonly IVerifyPasswordService _verifyPasswordService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUsersRepository _repo;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateUserUseCase(IEncryptionService encryptionService,
            UpdateUserValidationService updateUserValidationService,
            IVerifyPasswordService verifyPasswordService,
            IJwtTokenService jwtTokenService, 
            IUsersRepository repo, IUnitOfWork unitOfWork)
        {
            _encryptionService = encryptionService;
            _updateUserValidationService = updateUserValidationService;
            _verifyPasswordService = verifyPasswordService;
            _jwtTokenService = jwtTokenService;
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            var user = await _repo.CheckIfUserExists(updateUserDTO.Id);

            if (user is null)
                return new UpdateUserResponse(false, "User does not exist.", null, null);

            var validationResult = _updateUserValidationService.Validate(updateUserDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new UpdateUserResponse(false, "Błąd walidacji.", null, errorMessages);
            }

            var passwordToCheck = updateUserDTO.UserPassword;

            var verificationResult = _verifyPasswordService.VerifyPassword(passwordToCheck, user.Password);

            if (!verificationResult)
                return new UpdateUserResponse(false, "Nieprawidłowe hasło!", null, null);



            string newToken = string.Empty;

            try
            {

                if (string.IsNullOrEmpty(updateUserDTO.NewPassword))
                {


                   var dataForToken = await _repo.UpdateUserDataAsync(updateUserDTO.Id, updateUserDTO);

                    newToken = _jwtTokenService.GenerateToken(dataForToken.id, dataForToken.userName,
                        dataForToken.userMail);
                }
                else
                {
                    var newPassword = _encryptionService.HashPassword(updateUserDTO.NewPassword);
                    updateUserDTO.NewPassword = newPassword;
                    _repo.UpdateUserPasswordAsync(updateUserDTO.Id, updateUserDTO);

                    var dataForToken = await _repo.UpdateUserDataAsync(updateUserDTO.Id, updateUserDTO);

                    newToken = _jwtTokenService.GenerateToken(dataForToken.id, dataForToken.userName,
                        dataForToken.userMail);
                }

                _unitOfWork.CommitAsync();
                
                

            }
            catch (Exception e)
            {
                _unitOfWork.RollbackAsync();
                return new UpdateUserResponse(false, "Database error.", null, null);
            
            }


            return new UpdateUserResponse(true, "Dane konta zmienione pomyślnie.", newToken, null);



        }
    }

    

    
}
