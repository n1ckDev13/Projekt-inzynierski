using ClassLibrary.DTOs.UserDTOs;
using FluentValidation;

namespace TrainingTrackAPI.Application.Services.UserServices
{
    public class RegisterUserValidationService : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserValidationService()
        {
            RuleFor(x => x.userName)
                .NotEmpty().WithMessage("Nazwa użytkownika jest wymagana!")
                .MinimumLength(5).WithMessage("Nazwa użytkownika musi zawierać między 5, a 20 znaków!")
                .MaximumLength(20).WithMessage("Nazwa użytkownika musi zawierać między 5, a 20 znaków!")
                .Must(userName => !userName.Contains(" ")).WithMessage("Nazwa użytkownika nie może zawierać spacji!");

           
            RuleFor(x => x.userMail)
                .NotEmpty().WithMessage("Email jest wymagany!")
                .EmailAddress().WithMessage("Nieprawidłowy email!")
                .Must(userMail => !userMail.Contains(" ")).WithMessage("Email nie może zawierać spacji!");


            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Hasło jest wymagane!")
                .MinimumLength(8).WithMessage("Hasło musi zawierać pomiędzy 8, a 32 znaków!")
                .MaximumLength(32).WithMessage("Hasło musi zawierać pomiędzy 8, a 32 znaków!")
                .Matches("[A-Z]").WithMessage("Hasło musi zawierać przynajmniej jedną dużą literę!")
            .Matches("[a-z]").WithMessage("Hasło musi zawierać przynajmniej jedną małą literę!")
            .Matches("[0-9]").WithMessage("Hasło musi zawierać przynajmniej jedną cyfrę!")
            .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Hasło musi zawierać przynajmniej jeden znak specjalny z \"[!@#$%^&*(),.?\\\":{}|<>]\"")
            .Must(password => !password.Contains(" ")).WithMessage("Hasło nie może zawierać spacji!");
            

            RuleFor(x => x.confirmPassword).Equal(x => x.password).WithMessage("Hasła się nie zgadzają!");
        }

    }
}
