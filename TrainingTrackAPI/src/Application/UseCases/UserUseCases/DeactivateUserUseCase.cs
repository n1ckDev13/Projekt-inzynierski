using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.UserServices;

namespace TrainingTrackAPI.Application.UseCases.UserUseCases
{
    public class DeactivateUserUseCase : IDeactivateUserUseCase
    {
        private readonly DeactivateUserValidationService _deactivateUserValidationService;
        private readonly IEncryptionService _encryptionService;
        private readonly IVerifyPasswordService _verifyPasswordService;
        private readonly IUsersRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateUserUseCase(DeactivateUserValidationService deactivateUserValidationService, 
            IEncryptionService encryptionService, IVerifyPasswordService verifyPasswordService, 
            IUsersRepository repo, IUnitOfWork unitOfWork)
        {
            _deactivateUserValidationService = deactivateUserValidationService;
            _encryptionService = encryptionService;
            _verifyPasswordService = verifyPasswordService;
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeactivateUserResponse> DeactivateUserAsync(DeactivateUserDTO deactivateUserDTO)
        {
            var user = await _repo.CheckIfUserExists(deactivateUserDTO.id);

            if (user is null)
                return new DeactivateUserResponse(false, "User does not exist.", null);

            var validationResult = _deactivateUserValidationService.Validate(deactivateUserDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new DeactivateUserResponse(false, "Błąd walidacji.", errorMessages);
            }

            var passwordToCheck = deactivateUserDTO.password;

            var verificationResult = _verifyPasswordService.VerifyPassword(passwordToCheck, user.Password);

            if (!verificationResult)
                return new DeactivateUserResponse(false, "Nieprawidłowe hasło!", null);


            try
            {
                _repo.DeactivateUserAsync(deactivateUserDTO.id);
                _unitOfWork.CommitAsync();
            }
            catch (Exception e) 
            {
               
                _unitOfWork.RollbackAsync();
                return new DeactivateUserResponse(false, "Database error.", null);
            }

            return new DeactivateUserResponse(true, "Konto dezaktywowane pomyślnie.", null);

        }
    }
}
