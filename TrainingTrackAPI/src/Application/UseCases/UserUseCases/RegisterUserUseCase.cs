using ClassLibrary.DTOs.UserDTOs;
using ClassLibrary.Responses.User;
using TrainingTrackAPI.Application.Interfaces.RepositoryInterfaces;
using TrainingTrackAPI.Application.Interfaces.ServiceInterfaces.UserServices;
using TrainingTrackAPI.Application.Interfaces.UnitOfWorkInterface;
using TrainingTrackAPI.Application.Interfaces.UseCaseInterfaces.UserUseCaseInterfaces;
using TrainingTrackAPI.Application.Services.UserServices;
using TrainingTrackAPI.Domain.Entities;

namespace TrainingTrackAPI.Application.UseCases.UserUseCases
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IEncryptionService _encryptionService;
        private readonly RegisterUserValidationService _registerUserValidationService;
        private readonly IUsersRepository _repo;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserUseCase(IEncryptionService encryptionService, IUsersRepository repo, IUnitOfWork unitOfWork, RegisterUserValidationService registerUserValidationService)
        {
            _encryptionService = encryptionService;
            _registerUserValidationService = registerUserValidationService;
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var validationResult = _registerUserValidationService.Validate(registerUserDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return new RegisterUserResponse(false, "Błąd walidacji.", errorMessages);
            }
                
               


            var emailExists = await _repo.CheckIfMailIsUsed(registerUserDTO.userMail);

            if (emailExists)
                return new RegisterUserResponse(false, "Podany adres email jest już w użyciu.", null);



            
            var hashedPassword = _encryptionService.HashPassword(registerUserDTO.password);

            var userEntity = new User
            {
                UserName = registerUserDTO.userName,
                UserMail = registerUserDTO.userMail,
                Password = hashedPassword,
                IsActive = true
            };

            try
            {
                //await _unitOfWork.BeginTransactionAsync();

                await _repo.CreateAsync(userEntity);
                //await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitAsync();
            }
            catch (Exception e) 
            {
                await _unitOfWork.RollbackAsync();

                var errors = new List<string>();
                errors.Add(e.Message);

                return new RegisterUserResponse(false, "Database error.", errors);
            }

            string details = $"New user's id: {userEntity.Id}";
            List<string> detailsList = new List<string> { details };
            return new RegisterUserResponse(true, "Konto utworzone pomyślnie.", detailsList);
        }
    }
}
