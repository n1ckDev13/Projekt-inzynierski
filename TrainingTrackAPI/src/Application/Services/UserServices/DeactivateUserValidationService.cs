using ClassLibrary.DTOs.UserDTOs;
using FluentValidation;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class DeactivateUserValidationService : AbstractValidator<DeactivateUserDTO>
    {
        public DeactivateUserValidationService() 
        {
            RuleFor(x => x.password)
              .NotEmpty().WithMessage("Password field cannot be empty!");

            RuleFor(x => x.confirmPassword)
              .Equal(x => x.password).WithMessage("Hasła się nie zgadzają!");
        
        }
    }
}
